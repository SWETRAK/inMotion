package com.inmotion.in_motion_android

import android.net.Uri

data class PostDto(
    val username: String,
    val location: String,
    val time: String,
    val frontVideoPath: Uri,
    val backVideoPath: Uri,
    val likeCount: Int
)
