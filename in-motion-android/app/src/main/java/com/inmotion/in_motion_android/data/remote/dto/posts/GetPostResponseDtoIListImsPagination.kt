package com.inmotion.in_motion_android.data.remote.dto.posts

data class GetPostResponseDtoIListImsPagination(
    val pageNumber: Int,
    val pageSize: Int,
    val totalCount: Int,
    val data: List<GetPostResponseDto>?
)
