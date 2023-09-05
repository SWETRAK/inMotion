package com.inmotion.inmotionserverjava.model;

import java.io.Serializable;
import java.util.List;

public record MasstransitEvent<T extends Serializable>(
        List<String> messageType,
        T message
) implements Serializable {

}

