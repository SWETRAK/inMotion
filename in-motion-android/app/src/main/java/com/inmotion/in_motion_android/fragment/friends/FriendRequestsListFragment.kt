package com.inmotion.in_motion_android.fragment.friends

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.LiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.adapter.FriendRequestsAdapter
import com.inmotion.in_motion_android.data.database.InMotionDatabase
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.databinding.FragmentFriendRequestsListBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel

class FriendRequestsListFragment(
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel
) : Fragment() {

    private lateinit var binding: FragmentFriendRequestsListBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentFriendRequestsListBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        friendsViewModel.requestedFriends.observe(viewLifecycleOwner){
            binding.rvFriends.adapter = FriendRequestsAdapter(ArrayList(it), friendsViewModel, userViewModel)
        }
    }
}