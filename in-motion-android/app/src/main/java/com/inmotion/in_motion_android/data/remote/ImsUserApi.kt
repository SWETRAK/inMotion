package com.inmotion.in_motion_android.data.remote

import com.inmotion.in_motion_android.data.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.dto.user.FullUserInfoDto
import com.inmotion.in_motion_android.data.dto.user.UpdateUserBioDto
import com.inmotion.in_motion_android.data.dto.user.UpdatedUserBioDto
import com.inmotion.in_motion_android.data.dto.user.UserProfileVideoDto
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.PUT
import retrofit2.http.Path

interface ImsUserApi {

    @GET("/user/api/users/videos/byVideo/{videoId}")
    fun getProfileVideoByVideoId(@Path("videoId") videoId: String): Call<ImsHttpMessage<UserProfileVideoDto>>

    @GET("/user/api/users/videos/byUser/{userId}")
    fun getProfileVideoByUserId(@Path("userId") videoId: String): Call<ImsHttpMessage<UserProfileVideoDto>>

    @GET("/user/api/users/search/{nickname}")
    fun getUserByNickname(@Path("nickname") nickname: String): Call<ImsHttpMessage<FullUserInfoDto>>

    @GET("/users/api/users/{userId}")
    fun getUserById(@Header("Authorization") token: String, @Path("userId") userId: String): Call<ImsHttpMessage<FullUserInfoDto>>

    @PUT("/api/users/update/bio")
    fun updateLoggedInUserBio(@Body bio: UpdateUserBioDto): Call<ImsHttpMessage<UpdatedUserBioDto>>
}