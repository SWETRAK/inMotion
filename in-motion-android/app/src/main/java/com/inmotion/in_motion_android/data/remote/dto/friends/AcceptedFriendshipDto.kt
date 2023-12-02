package com.inmotion.in_motion_android.data.remote.dto.friends

import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend

data class AcceptedFriendshipDto(
    val id: String,
    val externalUserId: String,
    val externalUser: FriendInfoDto,
    val friendsSince: String
) {
    fun toAcceptedFriend(): AcceptedFriend {
        return AcceptedFriend(
            externalUserId,
            id,
            externalUser.nickname,
            externalUser.bio,
            friendsSince
        )
    }
}
