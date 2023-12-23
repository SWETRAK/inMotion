package com.inmotion.in_motion_android.adapter

import android.app.Activity
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.VideoView
import androidx.core.content.ContextCompat
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.dto.posts.GetPostResponseDto
import com.inmotion.in_motion_android.data.remote.dto.posts.reactions.CreatePostReactionDto
import com.inmotion.in_motion_android.databinding.PostRecyclerViewItemBinding
import com.inmotion.in_motion_android.state.UserViewModel
import com.inmotion.in_motion_android.util.DoubleClickListener
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.File
import java.io.FileOutputStream
import kotlin.io.encoding.Base64
import kotlin.io.encoding.ExperimentalEncodingApi

class PostsAdapter(
    private val postsList: List<GetPostResponseDto>,
    private val userViewModel: UserViewModel,
    private val activity: Activity
) :
    RecyclerView.Adapter<PostsAdapter.PostsViewHolder>() {

    inner class PostsViewHolder(private val itemBinding: PostRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        @OptIn(ExperimentalEncodingApi::class)
        fun bindItem(post: GetPostResponseDto) {
            itemBinding.tvUsername.text = post.author.nickname
            itemBinding.tvLocationAndTime.text = "${post.createdAt.substring(0, 19)}"

            CoroutineScope(Dispatchers.IO).launch {
                val response = ApiUtils.imsMediaApi.getPostVideos(
                    "Bearer ${userViewModel.user.value?.token}",
                    post.id
                )

                if (response.code() < 400) {
                    val postVideos = response.body()
                    val path = activity.filesDir

                    val backFile = File(path, "back_${post.id}.mp4")
                    val backStream = FileOutputStream(backFile)
                    postVideos!!
                    try {
                        backStream.write(
                            Base64.decode(
                                postVideos.backVideo as CharSequence,
                                0,
                                postVideos.backVideo.length
                            )
                        )
                    } finally {
                        backStream.close()
                    }

                    val frontFile = File(path, "front_${post.id}.mp4")
                    val frontStream = FileOutputStream(frontFile)
                    try {
                        frontStream.write(
                            Base64.decode(
                                postVideos.frontVideo as CharSequence,
                                0,
                                postVideos.frontVideo.length
                            )
                        )
                    } finally {
                        frontStream.close()
                    }

                    activity.runOnUiThread {
                        itemBinding.defaultBackPostVideo.setVideoPath(backFile.path)
                        itemBinding.defaultFrontPostVideo.setVideoPath(frontFile.path)

                        itemBinding.defaultBackPostVideo.setOnPreparedListener {
                            it.isLooping = true
                            it.seekTo(1)
                        }
                        itemBinding.defaultFrontPostVideo.setOnPreparedListener {
                            it.isLooping = true
                            it.seekTo(1)
                        }

                        itemBinding.defaultFrontPostVideo.setOnClickListener {
                            val bundle = Bundle()
                            bundle.putSerializable("POST", post)
                            bundle.putString("FRONT_VIDEO", frontFile.path)
                            bundle.putString("BACK_VIDEO", backFile.path)
                            it.findNavController()
                                .navigate(R.id.action_mainFragment_to_postDetailsFragment, bundle)
                        }

                        itemBinding.defaultBackPostVideo.setOnClickListener(object: DoubleClickListener() {
                            override fun onDoubleClick(v: View?) {
                                lovePost(post)
                            }
                        })

                        itemBinding.ivComment.setOnClickListener {
                            val bundle = Bundle()
                            bundle.putSerializable("POST", post)
                            bundle.putString("FRONT_VIDEO", frontFile.path)
                            bundle.putString("BACK_VIDEO", backFile.path)
                            it.findNavController()
                                .navigate(R.id.action_mainFragment_to_postDetailsFragment, bundle)
                        }
                    }

                }
            }

            itemBinding.btnLove.text = post.postReactionsCount.toString()
            if (post.isLikedByUser) {
                itemBinding.btnLove.background =
                    ContextCompat.getDrawable(activity, R.drawable.border_rounded_filled)
            } else {
                itemBinding.btnLove.setOnClickListener {
                    lovePost(post)
                }
            }
        }

        private fun lovePost(post: GetPostResponseDto) {
            CoroutineScope(Dispatchers.IO).launch {
                val reactionResponse = ApiUtils.imsPostsApi.createPostReaction(
                    "Bearer ${userViewModel.user.value?.token}",
                    CreatePostReactionDto(
                        post.id,
                        "U+2764"
                    )
                )
                if (reactionResponse.code() < 400) {
                    activity.runOnUiThread {
                        itemBinding.btnLove.background =
                            ContextCompat.getDrawable(activity, R.drawable.border_rounded_filled)
                    }
                    itemBinding.btnLove.text = (post.postReactionsCount + 1).toString()
                }
            }
        }

        fun startPlayback() {
            itemBinding.defaultBackPostVideo.start()
            itemBinding.defaultFrontPostVideo.start()
        }

        private fun pausePlayback() {
            itemBinding.defaultBackPostVideo.pause()
            itemBinding.defaultFrontPostVideo.pause()
        }

        fun stopPlayback() {
            itemBinding.defaultBackPostVideo.stopPlayback()
            itemBinding.defaultFrontPostVideo.stopPlayback()
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PostsViewHolder {
        return PostsViewHolder(
            PostRecyclerViewItemBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            )
        )
    }

    override fun getItemCount(): Int {
        return postsList.size
    }

    override fun onBindViewHolder(holder: PostsViewHolder, position: Int) {
        val post = postsList[position]
        holder.bindItem(post)
    }
}