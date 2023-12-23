package com.inmotion.in_motion_android.adapter

import android.app.Activity
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.PostCommentDto
import com.inmotion.in_motion_android.databinding.CommentRecyclerViewItemBinding
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import pl.droidsonroids.gif.GifDrawable

class CommentsAdapter(
    private val commentList: List<PostCommentDto>,
    private val userViewModel: UserViewModel,
    private val activity: Activity
) :
    RecyclerView.Adapter<CommentsAdapter.CommentsViewHolder>() {

    inner class CommentsViewHolder(private val itemBinding: CommentRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(comment: PostCommentDto) {
            itemBinding.tvUsername.text = comment.author.nickname
            itemBinding.tvLocationAndTime.text = "${comment.createdAt.substring(0, 19)}"
            itemBinding.tvComment.text = comment.content

            CoroutineScope(Dispatchers.IO).launch {
                val client = OkHttpClient()
                val request = Request.Builder()
                    .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video/gif/${comment.author.id}")
                    .addHeader("authentication", "token")
                    .addHeader("Authorization", "Bearer ${userViewModel.user.value?.token}")
                    .build()
                val response = client.newCall(request).execute()

                if (response.code() < 400) {
                    response.body()?.bytes().let {
                        activity.runOnUiThread {
                            itemBinding.ivAvatar.setImageDrawable(GifDrawable(it!!))
                            itemBinding.ivAvatar.rotation = 90F
                        }
                    }
                }
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CommentsViewHolder {
        return CommentsViewHolder(
            CommentRecyclerViewItemBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            )
        )
    }

    override fun getItemCount(): Int {
        return commentList.size
    }

    override fun onBindViewHolder(holder: CommentsViewHolder, position: Int) {
        val post = commentList[position]
        holder.bindItem(post)
    }
}