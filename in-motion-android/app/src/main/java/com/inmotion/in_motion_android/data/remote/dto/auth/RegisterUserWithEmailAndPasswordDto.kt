package com.inmotion.in_motion_android.data.remote.dto.auth

data class RegisterUserWithEmailAndPasswordDto(
    val email: String,
    val password: String,
    val repeatPassword: String,
    val nickname: String
)
