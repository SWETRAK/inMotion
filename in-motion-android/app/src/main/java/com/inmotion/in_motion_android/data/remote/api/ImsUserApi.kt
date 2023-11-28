package com.inmotion.in_motion_android.data.remote.api

import com.inmotion.in_motion_android.data.remote.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.user.FullUserInfoDto
import com.inmotion.in_motion_android.data.remote.dto.user.UpdateUserBioDto
import com.inmotion.in_motion_android.data.remote.dto.user.UpdatedUserBioDto
import com.inmotion.in_motion_android.data.remote.dto.user.UserProfileVideoDto
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.PUT
import retrofit2.http.Path

interface ImsUserApi {

    @GET("/user/api/users/videos/byVideo/{videoId}")
    suspend fun getProfileVideoByVideoId(@Header("Authorization") token: String, @Path("videoId") videoId: String): Response<ImsHttpMessage<UserProfileVideoDto>>

    @GET("/user/api/users/videos/byUser/{userId}")
    suspend fun getProfileVideoByUserId(@Header("Authorization") token: String, @Path("userId") videoId: String): Response<ImsHttpMessage<UserProfileVideoDto>>

    @GET("/user/api/users/search/{nickname}")
    suspend fun getUserByNickname(@Header("Authorization") token: String, @Path("nickname") nickname: String): Response<ImsHttpMessage<FullUserInfoDto>>

    @GET("/users/api/users/{userId}")
    suspend fun getUserById(@Header("Authorization") token: String, @Path("userId") userId: String): Response<ImsHttpMessage<FullUserInfoDto>>

    @PUT("/users/api/users/update/bio")
    suspend fun updateLoggedInUserBio(@Header("Authorization") token: String, @Body bio: UpdateUserBioDto): Response<ImsHttpMessage<UpdatedUserBioDto>>
}