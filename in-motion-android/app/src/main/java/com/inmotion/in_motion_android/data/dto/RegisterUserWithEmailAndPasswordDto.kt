package com.inmotion.in_motion_android.data.dto

data class RegisterUserWithEmailAndPasswordDto(
    val email: String,
    val password: String,
    val repeatPassword: String,
    val nickname: String
)
