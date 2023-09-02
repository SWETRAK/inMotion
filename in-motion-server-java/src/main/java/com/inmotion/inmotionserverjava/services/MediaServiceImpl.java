package com.inmotion.inmotionserverjava.services;

import com.inmotion.inmotionserverjava.config.RabbitConfiguration;
import com.inmotion.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.model.*;
import com.inmotion.inmotionserverjava.services.interfaces.MediaService;
import com.inmotion.inmotionserverjava.services.interfaces.MinioService;
import com.inmotion.inmotionserverjava.util.MP4ToSmallGifConverter;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.List;
import java.util.UUID;

@Slf4j
@Service
@RequiredArgsConstructor
public class MediaServiceImpl implements MediaService {

    private static final String PROFILE_VIDEO_NAME_TEMPLATE = "%s/%s/profile_%s%s";
    private static final String POST_FILE_NAME_TEMPLATE = "%s/%s%s.mp4";
    private static final String PROFILE_VIDEO_MP4_GET_ADDRESS_PREFIX = "/api/media/profile/video/mp4/";
    private static final String PROFILE_VIDEO_GIF_GET_ADDRESS_PREFIX = "/api/media/profile/video/gif/";
    private static final String POST_GET_ADDRESS_PREFIX = "/api/media/post/";
    private static final MediaType VIDEO_MEDIA_TYPE =  MediaType.parseMediaType("video/mp4");

    @Value("${minio.buckets.profile_videos}")
    private String profileVideosBucket;

    @Value("${minio.buckets.posts}")
    private String postsBucket;
    private final RabbitTemplate rabbitTemplate;
    private final MinioService minioService;
    private final MP4ToSmallGifConverter mp4ToGifConverter;

    // TODO: Rollback mechanism if one of files not uploaded
    public ProfileVideoUploadInfoDto addProfileVideo(MultipartFile mp4File, String jwtToken) {
        UserInfoDto user = validateJwt(jwtToken);

        String outputVideoPath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, user.getNickname(), "/mp4/", user.getId(), ".mp4");
        String outputGifPath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, user.getNickname(), "/gif/", user.getId(), ".gif");

        byte[] gifFileBytes = mp4ToGifConverter.convert(mp4File);
        minioService.uploadFile(profileVideosBucket, outputGifPath, gifFileBytes, MediaType.IMAGE_GIF);

        try {
            byte[] mp4FileBytes = mp4File.getBytes();
            minioService.uploadFile(profileVideosBucket, outputVideoPath, mp4FileBytes, VIDEO_MEDIA_TYPE);
        } catch (IOException e) {
            throw new MinioFilePostingException();
        }


        log.info("User {} posted new profile video", user.getNickname());
        return new ProfileVideoUploadInfoDto(
                PROFILE_VIDEO_MP4_GET_ADDRESS_PREFIX + user.getNickname() + "/" + user.getId(),
                PROFILE_VIDEO_GIF_GET_ADDRESS_PREFIX + user.getNickname() + "/" + user.getId()
        );
    }

    @Override
    public PostUploadInfoDto addPost(MultipartFile frontVideo, MultipartFile backVideo, String jwtToken) {
        UserInfoDto user = validateJwt(jwtToken);
        String postId = UUID.randomUUID().toString();
        String frontVideoPath = String.format(POST_FILE_NAME_TEMPLATE, postId, "/front_", postId);
        String backVideoPath = String.format(POST_FILE_NAME_TEMPLATE, postId,"/back_", postId);
        try {
            minioService.uploadFile(postsBucket, frontVideoPath, frontVideo.getBytes(), VIDEO_MEDIA_TYPE);
            minioService.uploadFile(postsBucket, backVideoPath, frontVideo.getBytes(), VIDEO_MEDIA_TYPE);
            log.info("User {} added a post", user.getNickname());
            return new PostUploadInfoDto(postId, POST_GET_ADDRESS_PREFIX + postId);
        } catch (IOException e){
            throw new MinioFilePostingException();
        }
    }

    @Override
    public byte[] getProfileVideoAsMp4(String nickname, String userId) {
        String requestedFilePath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, nickname, "/mp4/", userId, ".mp4");
        return minioService.getFile(profileVideosBucket, requestedFilePath);
    }

    @Override
    public byte[] getProfileVideoAsGif(String nickname, String userId) {
        String requestedFilePath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, nickname, "/gif/", userId, ".gif");
        return minioService.getFile(profileVideosBucket, requestedFilePath);
    }

    @Override
    public PostDto getPostById(String postId) {
        String frontVideoFilePath = String.format(POST_FILE_NAME_TEMPLATE, postId, "front_", postId);
        String backVideoFilePath = String.format(POST_FILE_NAME_TEMPLATE, postId, "back_", postId);
        byte[] frontVideo = minioService.getFile(postsBucket, frontVideoFilePath);
        byte[] backVideo = minioService.getFile(postsBucket, backVideoFilePath);
        return new PostDto(frontVideo, backVideo);
    }

    // TODO: Write as supposed to be with call to message queue
    private UserInfoDto validateJwt(String jwtToken){
        rabbitTemplate.convertAndSend("validate-jwt-event", "",
                new MasstransitEvent<>(
                        //--IMS:Shared:Messaging:Messages:JWT:RequestJwtValidationMessage
                        List.of("IMS:Shared:Messaging:Messages:IMSBaseMessage", "IMS:Shared:Messaging:Messages:JWT:RequestJwtValidationMessage"),
                        new BaseMessage<>(false, null,
                                new AuthenticationMessage("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                                        "eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2Ns" +
                                        "YWltcy9uYW1laWRlbnRpZmllciI6IjFkNzI0MDZlLTE1MTYtNDZiNC04MTI2LWMwZDZmNWY1" +
                                        "NTIyNCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2" +
                                        "xhaW1zL2VtYWlsYWRkcmVzcyI6InRlc3RAdGVzdC5wbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvY" +
                                        "XAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0IiwiaHR0cDovL3Nj" +
                                        "aGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXN" +
                                        "lciIsImV4cCI6MTY5NDM1MTE3MiwiaXNzIjoiaXJsLWJhY2tlbmQ6ODAiLCJhdWQiOiJpcmwtYmF" +
                                        "ja2VuZDo4MCJ9.QnpfAUMAg4v4WYmxu3aC6JvrbtcYihpYlRjEat63ILU")
                        )
                ));
        return new UserInfoDto("", "", "", "");
    }
}
