package com.inmotion.inmotionserverjava.model.message;

import java.io.Serializable;

public record BaseMessage<T> (
        boolean error,
        String errorMessage,
        T data
)implements Serializable {}
