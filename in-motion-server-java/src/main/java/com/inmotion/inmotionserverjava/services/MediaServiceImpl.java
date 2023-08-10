package com.inmotion.inmotionserverjava.services;

import com.inmotion.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.model.PostDto;
import com.inmotion.inmotionserverjava.model.PostUploadInfoDto;
import com.inmotion.inmotionserverjava.model.ProfileVideoUploadInfoDto;
import com.inmotion.inmotionserverjava.model.UserInfoDto;
import com.inmotion.inmotionserverjava.services.interfaces.MediaService;
import com.inmotion.inmotionserverjava.services.interfaces.MinioService;
import com.inmotion.inmotionserverjava.util.MP4ToSmallGifConverter;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
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

    // Temporary solution before adding RabbitMQ support
    // TODO: Write as supposed to be with call to message queue
    private UserInfoDto validateJwt(String jwtToken){
        return new UserInfoDto(

                "6ef2737d-bca0-4635-91ca-90a45cab96ea",
                "test@test.com",
                "TestUser1",
                jwtToken
        );
    }
}
