package com.inmotion.in_motion_android

import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.VideoView
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.PostDto
import com.inmotion.in_motion_android.databinding.PostRecyclerViewItemBinding

class PostsAdapter(private val postsList: List<PostDto>) :
    RecyclerView.Adapter<PostsAdapter.PostsViewHolder>() {

    inner class PostsViewHolder(private val itemBinding: PostRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(post: PostDto) {
            this.layoutPosition
            itemBinding.tvUsername.text = post.username
            itemBinding.tvLocationAndTime.text = "${post.location}, ${post.time}"
            itemBinding.defaultBackPostVideo.setVideoURI(post.backVideoPath)
            itemBinding.defaultFrontPostVideo.setVideoURI(post.frontVideoPath)
            itemBinding.tvLikeCount.text = post.likeCount.toString()

            itemBinding.defaultBackPostVideo.setOnPreparedListener {
                it.isLooping = true
                it.seekTo(1)
            }


            itemBinding.defaultFrontPostVideo.setOnPreparedListener {
                it.isLooping = true
                it.seekTo(1)
            }

            itemBinding.defaultBackPostVideo.setOnClickListener {
                if ((it as VideoView).isPlaying) {
                    pausePlayback()
                } else {
                    startPlayback()
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