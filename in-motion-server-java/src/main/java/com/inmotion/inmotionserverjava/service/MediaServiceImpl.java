package com.inmotion.inmotionserverjava.service;

import com.inmotion.inmotionserverjava.exception.UnauthorizedUserException;
import com.inmotion.inmotionserverjava.exception.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.model.*;
import com.inmotion.inmotionserverjava.model.message.*;
import com.inmotion.inmotionserverjava.service.interfaces.MediaService;
import com.inmotion.inmotionserverjava.service.interfaces.MinioService;
import com.inmotion.inmotionserverjava.util.MP4ToSmallGifConverter;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
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
    private static final String PROFILE_VIDEO_MP4_GET_ADDRESS_PREFIX = "/api/profile/video/mp4/";
    private static final String PROFILE_VIDEO_GIF_GET_ADDRESS_PREFIX = "/api/profile/video/gif/";
    private static final String POST_GET_ADDRESS_PREFIX = "/api/post/";
    private static final MediaType VIDEO_MEDIA_TYPE = MediaType.parseMediaType("video/mp4");

    @Value("${minio.buckets.profile_videos}")
    private String profileVideosBucket;

    @Value("${minio.buckets.posts}")
    private String postsBucket;
    private final MinioService minioService;
    private final MP4ToSmallGifConverter mp4ToGifConverter;
    private final MessagePublisher messagePublisher;


    // TODO: Rollback mechanism if one of files not uploaded
    public ProfileVideoUploadInfoDto addProfileVideo(MultipartFile mp4File, String jwtToken) {
        ValidatedUserInfoMessage user = validateJwt(jwtToken);

        String outputVideoPath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, user.nickname(), "/mp4/", user.id(), ".mp4");
        String outputGifPath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, user.nickname(), "/gif/", user.id(), ".gif");

        byte[] gifFileBytes = mp4ToGifConverter.convert(mp4File);
        minioService.uploadFile(profileVideosBucket, outputGifPath, gifFileBytes, MediaType.IMAGE_GIF);

        try {
            byte[] mp4FileBytes = mp4File.getBytes();
            minioService.uploadFile(profileVideosBucket, outputVideoPath, mp4FileBytes, VIDEO_MEDIA_TYPE);
        } catch (IOException e) {
            // rollback mechanism here
            throw new MinioFilePostingException();
        }

        messagePublisher.publishUserProfileVideoUploadEvent(new UpdateUserProfileVideoMessage(
                user.id(),
                user.id(),
                profileVideosBucket,
                profileVideosBucket,
                mp4File.getContentType()
        ));

        log.info("User {} posted new profile video", user.nickname());

        return new ProfileVideoUploadInfoDto(
                PROFILE_VIDEO_MP4_GET_ADDRESS_PREFIX + user.nickname() + "/" + user.id(),
                PROFILE_VIDEO_GIF_GET_ADDRESS_PREFIX + user.nickname() + "/" + user.id()
        );
    }

    @Override
    public PostUploadInfoDto addPost(MultipartFile frontVideo, MultipartFile backVideo, String jwtToken) {
        ValidatedUserInfoMessage user = validateJwt(jwtToken);
        String postId = UUID.randomUUID().toString();
        String frontVideoPath = String.format(POST_FILE_NAME_TEMPLATE, postId, "/front_", postId);
        String backVideoPath = String.format(POST_FILE_NAME_TEMPLATE, postId, "/back_", postId);
        try {
            minioService.uploadFile(postsBucket, frontVideoPath, frontVideo.getBytes(), VIDEO_MEDIA_TYPE);
            minioService.uploadFile(postsBucket, backVideoPath, frontVideo.getBytes(), VIDEO_MEDIA_TYPE);

            messagePublisher.publishVideoUploadedEvent(new UpdatePostVideoMetadataMessage(
                    postId,
                    user.id(),
                    List.of(
                            new VideoMetadataMessage(postsBucket, postsBucket, frontVideoPath, VIDEO_MEDIA_TYPE.toString(), "video/mp4"),
                            new VideoMetadataMessage(postsBucket, postsBucket, backVideoPath, VIDEO_MEDIA_TYPE.toString(), "video/mp4")
                    )
            ));
            log.info("User {} added a post", user.nickname());
            return new PostUploadInfoDto(postId, POST_GET_ADDRESS_PREFIX + postId);
        } catch (IOException e) {
            // rollback mechanism here
            throw new MinioFilePostingException();
        }
    }

    @Override
    public byte[] getProfileVideoAsMp4(String nickname, String userId, String jwtToken) {
        validateJwt(jwtToken);
        String requestedFilePath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, nickname, "/mp4/", userId, ".mp4");
        return minioService.getFile(profileVideosBucket, requestedFilePath);
    }

    @Override
    public byte[] getProfileVideoAsGif(String nickname, String userId, String jwtToken) {
        validateJwt(jwtToken);
        String requestedFilePath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, nickname, "/gif/", userId, ".gif");
        return minioService.getFile(profileVideosBucket, requestedFilePath);
    }

    @Override
    public PostDto getPostById(String postId, String jwtToken) {
        validateJwt(jwtToken);
        String frontVideoFilePath = String.format(POST_FILE_NAME_TEMPLATE, postId, "front_", postId);
        String backVideoFilePath = String.format(POST_FILE_NAME_TEMPLATE, postId, "back_", postId);
        byte[] frontVideo = minioService.getFile(postsBucket, frontVideoFilePath);
        byte[] backVideo = minioService.getFile(postsBucket, backVideoFilePath);
        return new PostDto(frontVideo, backVideo);
    }

    private ValidatedUserInfoMessage validateJwt(String jwtToken) {
        if(jwtToken.startsWith("Bearer ")) {
            jwtToken = jwtToken.replace("Bearer ", "");
            BaseMessage<ValidatedUserInfoMessage> response = messagePublisher.publishJwtValidationEvent(new AuthenticationMessage(jwtToken));
            if (response.data() != null) {
                return response.data();
            }
        }
        
        throw new UnauthorizedUserException();
    }
}
