package com.inmotion.in_motion_android.data.dto.user

data class FullUserInfoDto(
    val id: String,
    val nickname: String,
    val bio: String,
    val userProfileVideo: UserProfileVideoDto
)
