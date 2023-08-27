package com.inmotion.inmotionserverjava.model;

import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.io.Serializable;
import java.util.List;

public record MasstransitEvent<T extends Serializable>(
        List<String> messageType,
        T message
) implements Serializable {

}

