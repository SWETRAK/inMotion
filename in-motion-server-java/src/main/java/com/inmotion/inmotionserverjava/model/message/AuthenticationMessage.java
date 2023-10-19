package com.inmotion.inmotionserverjava.model.message;

import java.io.Serializable;

public record AuthenticationMessage(
        String jwtToken
) implements Serializable {
}
