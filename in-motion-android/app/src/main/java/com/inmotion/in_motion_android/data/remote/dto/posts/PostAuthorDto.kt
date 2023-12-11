package com.inmotion.in_motion_android.data.remote.dto.posts

import java.io.Serializable

data class PostAuthorDto(
    val id: String,
    val nickname: String,
    val frontVideo: String?
): Serializable
