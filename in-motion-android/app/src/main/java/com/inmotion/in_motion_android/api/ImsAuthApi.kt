package com.inmotion.in_motion_android.api

import com.inmotion.in_motion_android.data.ImsHttpMessage
import com.inmotion.in_motion_android.data.LoginWithEmailDto
import com.inmotion.in_motion_android.data.RegisterDto
import com.inmotion.in_motion_android.data.LoggedInUserDto
import com.inmotion.in_motion_android.data.SuccessFullRegistrationDto
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.POST

interface ImsAuthApi {
    @POST("/auth/api/email/login")
    fun loginWithEmail(@Body loginWithEmailDto: LoginWithEmailDto): Call<ImsHttpMessage<LoggedInUserDto>>

    @POST("/auth/api/email/register")
    fun register(@Body registerDto: RegisterDto): Call<ImsHttpMessage<SuccessFullRegistrationDto>>
}