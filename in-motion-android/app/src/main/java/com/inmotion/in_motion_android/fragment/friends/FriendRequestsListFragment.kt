package com.inmotion.in_motion_android.fragment.friends

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.inmotion.in_motion_android.adapter.FriendRequestsAdapter
import com.inmotion.in_motion_android.data.FriendRequestDto
import com.inmotion.in_motion_android.databinding.FragmentFriendRequestsListBinding

class FriendRequestsListFragment(private val requests: List<FriendRequestDto>) : Fragment() {

    private lateinit var binding: FragmentFriendRequestsListBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentFriendRequestsListBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        binding.rvFriends.adapter = FriendRequestsAdapter(requests)
    }
}