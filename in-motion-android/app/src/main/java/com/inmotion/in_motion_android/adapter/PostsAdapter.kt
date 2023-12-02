package com.inmotion.in_motion_android.adapter

import android.os.Bundle
import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.VideoView
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.remote.PostDto
import com.inmotion.in_motion_android.databinding.PostRecyclerViewItemBinding

class PostsAdapter(private val postsList: List<PostDto>) :
    RecyclerView.Adapter<PostsAdapter.PostsViewHolder>() {

    inner class PostsViewHolder(private val itemBinding: PostRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(post: PostDto) {
            itemBinding.tvUsername.text = post.username
            itemBinding.tvLocationAndTime.text = "${post.location}, ${post.time}"
            itemBinding.defaultBackPostVideo.setVideoURI(post.backVideoPath)
            itemBinding.defaultFrontPostVideo.setVideoURI(post.frontVideoPath)
            itemBinding.btnLove.text = post.loveCount.toString()
            itemBinding.btnLaugh.text = post.laughCount.toString()
            itemBinding.btnWow.text = post.wowCount.toString()
            itemBinding.btnCrying.text = post.cryingCount.toString()

            itemBinding.ivComment.setOnClickListener {
                val bundle = Bundle()
                bundle.putSerializable("POST", post)
                it.findNavController()
                    .navigate(R.id.action_mainFragment_to_postDetailsFragment, bundle)
            }

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