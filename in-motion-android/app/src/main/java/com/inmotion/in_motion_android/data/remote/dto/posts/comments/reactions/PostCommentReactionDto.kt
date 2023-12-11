package com.inmotion.in_motion_android.data.remote.dto.posts.comments.reactions

import com.inmotion.in_motion_android.data.remote.dto.posts.PostAuthorDto

data class PostCommentReactionDto(
    val id: String,
    val author: PostAuthorDto,
    val emoji: String,
    val postCommentId: String,
    val createdAt: String
)
