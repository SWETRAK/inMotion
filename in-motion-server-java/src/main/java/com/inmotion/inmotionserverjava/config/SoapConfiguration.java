package com.inmotion.inmotionserverjava.config;

import com.inmotion.inmotionserverjava.soap.AuthenticationClient;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.oxm.jaxb.Jaxb2Marshaller;

@Configuration
public class SoapConfiguration {

    @Bean
    public Jaxb2Marshaller marshaller() {
        Jaxb2Marshaller marshaller = new Jaxb2Marshaller();
        marshaller.setContextPath("com.inmotion.soap.wsdl");
        return marshaller;
    }

    @Bean
    AuthenticationClient authenticationClient(Jaxb2Marshaller marshaller){
        AuthenticationClient client = new AuthenticationClient();
        client.setDefaultUri("http://localhost:8001/soap");
        client.setMarshaller(marshaller);
        client.setUnmarshaller(marshaller);
        return client;
    }
}
