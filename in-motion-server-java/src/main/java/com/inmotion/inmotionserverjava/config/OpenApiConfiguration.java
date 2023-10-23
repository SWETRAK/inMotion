package com.inmotion.inmotionserverjava.config;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OpenApiConfiguration {

    @Bean
    OpenAPI openApi() {
        return new OpenAPI()
                .info(new Info()
                        .title("InMotion Media API")
                        .description("Media API for inMotion app. Allows posts management.")
                        .version("v1.0"));
    }
}
