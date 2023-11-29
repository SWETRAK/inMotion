package com.inmotion.in_motion_android.adapter

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend
import com.inmotion.in_motion_android.fragment.friends.FriendRequestsListFragment
import com.inmotion.in_motion_android.fragment.friends.FriendsListFragment

class FriendsManagementViewPageAdapter(
    fragmentManager: FragmentManager,
    lifecycle: Lifecycle,
    private val acceptedFriends: List<AcceptedFriend>,
    private val requestedFriends: List<RequestedFriend>
) : FragmentStateAdapter(fragmentManager, lifecycle) {


    override fun getItemCount(): Int {
        return 2
    }

    override fun createFragment(position: Int): Fragment {
        return when (position) {
            0 -> FriendsListFragment(acceptedFriends)
            1 -> FriendRequestsListFragment(requestedFriends)
            else -> FriendsListFragment(acceptedFriends)
        }
    }
}