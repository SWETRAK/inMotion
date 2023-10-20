package com.inmotion.in_motion_android

import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.VideoView
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.databinding.PostRecyclerViewItemBinding

class PostsAdapter(private val postsList: List<PostDto>): RecyclerView.Adapter<PostsAdapter.PostsViewHolder>() {

    inner class PostsViewHolder(private val itemBinding: PostRecyclerViewItemBinding): RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(post: PostDto) {
            itemBinding.tvUsername.text = post.username
            itemBinding.tvLocationAndTime.text = "${post.location}, ${post.time}"
            itemBinding.defaultBackPostVideo.setVideoURI(post.BackVideoPath)
            itemBinding.defaultFrontPostVideo.setVideoURI(post.FrontVideoPath)
            itemBinding.tvLikeCount.text = post.likeCount.toString()

            itemBinding.defaultBackPostVideo.setOnPreparedListener {
             it.isLooping = true
             it.start()
            }

            itemBinding.defaultBackPostVideo.setOnClickListener {
                val videoView = it as VideoView
                if(videoView.isPlaying){
                    videoView.pause()
                } else {
                    videoView.resume()
                }
            }

            itemBinding.defaultFrontPostVideo.setOnPreparedListener {
                it.isLooping = true
                it.start()
            }

            itemBinding.defaultFrontPostVideo.setOnClickListener {
                val videoView = it as VideoView
                if(videoView.isPlaying){
                    videoView.pause()
                } else {
                    videoView.resume()
                }
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PostsViewHolder {
        return PostsViewHolder(PostRecyclerViewItemBinding.inflate(LayoutInflater.from(parent.context), parent, false))
    }

    override fun getItemCount(): Int {
        return postsList.size
    }

    override fun onBindViewHolder(holder: PostsViewHolder, position: Int) {
        val post = postsList[position]
        holder.bindItem(post)
    }
}