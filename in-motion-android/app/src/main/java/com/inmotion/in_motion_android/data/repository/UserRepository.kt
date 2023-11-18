package com.inmotion.in_motion_android.data.repository

import com.inmotion.in_motion_android.data.remote.ImsUserApi
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class UserRepository {

    private val imsUserApi: ImsUserApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsUserApi::class.java)
}