package com.inmotion.in_motion_android.adapter

import android.app.Activity
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.databinding.FriendRecyclerViewItemBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import okhttp3.OkHttpClient
import okhttp3.Request
import pl.droidsonroids.gif.GifDrawable
import java.time.Duration
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class FriendsAdapter(
    private val friendsList: List<AcceptedFriend>,
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel,
    private val activity: Activity
) :
    RecyclerView.Adapter<FriendsAdapter.FriendsViewHolder>() {

    inner class FriendsViewHolder(private val itemBinding: FriendRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(friend: AcceptedFriend) {
            itemBinding.tvUsername.text = friend.nickname
            val df = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")
            val lastSeen =
                Duration.between(
                    LocalDateTime.parse(friend.friendsSince.substring(0, 19), df),
                    LocalDateTime.now()
                )

            itemBinding.tvLastSeen.text =
                "Last seen ${if (lastSeen.toHours() > 23) lastSeen.toDays() else lastSeen.toHours()} h ago"

            itemBinding.btnRemoveFriend.setOnClickListener {
                CoroutineScope(Dispatchers.IO).launch {
                    ApiUtils.imsFriendsApi.unfriendFriend(
                        "Bearer ${userViewModel.user.value?.token}",
                        friend.friendshipId
                    )
                    friendsViewModel.onEvent(FriendEvent.FetchAcceptedFriends(userViewModel.user.value?.token.toString()))
                }
            }

            runBlocking(Dispatchers.IO) {
                val client = OkHttpClient()
                val request = Request.Builder()
                    .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video/gif/${friend.id}")
                    .addHeader("authentication", "token")
                    .addHeader("Authorization", "Bearer ${userViewModel.user.value?.token}")
                    .build()
                val response = client.newCall(request).execute()

                if (response.code() < 400) {
                    val imageData = response.body()?.bytes().let {
                        activity.runOnUiThread {
                            itemBinding.ivAvatar.setImageDrawable(GifDrawable(it!!))
                            itemBinding.ivAvatar.rotation = 90F
                        }
                    }

                }
            }
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