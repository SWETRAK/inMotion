package com.inmotion.in_motion_android.data.remote.api

import com.inmotion.in_motion_android.data.remote.dto.BooleanImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.remote.dto.posts.CreatePostRequestDto
import com.inmotion.in_motion_android.data.remote.dto.posts.CreatePostResponseDto
import com.inmotion.in_motion_android.data.remote.dto.posts.EditPostRequestDto
import com.inmotion.in_motion_android.data.remote.dto.posts.GetPostResponseDto
import com.inmotion.in_motion_android.data.remote.dto.posts.GetPostResponseDtoIListImsPagination
import com.inmotion.in_motion_android.data.remote.dto.posts.ImsPaginationRequestDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.CreatePostCommentDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.PostCommentDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.UpdatePostCommentDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.reactions.EditPostCommentReactionDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.reactions.PostCommentReactionDto
import com.inmotion.in_motion_android.data.remote.dto.posts.reactions.CreatePostReactionDto
import com.inmotion.in_motion_android.data.remote.dto.posts.reactions.EditPostReactionDto
import com.inmotion.in_motion_android.data.remote.dto.posts.reactions.PostReactionDto
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path

interface ImsPostsApi {

    @GET("/posts/api/posts")
    suspend fun getUserPost(@Header("Authorization") token: String): Response<ImsHttpMessage<GetPostResponseDto>>

    @POST("/posts/api/posts")
    suspend fun createPost(
        @Header("Authorization") token: String,
        @Body createPostRequestDto: CreatePostRequestDto
    ): Response<ImsHttpMessage<CreatePostResponseDto>>

    @GET("/posts/api/posts/{authorId}")
    suspend fun getPostByAuthorId(
        @Header("Authorization") token: String,
        @Path("authorId") authorId: String
    ): Response<ImsHttpMessage<GetPostResponseDto>>

    @GET("/posts/api/posts/public")
    suspend fun getPublicPostsPaginated(
        @Header("Authorization") token: String,
        @Body paginationRequestDto: ImsPaginationRequestDto
    ): Response<ImsHttpMessage<GetPostResponseDtoIListImsPagination>>

    @GET("/posts/api/posts/friends")
    suspend fun getFriendPosts(@Header("Authorization") token: String): Response<ImsHttpMessage<List<GetPostResponseDto>>>

    @PUT("/posts/api/posts/{postId}")
    suspend fun editPost(
        @Header("Authorization") token: String,
        @Path("postId") postId: String,
        @Body editPostRequestDto: EditPostRequestDto
    ): Response<ImsHttpMessage<GetPostResponseDto>>

    @GET("/posts/api/posts/comments/{postId}")
    suspend fun getCommentsForPost(
        @Header("Authorization") token: String,
        @Path("postId") postId: String
    ): Response<ImsHttpMessage<List<PostCommentDto>>>

    @POST("/posts/api/posts/comments")
    suspend fun createPostComment(
        @Header("Authorization") token: String,
        @Body createPostCommentDto: CreatePostCommentDto
    ): Response<ImsHttpMessage<PostCommentDto>>

    @PUT("/posts/api/posts/comments/{commentId}")
    suspend fun updatePostComment(
        @Header("Authorization") token: String,
        @Path("commentId") commentId: String,
        @Body updatePostCommentDto: UpdatePostCommentDto
    ): Response<ImsHttpMessage<PostCommentDto>>

    @DELETE("/posts/api/posts/comments/{commentId}")
    suspend fun deletePostComment(
        @Header("Authorization") token: String,
        @Path("commentId") commentId: String
    ): BooleanImsHttpMessage

    @GET("/posts/api/comments/reactions/{postCommentId}")
    suspend fun getPostCommentReactions(
        @Header("Authorization") token: String,
        @Path("postCommentId") postCommentId: String
    ): ImsHttpMessage<List<PostCommentReactionDto>>

    @POST("/posts/api/comments/reactions")
    suspend fun createPostCommentReaction(
        @Header("Authorization") token: String,
        @Body createPostReactionDto: CreatePostReactionDto
    ): Response<ImsHttpMessage<PostCommentReactionDto>>

    @PUT("/posts/api/comments/reactions/{postCommentReactionId}")
    suspend fun editPostCommentReaction(
        @Header("Authorization") token: String,
        @Path("postCommentReactionId") postCommentReactionId: String,
        @Body editPostCommentReactionDto: EditPostCommentReactionDto
    ): Response<ImsHttpMessage<PostCommentReactionDto>>

    @DELETE("/posts/api/posts/comments/reactions/{postCommentReactionId}")
    suspend fun deletePostCommentReaction(
        @Header("Authorization") token: String,
        @Path("postCommentReactionId") postCommentReactionId: String
    ): Response<BooleanImsHttpMessage>

    @GET("/posts/api/posts/reactions/{postId}")
    suspend fun getReactionsForPost(
        @Header("Authorization") token: String,
        @Path("postId") postId: String
    ): Response<ImsHttpMessage<List<PostReactionDto>>>

    @POST("/posts/api/posts/reactions")
    suspend fun createPostReaction(
        @Header("Authorization") token: String,
        @Body createPostReactionDto: CreatePostReactionDto
    ): Response<ImsHttpMessage<PostReactionDto>>

    @PUT("/posts/api/posts/reactions/{reactionId}")
    suspend fun editPostReaction(
        @Header("Authorization") token: String,
        @Path("reactionId") reactionId: String,
        @Body editPostReactionDto: EditPostReactionDto
    ): Response<ImsHttpMessage<PostReactionDto>>

    @DELETE("/posts/api/posts/reactions/{reactionId}")
    suspend fun deletePostReaction(
        @Header("Authorization") token: String,
        @Path("reactionId") reactionId: String
    ): Response<BooleanImsHttpMessage>
}