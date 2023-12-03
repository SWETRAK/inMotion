package com.inmotion.in_motion_android.adapter

import android.app.Activity
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.data.remote.dto.user.FullUserInfoDto
import com.inmotion.in_motion_android.databinding.FoundUserRecyclerViewItemBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.MainScope
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import pl.droidsonroids.gif.GifDrawable

class FoundUsersAdapter(
    private val usersList: List<FullUserInfoDto>,
    private val imsFriendsApi: ImsFriendsApi,
    private val userViewModel: UserViewModel,
    private val friendsViewModel: FriendsViewModel,
    private val activity: Activity
) :
    RecyclerView.Adapter<FoundUsersAdapter.FoundUsersViewHolder>() {
    inner class FoundUsersViewHolder(private val itemBinding: FoundUserRecyclerViewItemBinding) :
        RecyclerView.ViewHolder(itemBinding.root) {
        fun bindItem(user: FullUserInfoDto) {
            itemBinding.tvNickname.text = user.nickname
            itemBinding.btnAddAsFriend.setOnClickListener {
                CoroutineScope(Dispatchers.IO).launch {
                    val response =
                        imsFriendsApi.addFriend(
                            "Bearer ${userViewModel.user.value?.token}",
                            user.id
                        )
                    if (response.code() < 400) {
                        friendsViewModel.onEvent(FriendEvent.FetchInvitedFriends(userViewModel.user.value?.token.toString()))
                        Log.i("FRIEND", "REQUESTED NEW FRIEND")
                        MainScope().launch {
                            itemBinding.btnAddAsFriend.visibility = View.GONE
                        }
                    }
                }
            }

            CoroutineScope(Dispatchers.IO).launch {
                val client = OkHttpClient()
                val request = Request.Builder()
                    .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video/gif/${user.id}")
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

    override fun getItemCount(): Int {
        return usersList.size
    }

    override fun onBindViewHolder(holder: FoundUsersViewHolder, position: Int) {
        val user = usersList[position]
        holder.bindItem(user)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FoundUsersViewHolder {
        return FoundUsersViewHolder(
            FoundUserRecyclerViewItemBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            )
        )
    }
}