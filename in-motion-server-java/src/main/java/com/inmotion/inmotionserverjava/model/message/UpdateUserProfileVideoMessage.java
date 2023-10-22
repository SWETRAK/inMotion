package com.inmotion.inmotionserverjava.model.message;

public record UpdateUserProfileVideoMessage(
        String userId,
        String filename,
        String bucketName,
        String bucketLocation,
        String contentType
) {
}
