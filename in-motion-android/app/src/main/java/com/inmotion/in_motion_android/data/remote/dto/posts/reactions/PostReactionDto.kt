package com.inmotion.in_motion_android.data.remote.dto.posts.reactions

import com.inmotion.in_motion_android.data.remote.dto.posts.PostAuthorDto

data class PostReactionDto(
    val id: String,
    val author: PostAuthorDto,
    val emoji: String,
    val postId: String,
    val createdAt: String
)
