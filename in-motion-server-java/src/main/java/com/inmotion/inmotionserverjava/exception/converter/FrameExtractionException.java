package com.inmotion.inmotionserverjava.exception.converter;

public class FrameExtractionException extends RuntimeException {
    public FrameExtractionException(String message) {
        super("System couldn't extract frames from requested video due to: " + message);
    }
}
