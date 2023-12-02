package com.inmotion.in_motion_android.adapter

import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.databinding.FriendRequestRecyclerViewItemBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.MainScope
import kotlinx.coroutines.launch
import java.time.Duration
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class FriendRequestsAdapter(
    private val requestsList: ArrayList<RequestedFriend>,
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel
) :
    RecyclerView.Adapter<FriendRequestsAdapter.FriendRequestsViewHolder>() {

    private val imsFriendsApi: ImsFriendsApi = ApiUtils.imsFriendsApi

    inner class FriendRequestsViewHolder(private val itemBinding: FriendRequestRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(request: RequestedFriend) {
            val df = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")
            itemBinding.tvUsername.text = request.nickname
            val requestedAgo = Duration.between(
                LocalDateTime.parse(request.requested.substring(0, 19), df),
                LocalDateTime.now()
            )
            itemBinding.tvRequestDate.text =
                "Sent ${if (requestedAgo.toHours() > 23) requestedAgo.toDays() else requestedAgo.toHours()} h ago"

            itemBinding.btnAccept.setOnClickListener {
                CoroutineScope(Dispatchers.IO).launch {
                    val response = imsFriendsApi.acceptFriend(
                        "Bearer ${userViewModel.user.value?.token}",
                        request.friendshipId
                    )
                    if(response.code() < 400) {
                        Log.i("FRIEND REQUESTS", "ACCEPTED ${request.nickname}")
                        friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(userViewModel.user.value?.token.toString()))
                        MainScope().launch{
                            notifyDataSetChanged()
                        }

                    } else {
                        Log.i("FRIEND REQUESTS", "FAILED TO ACCEPT ${request.nickname}, code ${response.code()}")
                    }

                }
            }

            itemBinding.btnReject.setOnClickListener {
                CoroutineScope(Dispatchers.IO).launch {
                    imsFriendsApi.rejectFriend(
                        "Bearer ${userViewModel.user.value?.token}",
                        request.friendshipId
                    )
                    friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(userViewModel.user.value?.token.toString()))
                    MainScope().launch{
                        notifyDataSetChanged()
                    }
                }
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FriendRequestsViewHolder {
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