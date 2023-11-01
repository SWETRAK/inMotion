package com.inmotion.in_motion_android.data

import java.io.Serializable

data class FriendDto(
    val nickname: String,
    val lastActivity: String
): Serializable
