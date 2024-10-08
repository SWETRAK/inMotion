package com.inmotion.in_motion_android.data.remote

import com.inmotion.in_motion_android.data.remote.api.ImsAuthApi
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.data.remote.api.ImsMediaApi
import com.inmotion.in_motion_android.data.remote.api.ImsPostsApi
import com.inmotion.in_motion_android.data.remote.api.ImsUsersApi
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object ApiUtils {

    val imsUsersApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsUsersApi::class.java)

    val imsAuthApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsAuthApi::class.java)

    val imsFriendsApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsFriendsApi::class.java)

    val imsMediaApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsMediaApi::class.java)

    val imsPostsApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsPostsApi::class.java)
}