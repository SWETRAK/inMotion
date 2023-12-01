package com.inmotion.in_motion_android.adapter

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.databinding.FriendRecyclerViewItemBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel
import java.time.Duration
import java.time.LocalDateTime

class FriendsAdapter(
    private val friendsList: List<AcceptedFriend>,
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel
) :
    RecyclerView.Adapter<FriendsAdapter.FriendsViewHolder>() {

    inner class FriendsViewHolder(private val itemBinding: FriendRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(friend: AcceptedFriend) {
            itemBinding.tvUsername.text = friend.nickname
            val lastSeen =
                Duration.between(LocalDateTime.parse(friend.friendsSince), LocalDateTime.now())

            itemBinding.tvLastSeen.text = "Last seen ${lastSeen.toHours()} h ago"
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FriendsViewHolder {
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