package com.inmotion.in_motion_android.data.remote

import com.inmotion.in_motion_android.data.dto.BooleanImsHttpMessage
import com.inmotion.in_motion_android.data.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.dto.auth.AddPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.AuthenticateWithGoogleProviderDto
import com.inmotion.in_motion_android.data.dto.auth.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.SuccessfullRegistrationResponseDto
import com.inmotion.in_motion_android.data.dto.auth.UpdateEmailDto
import com.inmotion.in_motion_android.data.dto.auth.UpdateNicknameDto
import com.inmotion.in_motion_android.data.dto.auth.UpdatePasswordDto
import com.inmotion.in_motion_android.data.dto.auth.UserInfoDto
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.HeaderMap
import retrofit2.http.POST
import retrofit2.http.PUT

interface ImsAuthApi {
    @POST("/auth/api/email/login")
    fun loginWithEmail(@Body loginUserWithEmailAndPasswordDto: LoginUserWithEmailAndPasswordDto): Call<ImsHttpMessage<UserInfoDto>>

    @POST("/auth/api/email/register")
    fun register(@Body registerUserWithEmailAndPasswordDto: RegisterUserWithEmailAndPasswordDto): Call<ImsHttpMessage<SuccessfullRegistrationResponseDto>>

    @PUT("/auth/api/email/password/update")
    fun updatePassword(@Body updatePasswordDto: UpdatePasswordDto, @HeaderMap headers: Map<String, String>): Call<BooleanImsHttpMessage>

    @PUT("/auth/api/email/password/add")
    fun addPassword(@Body addPasswordDto: AddPasswordDto): Call<BooleanImsHttpMessage>

    @POST("/auth/api/google/login")
    fun loginWithGoogle(@Body authenticateWithGoogleProviderDto: AuthenticateWithGoogleProviderDto): Call<ImsHttpMessage<UserInfoDto>>

    @POST("/auth/api/google/add")
    fun addLoginWithGoogle(@Body authenticateWithGoogleProviderDto: AuthenticateWithGoogleProviderDto): Call<BooleanImsHttpMessage>

    @PUT("/auth/api/user/email")
    fun updateEmail(@Body updateEmailDto: UpdateEmailDto, @HeaderMap headers: Map<String, String>): Call<ImsHttpMessage<UserInfoDto>>

    @PUT("/auth/api/user/nickname")
    fun updateNickname(@Body updateNicknameDto: UpdateNicknameDto, @HeaderMap headers: Map<String, String>): Call<ImsHttpMessage<UserInfoDto>>

    @GET("/auth/api/user")
    fun getUser(@HeaderMap headers: Map<String, String>): Call<ImsHttpMessage<UserInfoDto>>
}