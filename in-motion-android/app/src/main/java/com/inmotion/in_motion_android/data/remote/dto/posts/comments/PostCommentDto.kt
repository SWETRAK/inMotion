package com.inmotion.in_motion_android.data.remote.dto.posts.comments

import com.inmotion.in_motion_android.data.remote.dto.posts.PostAuthorDto

data class PostCommentDto(
    val id: String,
    val author: PostAuthorDto,
    val content: String,
    val postId: String,
    val postCommentReactionCount: String,
    val createdAt: String
)
