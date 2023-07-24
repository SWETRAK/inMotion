package com.inmotion.inmotionserverjava.exceptions.minio;

public class MinioFilePostingException extends RuntimeException{
    public MinioFilePostingException() {
        super("System couldn't post a file to object storage!");
    }
}
