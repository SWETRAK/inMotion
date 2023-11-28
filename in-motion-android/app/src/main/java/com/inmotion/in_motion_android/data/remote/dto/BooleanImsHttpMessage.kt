package com.inmotion.in_motion_android.data.remote.dto

data class BooleanImsHttpMessage(
    val status: Int,
    val serverResponseTime: String,
    val serverRequestTime: String,
    val data: Boolean
)
