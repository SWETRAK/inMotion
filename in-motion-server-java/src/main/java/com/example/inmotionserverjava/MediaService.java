package com.example.inmotionserverjava;

import com.example.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import io.minio.MinioClient;
import io.minio.PutObjectArgs;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.ByteArrayInputStream;

@Service
@RequiredArgsConstructor
public class MediaService {

    @Value("${minio.buckets.profile_videos}")
    private String profileVideosBucket;

    @Value("${minio.buckets.posts}")
    private String postsBucket;
    private final MinioClient minioClient;
    private final Logger logger = LoggerFactory.getLogger(MediaService.class);
    private final MP4ToSmallGifConverter mp4ToGifConverter;

    public void addProfileVideo(String filename, MultipartFile mp4File, String nickname) {

        byte[] gifFileBytes = mp4ToGifConverter.convert(mp4File);

        try {
            minioClient.putObject(PutObjectArgs.builder()
                    .bucket(profileVideosBucket)
                    .object(nickname + "/gif/" + filename + ".gif")
                    .stream(new ByteArrayInputStream(gifFileBytes), gifFileBytes.length, -1)
                    .contentType("image/gif")
                    .build());

            byte[] mp4FileBytes =  mp4File.getBytes();

            minioClient.putObject(PutObjectArgs.builder()
                    .bucket(profileVideosBucket)
                    .object(nickname + "/mp4/" + filename + ".mp4")
                    .stream(new ByteArrayInputStream(mp4FileBytes), mp4FileBytes.length, -1)
                    .contentType("video/mp4")
                    .build());
        } catch (Exception e) {
            throw new MinioFilePostingException();
        }

        logger.info("User {} posted new profile video", nickname);
    }
}
