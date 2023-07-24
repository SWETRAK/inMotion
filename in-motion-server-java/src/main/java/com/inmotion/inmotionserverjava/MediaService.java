package com.inmotion.inmotionserverjava;

import com.inmotion.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.model.UserInfoDto;
import com.inmotion.inmotionserverjava.util.MP4ToSmallGifConverter;
import io.minio.MinioClient;
import io.minio.PutObjectArgs;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.ByteArrayInputStream;

@Slf4j
@Service
@RequiredArgsConstructor
public class MediaService {

    private static final String PROFILE_VIDEO_NAME_TEMPLATE = "%s/%s/profile_%s%s";

    @Value("${minio.buckets.profile_videos}")
    private String profileVideosBucket;

    @Value("${minio.buckets.posts}")
    private String postsBucket;
    private final MinioClient minioClient;
    private final MP4ToSmallGifConverter mp4ToGifConverter;

    public void addProfileVideo(MultipartFile mp4File, String jwtToken) {

        UserInfoDto user = validateJwt(jwtToken);

        String outputVideoPath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, user.getNickname(), "/mp4/", user.getId(), ".mp4");
        String outputGifPath = String.format(PROFILE_VIDEO_NAME_TEMPLATE, user.getNickname(), "/gif/", user.getId(), ".gif");

        byte[] gifFileBytes = mp4ToGifConverter.convert(mp4File);

        try {
            minioClient.putObject(PutObjectArgs.builder()
                    .bucket(profileVideosBucket)
                    .object(outputGifPath)
                    .stream(new ByteArrayInputStream(gifFileBytes), gifFileBytes.length, -1)
                    .contentType("image/gif")
                    .build());

            byte[] mp4FileBytes =  mp4File.getBytes();

            minioClient.putObject(PutObjectArgs.builder()
                    .bucket(profileVideosBucket)
                    .object(outputVideoPath)
                    .stream(new ByteArrayInputStream(mp4FileBytes), mp4FileBytes.length, -1)
                    .contentType("video/mp4")
                    .build());
        } catch (Exception e) {
            throw new MinioFilePostingException();
        }

        log.info("User {} posted new profile video", user.getNickname());
    }

    // Temporary solution before adding RabbitMQ support
    // TODO: Write as supposed to be with call to message queue
    private UserInfoDto validateJwt(String jwtToken){
        return new UserInfoDto(

                "6ef2737d-bca0-4635-91ca-90a45cab96ea",
                "test@test.com",
                "TestUser#1",
                jwtToken
        );
    }
}
