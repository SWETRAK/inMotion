package com.inmotion.in_motion_android.data.remote

import com.inmotion.in_motion_android.data.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.dto.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.UserInfoDto
import com.inmotion.in_motion_android.data.dto.SuccessfullRegistrationResponseDto
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.POST

interface ImsAuthApi {
    @POST("/auth/api/email/login")
    fun loginWithEmail(@Body loginUserWithEmailAndPasswordDto: LoginUserWithEmailAndPasswordDto): Call<ImsHttpMessage<UserInfoDto>>

    @POST("/auth/api/email/register")
    fun register(@Body registerUserWithEmailAndPasswordDto: RegisterUserWithEmailAndPasswordDto): Call<ImsHttpMessage<SuccessfullRegistrationResponseDto>>
}