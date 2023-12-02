package com.inmotion.in_motion_android.data.remote.dto.auth

import com.inmotion.in_motion_android.data.database.entity.UserInfo

data class UserInfoDto(
    val id: String,
    val email: String,
    val nickname: String,
    val token: String
) {
    fun toUserInfo(): UserInfo {
        return UserInfo(id, email, nickname, token)
    }
}
