package com.inmotion.in_motion_android.data

data class LoggedInUserDto(
    val id: String,
    val email: String,
    val nickname: String,
    val token: String
)
