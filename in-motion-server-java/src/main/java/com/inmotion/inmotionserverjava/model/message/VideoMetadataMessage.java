package com.inmotion.inmotionserverjava.model.message;

import java.io.Serializable;

public record VideoMetadataMessage(
        String bucketName,
        String bucketLocation,
        String filename,
        String contentType,
        String type
) implements Serializable {
}
