package com.inmotion.in_motion_android.adapter

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.FriendRequestDto
import com.inmotion.in_motion_android.databinding.FriendRequestRecyclerViewItemBinding
import java.time.Duration
import java.time.LocalDateTime

class FriendRequestsAdapter(private val requestsList: List<FriendRequestDto>) :
    RecyclerView.Adapter<FriendRequestsAdapter.FriendRequestsViewHolder>() {

    inner class FriendRequestsViewHolder(private val itemBinding: FriendRequestRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(request: FriendRequestDto) {
            itemBinding.tvUsername.text = request.nickname
            val requestedAgo = Duration.between(LocalDateTime.parse(request.requestDate), LocalDateTime.now())
            itemBinding.tvRequestDate.text = "Sent ${requestedAgo.toHours()} h ago"
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FriendRequestsViewHolder{
        return FriendRequestsViewHolder(
            FriendRequestRecyclerViewItemBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            )
        )
    }

    override fun getItemCount(): Int {
        return requestsList.size
    }

    override fun onBindViewHolder(holder: FriendRequestsViewHolder, position: Int) {
        val friend = requestsList[position]
        holder.bindItem(friend)
    }
}