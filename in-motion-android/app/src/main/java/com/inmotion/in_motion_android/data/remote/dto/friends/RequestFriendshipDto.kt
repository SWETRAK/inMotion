package com.inmotion.in_motion_android.data.remote.dto.friends

import com.inmotion.in_motion_android.data.database.entity.RequestedFriend

data class RequestFriendshipDto(
    val id: String,
    val externalUserId: String,
    val externalUser: FriendInfoDto,
    val requested: String
) {
    fun toRequestedFriend(): RequestedFriend {
        return RequestedFriend(
            externalUserId,
            id,
            externalUser.nickname,
            externalUser.bio,
            requested
        )
    }
}
