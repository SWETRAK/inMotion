package com.inmotion.in_motion_android.data

import java.util.Date

data class ImsHttpMessage<T>(
    val status: Int,
    val serverResponseTime: String,
    val serverRequestTime: String,
    val data: T
)
