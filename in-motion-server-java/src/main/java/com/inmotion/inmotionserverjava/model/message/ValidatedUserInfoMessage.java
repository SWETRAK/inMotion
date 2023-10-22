package com.inmotion.inmotionserverjava.model.message;

import java.io.Serializable;

public record ValidatedUserInfoMessage(
        String id,
        String email,
        String nickname,
        String role
) implements Serializable {
}
