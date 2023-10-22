package com.inmotion.inmotionserverjava.exception.converter;

public class BadFileExtensionException extends RuntimeException {
    public BadFileExtensionException() {
        super("File extension is not mp4!");
    }
}
