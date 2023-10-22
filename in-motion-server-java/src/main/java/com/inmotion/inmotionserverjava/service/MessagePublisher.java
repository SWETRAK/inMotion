package com.inmotion.inmotionserverjava.service;

import com.inmotion.inmotionserverjava.config.RabbitConfiguration;
import com.inmotion.inmotionserverjava.model.message.*;

import lombok.AllArgsConstructor;
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

        if (!((boolean) response.get("Error"))) {
            ValidatedUserInfoMessage userInfo = getValidatedUserInfoMessage(response);

            return new BaseMessage<>(
                    (boolean) response.get("Error"),
                    null,
                    userInfo
            );
        }

        return new BaseMessage<>(
                (boolean) response.get("Error"),
                response.get("ErrorMessage").toString(),
                null
        );
    }

    private ValidatedUserInfoMessage getValidatedUserInfoMessage(LinkedHashMap<String, Object> response) {
        LinkedHashMap<String, Object> data = (LinkedHashMap<String, Object>) response.get("Data");

        ValidatedUserInfoMessage userInfo = new ValidatedUserInfoMessage(
                data.get("Id").toString(),
                data.get("Email").toString(),
                data.get("Nickname").toString(),
                data.get("Role").toString()
        );
        return userInfo;
    }

    public void publishVideoUploadedEvent(UpdatePostVideoMetadataMessage videoMetadata) {
        rabbitTemplate.convertAndSend(RabbitConfiguration.UPDATE_POST_VIDEO_QUEUE, videoMetadata);
    }

    public void publishUserProfileVideoUploadEvent(UpdateUserProfileVideoMessage profileVideo) {
        rabbitTemplate.convertAndSend(RabbitConfiguration.UPDATE_PROFILE_VIDEO_QUEUE, profileVideo);
    }
}
