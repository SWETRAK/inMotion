package com.inmotion.in_motion_android.data.remote.api

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Header
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

//    @GET("/media/api/post/{postId}")
//    suspend fun getPostVideos(@Header("Authorization") token: String, @Path("postId") postId: String): Response<PostVideosDto>
}