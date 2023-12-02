package com.inmotion.in_motion_android.data.database.event

import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.data.database.entity.InvitedFriend
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend

sealed interface FriendEvent {
    // SET
    data class SetAcceptedFriends(val acceptedFriends: List<AcceptedFriend>) : FriendEvent
    data class SetInvitedFriends(val invitedFriends: List<InvitedFriend>) : FriendEvent
    data class SetRequestedFriends(val requestedFriends: List<RequestedFriend>) : FriendEvent

    // FETCH
    class FetchAcceptedFriends(val token: String) : FriendEvent
    class FetchInvitedFriends(val token: String) : FriendEvent
    class FetchRequestedFriends(val token: String) : FriendEvent
}