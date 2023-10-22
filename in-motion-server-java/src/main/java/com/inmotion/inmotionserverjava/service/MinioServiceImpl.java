package com.inmotion.inmotionserverjava.service;

import com.inmotion.inmotionserverjava.exception.minio.MinioFileNotFoundException;
import com.inmotion.inmotionserverjava.exception.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.service.interfaces.MinioService;
import io.minio.GetObjectArgs;
import io.minio.MinioClient;
import io.minio.PutObjectArgs;
import io.minio.errors.*;
import lombok.AllArgsConstructor;
import org.apache.commons.compress.utils.IOUtils;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;

@Service
@AllArgsConstructor
public class MinioServiceImpl implements MinioService {

    private final MinioClient minioClient;

    @Override
    public void uploadFile(String bucketName, String filePath, byte[] file, MediaType mediaType) {
        try {
            minioClient.putObject(PutObjectArgs.builder()
                    .bucket(bucketName)
                    .object(filePath)
                    .stream(new ByteArrayInputStream(file), file.length, -1)
                    .contentType(mediaType.toString())
                    .build());
        } catch (ErrorResponseException | InvalidKeyException | InvalidResponseException | IOException |
                 NoSuchAlgorithmException | ServerException | XmlParserException | InsufficientDataException |
                 InternalException e) {
            throw new MinioFilePostingException();
        }
    }

    @Override
    public byte[] getFile(String bucketName, String filePath) {
        InputStream requestedFileStream;
        try {
            requestedFileStream = minioClient.getObject(GetObjectArgs.builder()
                    .bucket(bucketName)
                    .object(filePath)
                    .build());
            return IOUtils.toByteArray(requestedFileStream);
        } catch (Exception e) {
            throw new MinioFileNotFoundException(filePath, bucketName);
        }
    }
}
