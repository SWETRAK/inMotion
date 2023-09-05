package com.inmotion.inmotionserverjava.services;

import com.inmotion.inmotionserverjava.model.AuthenticationMessage;
import com.inmotion.inmotionserverjava.model.BaseMessage;
import com.inmotion.inmotionserverjava.model.MasstransitEvent;
import lombok.AllArgsConstructor;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
@AllArgsConstructor
public class MessagePublisher {

    private final RabbitTemplate rabbitTemplate;

    public void publishTestEvent() {
        MasstransitEvent<BaseMessage<AuthenticationMessage>> masstransitEvent = new MasstransitEvent<>(
                List.of("urn:message:IMS.Shared.Messaging.Messages:ImsBaseMessage[[IMS.Shared.Messaging.Messages.JWT:RequestJwtValidationMessage]]"),
                new BaseMessage<>(
                        false,
                        null,
                        new AuthenticationMessage("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy" +
                                "54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Ijc1OWMyYmE4" +
                                "LTU5YWQtNDY5Ny04MGZkLTYxZTFhZTg2MjRkYiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMD" +
                                "UvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImthbWlscGlldHJhazEyM0BnbWFpbC5jb20iLCJo" +
                                "dHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiU1dFVF" +
                                "JBSyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9s" +
                                "ZSI6IlVzZXIiLCJleHAiOjE2OTQ0MzI1MTEsImlzcyI6ImlybC1iYWNrZW5kOjgwIiwiYXVkIjoiaXJsLWJhY2" +
                                "tlbmQ6ODAifQ.8dV_GipRmZYj8bgjEKTpuiTJp6mTDsnohR0GWZS2Kbg")
                )
        );

        rabbitTemplate.convertAndSend("validate-jwt-event", masstransitEvent);
    }
}
