package com.inmotion.in_motion_android.data.remote.dto.posts

import java.io.Serializable

data class GetPostResponseDto(
    val id: String,
    val description: String?,
    val title: String?,
    val author: PostAuthorDto,
    val tags: List<PostTagDto>,
    val videos: List<PostVideoDto>,
    val isLikedByUser: Boolean,
    val postReaction: PostReactionWithoutAuthorDto,
    val postCommentCount: Int,
    val postReactionsCount: Int,
    val createdAt: String
): Serializable
