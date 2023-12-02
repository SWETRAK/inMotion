package com.inmotion.in_motion_android.data.remote.api

import com.inmotion.in_motion_android.data.remote.dto.BooleanImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.auth.AddPasswordDto
import com.inmotion.in_motion_android.data.remote.dto.auth.AuthenticateWithGoogleProviderDto
import com.inmotion.in_motion_android.data.remote.dto.auth.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.remote.dto.auth.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.remote.dto.auth.SuccessfulRegistrationResponseDto
import com.inmotion.in_motion_android.data.remote.dto.auth.UpdateEmailDto
import com.inmotion.in_motion_android.data.remote.dto.auth.UpdateNicknameDto
import com.inmotion.in_motion_android.data.remote.dto.auth.UpdatePasswordDto
import com.inmotion.in_motion_android.data.remote.dto.auth.UserInfoDto
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.PUT

interface ImsAuthApi {
    @POST("/auth/api/email/login")
    suspend fun loginWithEmail(@Body loginUserWithEmailAndPasswordDto: LoginUserWithEmailAndPasswordDto): Response<ImsHttpMessage<UserInfoDto>>

    @POST("/auth/api/email/register")
    suspend fun register(@Body registerUserWithEmailAndPasswordDto: RegisterUserWithEmailAndPasswordDto): Response<ImsHttpMessage<SuccessfulRegistrationResponseDto>>

    @PUT("/auth/api/email/password/update")
    suspend fun updatePassword(
        @Header("Authorization") token: String,
        @Body updatePasswordDto: UpdatePasswordDto
    ): Response<BooleanImsHttpMessage>

    @PUT("/auth/api/email/password/add")
    suspend fun addPassword(
        @Header("Authorization") token: String,
        @Body addPasswordDto: AddPasswordDto
    ): Response<BooleanImsHttpMessage>

    @POST("/auth/api/google/login")
    suspend fun loginWithGoogle(@Body authenticateWithGoogleProviderDto: AuthenticateWithGoogleProviderDto): Response<ImsHttpMessage<UserInfoDto>>

    @POST("/auth/api/google/add")
    suspend fun addLoginWithGoogle(@Body authenticateWithGoogleProviderDto: AuthenticateWithGoogleProviderDto): Response<BooleanImsHttpMessage>

    @PUT("/auth/api/user/email")
    suspend fun updateEmail(
        @Header("Authorization") token: String,
        @Body updateEmailDto: UpdateEmailDto
    ): Response<ImsHttpMessage<UserInfoDto>>

    @PUT("/auth/api/user/nickname")
    suspend fun updateNickname(
        @Header("Authorization") token: String,
        @Body updateNicknameDto: UpdateNicknameDto
    ): Response<ImsHttpMessage<UserInfoDto>>

    @GET("/auth/api/user")
    suspend fun getUser(@Header("Authorization") token: String): Response<ImsHttpMessage<UserInfoDto>>
}