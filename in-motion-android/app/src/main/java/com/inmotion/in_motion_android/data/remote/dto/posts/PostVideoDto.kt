package com.inmotion.in_motion_android.data.remote.dto.posts

import java.io.Serializable

data class PostVideoDto (
    val id: String,
    val filename: String,
    val bucketName: String,
    val bucketLocation: String,
    val contentType: String,
    val videoType: String
): Serializable
