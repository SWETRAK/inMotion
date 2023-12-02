package com.inmotion.in_motion_android.data.remote.dto.auth

data class UpdatePasswordDto(
    val oldPassword: String,
    val newPassword: String,
    val repeatPassword: String
)
