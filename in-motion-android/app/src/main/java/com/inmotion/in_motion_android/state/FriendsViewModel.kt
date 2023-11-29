package com.inmotion.in_motion_android.state

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.inmotion.in_motion_android.data.database.dao.AcceptedFriendDao
import com.inmotion.in_motion_android.data.database.dao.InvitedFriendDao
import com.inmotion.in_motion_android.data.database.dao.RequestedFriendDao
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.combine
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch

class FriendsViewModel(
    private val acceptedFriendDao: AcceptedFriendDao,
    private val invitedFriendDao: InvitedFriendDao,
    private val requestedFriendDao: RequestedFriendDao,
    private val imsFriendsApi: ImsFriendsApi
    ) : ViewModel() {
    private val _acceptedFriends =
        acceptedFriendDao.getAll().stateIn(viewModelScope, SharingStarted.Eagerly, listOf())
    private val _invitedFriends =
        invitedFriendDao.getAll().stateIn(viewModelScope, SharingStarted.Eagerly, listOf())
    private val _requestedFriends =
        requestedFriendDao.getAll().stateIn(viewModelScope, SharingStarted.Eagerly, listOf())
    private val _state = MutableStateFlow(FriendsState())

    val state = combine(
        _state,
        _acceptedFriends,
        _invitedFriends,
        _requestedFriends
    ) { state, accepted, invited, requested ->
        state.copy(
            accepted = accepted,
            invited = invited,
            requested = requested
        )
    }.stateIn(viewModelScope, SharingStarted.Eagerly, FriendsState())


    fun onEvent(event: FriendEvent) {
        when(event) {
            is FriendEvent.AddAcceptedFriend -> {
                viewModelScope.launch {
                    acceptedFriendDao.upsert(event.acceptedFriend)
                }
            }
            is FriendEvent.AddInvitedFriend -> {
                viewModelScope.launch {
                    invitedFriendDao.upsert(event.invitedFriend)
                }
            }
            is FriendEvent.AddRequestedFriend -> {
                viewModelScope.launch {
                    requestedFriendDao.upsert(event.requestedFriend)
                }
            }
            is FriendEvent.DeleteAcceptedFriend -> {
                viewModelScope.launch {
                    acceptedFriendDao.delete(event.acceptedFriend)
                }
            }
            is FriendEvent.DeleteInvitedFriend -> {
                viewModelScope.launch {
                    invitedFriendDao.delete(event.invitedFriend)
                }
            }
            is FriendEvent.DeleteRequestedFriend -> {
                viewModelScope.launch {
                    requestedFriendDao.delete(event.requestedFriend)
                }
            }
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
                    val acceptedFriendDtos = imsFriendsApi.getAcceptedFriends(event.token).body()?.data
                    if(acceptedFriendDtos != null) {
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
                    val invitedFriendDtos = imsFriendsApi.getInvitedFriends(event.token).body()?.data
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
                    val body = imsFriendsApi.getRequestedFriends(event.token).body()
                    val requestedFriendDtos = body?.data
                    if(requestedFriendDtos != null) {
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