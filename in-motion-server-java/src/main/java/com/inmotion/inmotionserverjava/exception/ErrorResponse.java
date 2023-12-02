package com.inmotion.inmotionserverjava.exception;

import java.io.Serializable;

public record ErrorResponse(String message) implements Serializable {
}
