package com.inmotion.inmotionserverjava.exception;

public class NonExistingPostSideException extends RuntimeException {
    public NonExistingPostSideException(String side) {
        super("There is no side " + side + " of post!");
    }
}
