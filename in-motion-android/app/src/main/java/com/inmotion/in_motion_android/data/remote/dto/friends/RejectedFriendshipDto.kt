package com.inmotion.in_motion_android.data.remote.dto.friends

data class RejectedFriendshipDto(
    val id: String,
    val externalUserId: String,
    val externalUser: FriendInfoDto,
    val rejected: String
)
