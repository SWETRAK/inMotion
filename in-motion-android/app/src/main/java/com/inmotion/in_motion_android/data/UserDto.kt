package com.inmotion.in_motion_android.data

import java.io.Serializable

data class UserDto(
    val nickname: String,
    val bio: String
) : Serializable