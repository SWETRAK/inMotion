package com.inmotion.in_motion_android.fragment.friends

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.inmotion.in_motion_android.adapter.FriendsAdapter
import com.inmotion.in_motion_android.data.remote.FriendDto
import com.inmotion.in_motion_android.databinding.FragmentFriendsListBinding

class FriendsListFragment(private var friends: List<FriendDto>) : Fragment() {

    private lateinit var binding: FragmentFriendsListBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentFriendsListBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        binding.rvFriends.adapter = FriendsAdapter(friends)
    }
}