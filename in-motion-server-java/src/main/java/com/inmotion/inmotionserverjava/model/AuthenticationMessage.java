package com.inmotion.inmotionserverjava.model;

import java.io.Serializable;

public record AuthenticationMessage(
        String JwtToken
) implements Serializable {
}
