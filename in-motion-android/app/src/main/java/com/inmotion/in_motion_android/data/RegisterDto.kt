package com.inmotion.in_motion_android.data

data class RegisterDto(
    val email: String,
    val password: String,
    val repeatPassword: String,
    val nickname: String
)
