package com.inmotion.in_motion_android.state

import androidx.lifecycle.ViewModel
import androidx.lifecycle.asLiveData
import androidx.lifecycle.viewModelScope
import com.inmotion.in_motion_android.data.database.dao.AcceptedFriendDao
import com.inmotion.in_motion_android.data.database.dao.InvitedFriendDao
import com.inmotion.in_motion_android.data.database.dao.RequestedFriendDao
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import kotlinx.coroutines.launch

class FriendsViewModel(
    private val acceptedFriendDao: AcceptedFriendDao,
    private val invitedFriendDao: InvitedFriendDao,
    private val requestedFriendDao: RequestedFriendDao,
    private val imsFriendsApi: ImsFriendsApi
) : ViewModel() {


    val acceptedFriends = acceptedFriendDao.getAll().asLiveData()
    val invitedFriends = invitedFriendDao.getAll().asLiveData()
    val requestedFriends = requestedFriendDao.getAll().asLiveData()


    fun onEvent(event: FriendEvent) {
        when (event) {
            is FriendEvent.SetAcceptedFriends -> {
                viewModelScope.launch {
                    acceptedFriendDao.deleteAll()
                    acceptedFriendDao.upsertAll(event.acceptedFriends)
                }
            }

            is FriendEvent.SetInvitedFriends -> {
                viewModelScope.launch {
                    invitedFriendDao.deleteAll()
                    invitedFriendDao.upsertAll(event.invitedFriends)
                }
            }

            is FriendEvent.SetRequestedFriends -> {
                viewModelScope.launch {
                    requestedFriendDao.deleteAll()
                    requestedFriendDao.upsertAll(event.requestedFriends)
                }
            }

            is FriendEvent.FetchAcceptedFriends -> {
                viewModelScope.launch {
                    val acceptedFriendDtos =
                        imsFriendsApi.getAcceptedFriends("Bearer ${event.token}").body()?.data
                    if (acceptedFriendDtos != null) {
                        val acceptedFriends = acceptedFriendDtos.map {
                            it.toAcceptedFriend()
                        }.toList()
                        onEvent(FriendEvent.SetAcceptedFriends(acceptedFriends = acceptedFriends))
                    } else {
                        onEvent(FriendEvent.SetAcceptedFriends(acceptedFriends = listOf()))
                    }
                }
            }

            is FriendEvent.FetchInvitedFriends -> {
                viewModelScope.launch {
                    val invitedFriendDtos =
                        imsFriendsApi.getInvitedFriends("Bearer ${event.token}").body()?.data
                    if (invitedFriendDtos != null) {
                        val invitedFriends = invitedFriendDtos.map {
                            it.toInvitedFriend()
                        }.toList()
                        onEvent(FriendEvent.SetInvitedFriends(invitedFriends = invitedFriends))
                    } else {
                        onEvent(FriendEvent.SetInvitedFriends(invitedFriends = listOf()))
                    }
                }
            }

            is FriendEvent.FetchRequestedFriends -> {
                viewModelScope.launch {
                    val body = imsFriendsApi.getRequestedFriends("Bearer ${event.token}").body()
                    val requestedFriendDtos = body?.data
                    if (requestedFriendDtos != null) {
                        val requestedFriends = requestedFriendDtos.map {
                            it.toRequestedFriend()
                        }.toList()
                        onEvent(FriendEvent.SetRequestedFriends(requestedFriends = requestedFriends))
                    } else {
                        onEvent(FriendEvent.SetRequestedFriends(requestedFriends = listOf()))
                    }
                }
            }
        }
    }
}