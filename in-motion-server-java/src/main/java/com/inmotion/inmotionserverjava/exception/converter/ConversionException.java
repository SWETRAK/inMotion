package com.inmotion.inmotionserverjava.exception.converter;

public class ConversionException extends RuntimeException{
    public ConversionException(String message) {
        super("System couldn't convert requested video due to: " + message);
    }
}
