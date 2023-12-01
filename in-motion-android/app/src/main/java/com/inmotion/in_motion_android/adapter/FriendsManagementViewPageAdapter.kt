package com.inmotion.in_motion_android.adapter

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend
import com.inmotion.in_motion_android.fragment.friends.FriendRequestsListFragment
import com.inmotion.in_motion_android.fragment.friends.FriendsListFragment
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel

class FriendsManagementViewPageAdapter(
    fragmentManager: FragmentManager,
    lifecycle: Lifecycle,
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel
) : FragmentStateAdapter(fragmentManager, lifecycle) {


    override fun getItemCount(): Int {
        return 2
    }

    override fun createFragment(position: Int): Fragment {
        return when (position) {
            0 -> FriendsListFragment(friendsViewModel, userViewModel)
            1 -> FriendRequestsListFragment(friendsViewModel, userViewModel)
            else -> FriendsListFragment(friendsViewModel, userViewModel)
        }
    }
}