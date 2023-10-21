package com.inmotion.inmotionserverjava.service;

import com.inmotion.inmotionserverjava.config.RabbitConfiguration;
import com.inmotion.inmotionserverjava.model.message.*;

import jakarta.validation.Valid;
import lombok.AllArgsConstructor;
import lombok.Data;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Component;

import java.util.LinkedHashMap;

@Component
@AllArgsConstructor
public class MessagePublisher {

    private final RabbitTemplate rabbitTemplate;

    public BaseMessage<ValidatedUserInfoMessage> publishJwtValidationEvent(AuthenticationMessage authenticationMessage) {
        LinkedHashMap<String, Object> response = (LinkedHashMap<String, Object>) rabbitTemplate.convertSendAndReceive(
                RabbitConfiguration.JWT_QUEUE,
                authenticationMessage, m -> {
                    m.getMessageProperties().setHeader("type", "jwtValidation");
                    return m;
                });

        BaseMessage<ValidatedUserInfoMessage> message = new BaseMessage<>(
                (boolean) response.get("Error"), 
                response.get("ErrorMessage").toString(),
                (ValidatedUserInfoMessage) response.get("Data")
        );
        
        return message;
    }

    public void publishVideoUploadedEvent(UpdatePostVideoMetadataMessage videoMetadata) {
        rabbitTemplate.convertAndSend(RabbitConfiguration.UPDATE_POST_VIDEO_QUEUE, videoMetadata);
    }

    public void publishUserProfileVideoUploadEvent(UpdateUserProfileVideoMessage profileVideo) {
        rabbitTemplate.convertAndSend(RabbitConfiguration.UPDATE_PROFILE_VIDEO_QUEUE, profileVideo);
    }
}
