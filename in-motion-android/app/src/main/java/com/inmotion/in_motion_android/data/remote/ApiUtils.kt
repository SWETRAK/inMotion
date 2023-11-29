package com.inmotion.in_motion_android.data.remote

import com.inmotion.in_motion_android.data.remote.api.ImsAuthApi
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.data.remote.api.ImsUserApi
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object ApiUtils {

    val imsUserApi = Retrofit.Builder()
    .baseUrl(ApiConstants.BASE_URL)
    .addConverterFactory(GsonConverterFactory.create())
    .build()
    .create(ImsUserApi::class.java)

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
}