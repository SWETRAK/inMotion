package com.inmotion.in_motion_android.data.remote.dto.friends

data class FriendInfoDto(
    val id: String,
    val nickname: String,
    val bio: String,
    val frontVideo: FriendProfileVideoDto
)
