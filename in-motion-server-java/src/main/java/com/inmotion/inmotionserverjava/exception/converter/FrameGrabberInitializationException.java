package com.inmotion.inmotionserverjava.exception.converter;

public class FrameGrabberInitializationException extends RuntimeException {
    public FrameGrabberInitializationException(String message) {
        super("System couldn't initialize FrameGrabber due to: " + message);
    }
}
