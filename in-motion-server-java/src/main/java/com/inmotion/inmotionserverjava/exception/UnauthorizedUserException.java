package com.inmotion.inmotionserverjava.exception;

public class UnauthorizedUserException extends RuntimeException {
    public UnauthorizedUserException() {
        super("User doesn't exist or token is expired");
    }
}
