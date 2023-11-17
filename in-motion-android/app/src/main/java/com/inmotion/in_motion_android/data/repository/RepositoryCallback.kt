package com.inmotion.in_motion_android.data.repository

interface RepositoryCallback<T> {
    fun onResponse(response: T)
    fun onFailure()
}