package com.inmotion.in_motion_android.data

import java.io.Serializable

data class FriendRequestDto(
    val nickname: String,
    val requestDate: String
): Serializable
