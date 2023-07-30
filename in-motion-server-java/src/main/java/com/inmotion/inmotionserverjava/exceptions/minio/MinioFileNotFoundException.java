package com.inmotion.inmotionserverjava.exceptions.minio;

public class MinioFileNotFoundException extends RuntimeException{
    public MinioFileNotFoundException(String filePath, String bucketName) {
        super("File " + filePath + " not found in bucket: " + bucketName);
    }
}
