package com.inmotion.in_motion_android.data.remote.api

import com.inmotion.in_motion_android.data.remote.dto.media.ProfileVideoUploadInfoDto
import okhttp3.RequestBody
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.Multipart
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Path

interface ImsMediaApi {

    @GET("/media/api/profile/video/mp4/{userId}")
    suspend fun getProfileVideoAsMp4ByUserId(
        @Header("Authorization") token: String,
        @Path("userId") userId: String
    ): Response<Array<Byte>>

    @GET("/media/api/profile/video/gif/{userId}")
    suspend fun getProfileVideoAsGifByUserId(
        @Header("Authorization") token: String,
        @Path("userId") userId: String
    ): Response<Array<Byte>>

    @Multipart
    @POST("/media/api/profile/video")
    suspend fun addProfileVideo(
        @Header("Authorization") token: String,
        @Part("mp4File") mp4File: RequestBody
    ): Response<ProfileVideoUploadInfoDto>

//    @GET("/media/api/post/{postId}")
//    suspend fun getPostVideos(@Header("Authorization") token: String, @Path("postId") postId: String): Response<PostVideosDto>
}