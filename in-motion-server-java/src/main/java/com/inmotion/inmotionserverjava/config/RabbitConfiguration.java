package com.inmotion.inmotionserverjava.config;

import org.springframework.amqp.rabbit.annotation.EnableRabbit;
import org.springframework.amqp.rabbit.config.SimpleRabbitListenerContainerFactory;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.amqp.support.converter.MessageConverter;
import org.springframework.boot.autoconfigure.amqp.SimpleRabbitListenerContainerFactoryConfigurer;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
@EnableRabbit
public class RabbitConfiguration {

    public static final String JWT_QUEUE = "queue:jwt-validator";
    public static final String UPDATE_POST_VIDEO_QUEUE = "queue:update-post-video";
    public static final String UPDATE_PROFILE_VIDEO_QUEUE = "queue:update-profile-video";


    @Bean
    SimpleRabbitListenerContainerFactory containerFactory(ConnectionFactory connectionFactory,
                                                          SimpleRabbitListenerContainerFactoryConfigurer configurer) {
        SimpleRabbitListenerContainerFactory factory = new SimpleRabbitListenerContainerFactory();

        configurer.configure(factory, connectionFactory);
        factory.setMessageConverter(consumerJackson2MessageConverter());
        factory.setConnectionFactory(connectionFactory);
        return factory;
    }

    @Bean
    public RabbitTemplate rabbitTemplate(ConnectionFactory connectionFactory) {
        final RabbitTemplate rabbitTemplate = new RabbitTemplate(connectionFactory);
        rabbitTemplate.setMessageConverter(consumerJackson2MessageConverter());
        return rabbitTemplate;
    }

    @Bean
    public MessageConverter consumerJackson2MessageConverter() {
        return new Jackson2JsonMessageConverter();
    }
}
