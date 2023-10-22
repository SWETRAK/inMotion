package com.inmotion.inmotionserverjava.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class UserInfoDto {
    private String id;
    private String email;
    private String nickname;
    private String token;
}
