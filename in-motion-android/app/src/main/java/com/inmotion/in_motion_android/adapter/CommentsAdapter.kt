package com.inmotion.in_motion_android.adapter

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.remote.CommentDto
import com.inmotion.in_motion_android.databinding.CommentRecyclerViewItemBinding

class CommentsAdapter(private val commentList: List<CommentDto>) :
    RecyclerView.Adapter<CommentsAdapter.CommentsViewHolder>() {

    inner class CommentsViewHolder(private val itemBinding: CommentRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(comment: CommentDto) {
            itemBinding.tvUsername.text = comment.username
            itemBinding.tvLocationAndTime.text = "${comment.location}, ${comment.time}"
            itemBinding.tvComment.text = comment.comment
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