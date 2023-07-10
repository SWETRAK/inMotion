package com.example.inmotionserverjava;

import io.minio.MinioClient;
import io.minio.PutObjectArgs;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.ByteArrayInputStream;

@Service
public class MediaService {

    @Value("${minio.bucket.name}")
    private String bucketName;
    private final MinioClient minioClient;

    private final Logger logger;

    public MediaService(MinioClient minioClient) {
        this.minioClient = minioClient;
        this.logger = LoggerFactory.getLogger(MediaService.class);
    }

    public void addProfileGif(String filename, MultipartFile mp4File) {
        MP4ToSmallGifConverter converter = new MP4ToSmallGifConverter(mp4File);
        converter.convert();
        byte[] gifFile = converter.getOutput();
        try {
            minioClient.putObject(PutObjectArgs.builder()
                    .bucket(bucketName)
                    .object(filename)
                    .stream(new ByteArrayInputStream(gifFile), gifFile.length, -1)
                    .contentType("image/gif")
                    .build());
        } catch (Exception e) {
            logger.error(e.getMessage());
        }
    }

//    public Object getByFilename(String filename) {
//        InputStream stream;
//        try {
//            stream = minioClient.getObject(GetObjectArgs.builder()
//                    .bucket("cinehub")
//                    .object(filename)
//                    .build());
//            return IOUtils.toByteArray(stream);
//        } catch (Exception e) {
//            throw new PosterNotFoundException(filename);
//        }
//    }

//    @Override
//    public void deleteByFilename(String filename) {
//        try {
//            minioClient.removeObject(RemoveObjectArgs.builder().bucket(bucketName).object(filename).build());
//        } catch (Exception e){
//            throw new PosterNotFoundException(filename);
//        }
//    }
}
