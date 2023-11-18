package com.inmotion.in_motion_android.data.repository

import com.inmotion.in_motion_android.data.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.dto.user.FullUserInfoDto
import com.inmotion.in_motion_android.data.remote.ImsUserApi
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class UserRepository {

    private val imsUserApi: ImsUserApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsUserApi::class.java)

    fun getFullUserInfoById(id: String, token: String, callback: RepositoryCallback<FullUserInfoDto>) {
        imsUserApi.getUserById(token, id).enqueue(
            object: Callback<ImsHttpMessage<FullUserInfoDto>> {
                override fun onResponse(
                    call: Call<ImsHttpMessage<FullUserInfoDto>>,
                    response: Response<ImsHttpMessage<FullUserInfoDto>>
                ) {
                    if(response.code() < 400){
                        callback.onResponse(response.body()!!.data)
                    }
                }

                override fun onFailure(call: Call<ImsHttpMessage<FullUserInfoDto>>, t: Throwable) {
                    callback.onFailure()
                }
            }
        )
    }
}