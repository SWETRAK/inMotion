package com.inmotion.in_motion_android.state

import com.inmotion.in_motion_android.data.database.entity.UserInfo

data class UserState(
    val user: UserInfo? = null,
    val fullUserInfo: com.inmotion.in_motion_android.data.remote.dto.user.FullUserInfoDto? = null
)
