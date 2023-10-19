package com.inmotion.inmotionserverjava.services;

import com.inmotion.inmotionserverjava.config.RabbitConfiguration;
import com.inmotion.inmotionserverjava.model.message.AuthenticationMessage;

import com.inmotion.inmotionserverjava.model.message.UpdatePostVideoMetadataMessage;
import com.inmotion.inmotionserverjava.model.message.UpdateUserProfileVideoMessage;
import lombok.AllArgsConstructor;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Component;

@Component
@AllArgsConstructor
public class MessagePublisher {

    private final RabbitTemplate rabbitTemplate;

    public void publishJwtValidationEvent(AuthenticationMessage authenticationMessage) {
        var response = rabbitTemplate.convertSendAndReceive(RabbitConfiguration.JWT_QUEUE, authenticationMessage, m -> {
            m.getMessageProperties().setHeader("type", "jwtValidation");
            return m;
        });

        // Response should be type of BaseMessage<ValidatedUserInfoMessage>
        // I can't map it to this type

        System.out.println(response);
    }

    public void publishVideoUploadedEvent(UpdatePostVideoMetadataMessage videoMetadata) {
        rabbitTemplate.convertAndSend(RabbitConfiguration.UPDATE_POST_VIDEO_QUEUE, videoMetadata);
    }

    public void publishUserProfileVideoUploadEvent(UpdateUserProfileVideoMessage profileVideo) {
        rabbitTemplate.convertAndSend(RabbitConfiguration.UPDATE_PROFILE_VIDEO_QUEUE, profileVideo);
    }
}
