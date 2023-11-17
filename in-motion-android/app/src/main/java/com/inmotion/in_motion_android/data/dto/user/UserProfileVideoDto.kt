package com.inmotion.in_motion_android.data.dto.user

data class UserProfileVideoDto(
    val id: String,
    val userId: String,
    val filename: String,
    val bucketName: String,
    val bucketLocation: String,
    val contentType: String
)
