package com.inmotion.in_motion_android.data.remote.dto

data class ImsHttpMessage<T>(
    val status: Int,
    val serverResponseTime: String,
    val serverRequestTime: String,
    val data: T
)
