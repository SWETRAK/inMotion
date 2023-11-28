package com.inmotion.in_motion_android.adapter

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.remote.FriendDto
import com.inmotion.in_motion_android.databinding.FriendRecyclerViewItemBinding
import java.time.Duration
import java.time.LocalDateTime

class FriendsAdapter(private val friendsList: List<FriendDto>) :
    RecyclerView.Adapter<FriendsAdapter.FriendsViewHolder>() {

    inner class FriendsViewHolder(private val itemBinding: FriendRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(friend: FriendDto) {
            itemBinding.tvUsername.text = friend.nickname
            val lastSeen = Duration.between(LocalDateTime.parse(friend.lastActivity), LocalDateTime.now())

            itemBinding.tvLastSeen.text = "Last seen ${lastSeen.toHours()} h ago"
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FriendsViewHolder{
        return FriendsViewHolder(
            FriendRecyclerViewItemBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            )
        )
    }

    override fun getItemCount(): Int {
        return friendsList.size
    }

    override fun onBindViewHolder(holder: FriendsViewHolder, position: Int) {
        val friend = friendsList[position]
        holder.bindItem(friend)
    }
}