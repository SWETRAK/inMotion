package com.inmotion.in_motion_android.data.remote.dto.friends

data class RequestFriendshipDto(
    val id: String,
    val externalUserId: String,
    val externalUser: FriendInfoDto,
    val requested: String
)
