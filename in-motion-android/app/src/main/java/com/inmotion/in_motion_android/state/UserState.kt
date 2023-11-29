package com.inmotion.in_motion_android.state

import com.inmotion.in_motion_android.data.database.entity.UserInfo
import com.inmotion.in_motion_android.data.remote.dto.user.FullUserInfoDto

data class UserState(
    val user: UserInfo? = null,
    val fullUserInfo: FullUserInfoDto? = null
)
