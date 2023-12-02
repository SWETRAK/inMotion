package com.inmotion.in_motion_android.data.remote

import android.net.Uri
import java.io.Serializable

data class PostDto(
    val username: String,
    val location: String,
    val time: String,
    val frontVideoPath: Uri,
    val backVideoPath: Uri,
    val loveCount: Int,
    val laughCount: Int,
    val wowCount: Int,
    val cryingCount: Int
) : Serializable
