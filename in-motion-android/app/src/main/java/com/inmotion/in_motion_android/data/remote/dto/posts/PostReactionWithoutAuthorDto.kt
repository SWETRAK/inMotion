package com.inmotion.in_motion_android.data.remote.dto.posts

import java.io.Serializable

data class PostReactionWithoutAuthorDto(
    val id: String,
    val authorId: String,
    val emoji: String,
    val createdAt: String
): Serializable
