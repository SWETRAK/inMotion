package com.inmotion.in_motion_android.state

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.asFlow
import androidx.lifecycle.asLiveData
import androidx.lifecycle.viewModelScope
import com.inmotion.in_motion_android.data.database.dao.UserInfoDao
import com.inmotion.in_motion_android.data.database.event.UserEvent
import com.inmotion.in_motion_android.data.remote.api.ImsUsersApi
import com.inmotion.in_motion_android.data.remote.dto.user.FullUserInfoDto
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class UserViewModel(
    private val dao: UserInfoDao,
    private val userApi: ImsUsersApi
) : ViewModel() {

    val user = dao.get().asLiveData()
    val fullUserInfo = MutableLiveData<FullUserInfoDto?>(null)


    init {
        updateFullUserInfo()
    }

    fun onEvent(event: UserEvent) {
        when (event) {
            is UserEvent.SaveUser -> {

                if (event.user.id.isBlank() || event.user.nickname.isBlank() || event.user.email.isBlank() || event.user.token.isBlank()) {
                    return
                }

                viewModelScope.launch {
                    dao.upsertUser(event.user)
                }
            }

            is UserEvent.DeleteUser -> {
                viewModelScope.launch(Dispatchers.Main) {
                    user.value?.let { dao.delete(it) }
                    dao.deleteAll()
                }
            }

            is UserEvent.UpdateFullUserInfo -> {
                updateFullUserInfo()
            }
        }
    }

    private fun updateFullUserInfo() {
        viewModelScope.launch {
            if (user.value != null) {
                user.asFlow().collect {
                    if (it != null) {
                        val response = userApi.getUserById("Bearer ${it.token}", it.id)
                        if (response.code() < 400)
                            fullUserInfo.value = response.body()!!.data
                    }
                }
            }
        }
    }
}