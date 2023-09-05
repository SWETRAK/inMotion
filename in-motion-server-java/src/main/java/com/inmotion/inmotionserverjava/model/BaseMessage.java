package com.inmotion.inmotionserverjava.model;

import java.io.Serializable;

public record BaseMessage<T> (
        boolean Error,
        String ErrorMessage,
        T Data
)implements Serializable {}
