package com.example.inmotionserverjava.exceptions.minio;

public class MinioConfigurationException extends RuntimeException{

    public MinioConfigurationException(String message) {
        super("System couldn't connect or initialize MinIO object storage due to: " + message);

    }
}
