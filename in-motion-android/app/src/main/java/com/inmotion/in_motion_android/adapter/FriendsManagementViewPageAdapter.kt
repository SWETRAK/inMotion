package com.inmotion.in_motion_android.adapter

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.inmotion.in_motion_android.data.remote.FriendDto
import com.inmotion.in_motion_android.data.remote.FriendRequestDto
import com.inmotion.in_motion_android.fragment.friends.FriendRequestsListFragment
import com.inmotion.in_motion_android.fragment.friends.FriendsListFragment

class FriendsManagementViewPageAdapter(
    fragmentManager: FragmentManager,
    lifecycle: Lifecycle,
    private val friends: List<FriendDto>,
    private val requests: List<FriendRequestDto>
) : FragmentStateAdapter(fragmentManager, lifecycle) {


    override fun getItemCount(): Int {
        return 2
    }

    override fun createFragment(position: Int): Fragment {
        return when (position) {
            0 -> FriendsListFragment(friends)
            1 -> FriendRequestsListFragment(requests)
            else -> FriendsListFragment(friends)
        }
    }
}