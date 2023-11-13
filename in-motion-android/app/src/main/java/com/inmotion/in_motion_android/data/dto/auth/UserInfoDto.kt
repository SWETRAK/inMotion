package com.inmotion.in_motion_android.data.dto.auth

import com.inmotion.in_motion_android.database.entity.UserInfo

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
