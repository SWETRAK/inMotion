package com.inmotion.in_motion_android.data.remote.dto.friends

data class AcceptedFriendshipDto(
    val id: String,
    val externalUserId: String,
    val externalUser: FriendInfoDto,
    val friendsSince: String
)
