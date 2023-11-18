package com.inmotion.in_motion_android.data.dto.user

import java.io.Serializable

data class FullUserInfoDto(
    val id: String,
    val nickname: String,
    val bio: String?,
    val userProfileVideo: UserProfileVideoDto?
) : Serializable
