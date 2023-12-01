package com.inmotion.in_motion_android.data.database.event

import com.inmotion.in_motion_android.data.database.entity.UserInfo

sealed interface UserEvent {
    data object UpdateFullUserInfo: UserEvent
    data class SaveUser(val user: UserInfo): UserEvent
    data object DeleteUser: UserEvent
}