package com.inmotion.in_motion_android.data.remote.dto.friends

import com.inmotion.in_motion_android.data.database.entity.InvitedFriend

data class InvitationFriendshipDto(
    val id: String,
    val externalUserId: String,
    val externalUser: FriendInfoDto,
    val invited: String
) {
    fun toInvitedFriend(): InvitedFriend {
        return InvitedFriend(
            externalUserId,
            id,
            externalUser.nickname,
            externalUser.bio,
            invited
        )
    }
}
