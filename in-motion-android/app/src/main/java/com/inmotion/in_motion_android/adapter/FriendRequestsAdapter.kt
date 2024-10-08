package com.inmotion.in_motion_android.adapter

import android.app.Activity
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
import okhttp3.OkHttpClient
import okhttp3.Request
import pl.droidsonroids.gif.GifDrawable
import java.time.Duration
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class FriendRequestsAdapter(
    private val requestsList: ArrayList<RequestedFriend>,
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel,
    private val activity: Activity
) :
    RecyclerView.Adapter<FriendRequestsAdapter.FriendRequestsViewHolder>() {

    private val imsFriendsApi: ImsFriendsApi = ApiUtils.imsFriendsApi

    inner class FriendRequestsViewHolder(private val itemBinding: FriendRequestRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(friendRequest: RequestedFriend) {
            val df = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")
            itemBinding.tvUsername.text = friendRequest.nickname
            val requestedAgo = Duration.between(
                LocalDateTime.parse(friendRequest.requested.substring(0, 19), df),
                LocalDateTime.now()
            )
            itemBinding.tvRequestDate.text =
                "Sent ${if (requestedAgo.toHours() > 23) requestedAgo.toDays() else requestedAgo.toHours()} h ago"

            itemBinding.btnAccept.setOnClickListener {
                CoroutineScope(Dispatchers.IO).launch {
                    val response = imsFriendsApi.acceptFriend(
                        "Bearer ${userViewModel.user.value?.token}",
                        friendRequest.friendshipId
                    )
                    if (response.code() < 400) {
                        Log.i("FRIEND REQUESTS", "ACCEPTED ${friendRequest.nickname}")
                        friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(userViewModel.user.value?.token.toString()))
                        MainScope().launch {
                            notifyDataSetChanged()
                        }

                    } else {
                        Log.i(
                            "FRIEND REQUESTS",
                            "FAILED TO ACCEPT ${friendRequest.nickname}, code ${response.code()}"
                        )
                    }

                }
            }

            itemBinding.btnReject.setOnClickListener {
                CoroutineScope(Dispatchers.IO).launch {
                    imsFriendsApi.rejectFriend(
                        "Bearer ${userViewModel.user.value?.token}",
                        friendRequest.friendshipId
                    )
                    friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(userViewModel.user.value?.token.toString()))
                    MainScope().launch {
                        notifyDataSetChanged()
                    }
                }
            }

            CoroutineScope(Dispatchers.IO).launch {
                val client = OkHttpClient()
                val request = Request.Builder()
                    .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video/gif/${friendRequest.id}")
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