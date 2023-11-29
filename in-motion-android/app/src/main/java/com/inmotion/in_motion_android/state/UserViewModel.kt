package com.inmotion.in_motion_android.state

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.inmotion.in_motion_android.data.database.event.UserEvent
import com.inmotion.in_motion_android.data.database.dao.UserInfoDao
import com.inmotion.in_motion_android.data.database.entity.UserInfo
import com.inmotion.in_motion_android.data.remote.api.ImsUserApi
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.combine
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class UserViewModel(
    private val dao: UserInfoDao,
    private val userApi: ImsUserApi
) : ViewModel() {
    private val _user = dao.get().stateIn(viewModelScope, SharingStarted.Eagerly, null)
    private val _state = MutableStateFlow(UserState())

    val state = combine(_state, _user) { state, user ->
        state.copy(
            user = user
        )
    }.stateIn(viewModelScope, SharingStarted.Eagerly, UserState())

    fun getBearerToken(): String {
        return "Bearer ${_user.value!!.token}"
    }

    fun onEvent(event: UserEvent) {
        when (event) {
            is UserEvent.SaveUser -> {
                val id = state.value.user!!.id
                val nickname = state.value.user!!.nickname
                val email = state.value.user!!.email
                val token = state.value.user!!.token

                if (id.isBlank() || nickname.isBlank() || email.isBlank() || token.isBlank()) {
                    return
                }

                val user = UserInfo(
                    id = id,
                    nickname = nickname,
                    email = email,
                    token = token
                )

                viewModelScope.launch {
                    dao.upsertUser(user)
                }

                _state.update {
                    it.copy(
                        user = user
                    )
                }
            }

            is UserEvent.DeleteUser -> {
                viewModelScope.launch {
                    dao.deleteAll()
                }
            }

            is UserEvent.SetEmail -> {
                _state.update {
                    it.copy(
                        user = it.user?.copy(email = event.email)
                    )
                }
            }

            is UserEvent.SetNickname -> {
                _state.update {
                    it.copy(
                        user = it.user?.copy(nickname = event.nickname)
                    )
                }
            }

            is UserEvent.SetToken -> {
                _state.update {
                    it.copy(
                        user = it.user?.copy(token = event.token)
                    )
                }
            }

            UserEvent.UpdateFullUserInfo -> {
                viewModelScope.launch {
                    val response =
                        userApi.getUserById(getBearerToken(), state.value.user!!.id)
                    _state.update {
                        it.copy(
                            fullUserInfo = response.body()!!.data
                        )
                    }
                }
            }

            is UserEvent.SetId -> {
                _state.update {
                    it.copy(
                        user = it.user?.copy(id = event.id)
                    )
                }
            }

            is UserEvent.SetUser -> {
                viewModelScope.launch {
                    dao.upsertUser(event.user)
                }
                _state.update {
                    it.copy(
                        user = event.user
                    )
                }
            }
        }
    }
}