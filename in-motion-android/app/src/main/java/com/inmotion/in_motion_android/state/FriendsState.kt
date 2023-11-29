package com.inmotion.in_motion_android.state

import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.data.database.entity.InvitedFriend
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend

data class FriendsState (
    val accepted: List<AcceptedFriend> = listOf(),
    val requested: List<RequestedFriend> = listOf(),
    val invited: List<InvitedFriend> = listOf()
)