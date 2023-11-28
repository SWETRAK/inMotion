package com.inmotion.in_motion_android.data.remote.api

import com.inmotion.in_motion_android.data.remote.dto.BooleanImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.friends.AcceptedFriendshipDto
import com.inmotion.in_motion_android.data.remote.dto.friends.InvitationFriendshipDto
import com.inmotion.in_motion_android.data.remote.dto.friends.RejectedFriendshipDto
import com.inmotion.in_motion_android.data.remote.dto.friends.RequestFriendshipDto
import retrofit2.Response
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path

interface ImsFriendsApi {

    @POST("/friends/api/friends/{externalUserIdString}")
    suspend fun addFriend(@Header("Authorization") token: String, @Path("externalUserIdString") externalUserId: String):
            Response<ImsHttpMessage<InvitationFriendshipDto>>

    @PUT("/friends/api/friends/accept/{externalUserIdString}")
    suspend fun acceptFriend(@Header("Authorization") token: String, @Path("externalUserIdString") externalUserId: String):
            Response<ImsHttpMessage<AcceptedFriendshipDto>>

    @PUT("/friends/api/friends/reject/{externalUserIdString}")
    suspend fun rejectFriend(@Header("Authorization") token: String, @Path("externalUserIdString") externalUserId: String):
            Response<ImsHttpMessage<RejectedFriendshipDto>>

    @DELETE("/friends/api/friends/revert/{friendshipIdString}")
    suspend fun revertFriend(@Header("Authorization") token: String, @Path("friendshipIdString") friendshipId: String):
            Response<BooleanImsHttpMessage>

    @DELETE("/friends/api/friends/unfriend/{friendshipIdString}")
    suspend fun unfriendFriend(@Header("Authorization") token: String, @Path("friendshipIdString") friendshipId: String):
            Response<ImsHttpMessage<RejectedFriendshipDto>>

    @GET("/friends/api/friends/lists/accepted")
    suspend fun getAcceptedFriends(@Header("Authorization") token: String): Response<ImsHttpMessage<List<AcceptedFriendshipDto>>>

    @GET("/friends/api/friends/lists/requested")
    suspend fun getRequestedFriends(@Header("Authorization") token: String): Response<ImsHttpMessage<List<RequestFriendshipDto>>>

    @GET("/friends/api/friends/lists/accepted")
    suspend fun getInvitedFriends(@Header("Authorization") token: String): Response<ImsHttpMessage<List<InvitationFriendshipDto>>>
}