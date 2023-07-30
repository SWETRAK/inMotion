package com.inmotion.inmotionserverjava.services;

import com.inmotion.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.model.ProfileVideoUploadInfoDto;
import com.inmotion.inmotionserverjava.model.UserInfoDto;
import com.inmotion.inmotionserverjava.services.interfaces.MediaService;
import com.inmotion.inmotionserverjava.services.interfaces.MinioService;
import com.inmotion.inmotionserverjava.util.MP4ToSmallGifConverter;
import io.minio.MinioClient;
import io.minio.PutObjectArgs;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.autoconfigure.security.SecurityProperties;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.ByteArrayInputStream;
import java.io.IOException;

@Slf4j
@Service
@RequiredArgsConstructor
public class MediaServiceImpl implements MediaService {

    private static final String PROFILE_VIDEO_NAME_TEMPLATE = "%s/%s/profile_%s%s";
    //TODO: write proper path
    private static final String PROFILE_VIDEO_MP4_GET_ADDRESS = "http://localhost:8080/api/media/profile/video/mp4/";
    private static final String PROFILE_VIDEO_GIF_GET_ADDRESS = "http://localhost:8080/api/media/profile/video/mp4/";

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
            minioService.uploadFile(profileVideosBucket, outputVideoPath, mp4FileBytes, MediaType.parseMediaType("video/mp4"));
        } catch (IOException e) {
            throw new MinioFilePostingException();
        }


        log.info("User {} posted new profile video", user.getNickname());
        return new ProfileVideoUploadInfoDto(
                PROFILE_VIDEO_MP4_GET_ADDRESS + user.getNickname() + "/" + user.getId(),
                PROFILE_VIDEO_GIF_GET_ADDRESS + user.getNickname() + "/" + user.getId()
        );
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
