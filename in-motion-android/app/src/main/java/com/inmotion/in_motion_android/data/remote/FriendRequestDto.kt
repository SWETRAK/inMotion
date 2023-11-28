package com.inmotion.in_motion_android.data.remote

import java.io.Serializable

data class FriendRequestDto(
    val nickname: String,
    val requestDate: String
): Serializable
