package com.inmotion.in_motion_android.data.remote

import java.io.Serializable

data class UserDto(
    val nickname: String,
    val bio: String
) : Serializable