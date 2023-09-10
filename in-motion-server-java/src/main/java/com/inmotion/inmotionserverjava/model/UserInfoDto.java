package com.inmotion.inmotionserverjava.model;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class UserInfoDto {
    private String id;
    private String email;
    private String nickname;
    private String token;
}
