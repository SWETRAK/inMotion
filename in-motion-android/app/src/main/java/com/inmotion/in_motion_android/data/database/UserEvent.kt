package com.inmotion.in_motion_android.data.database

import com.inmotion.in_motion_android.data.database.entity.UserInfo

sealed interface UserEvent {
    data object SaveUser: UserEvent
    data object UpdateFullUserInfo: UserEvent
    data class SetId(val id: String): UserEvent
    data class SetNickname(val nickname: String): UserEvent
    data class SetEmail(val email: String): UserEvent
    data class SetToken(val token: String): UserEvent
    data class SetUser(val user: UserInfo): UserEvent
    data class DeleteUser(val user: UserInfo): UserEvent
}