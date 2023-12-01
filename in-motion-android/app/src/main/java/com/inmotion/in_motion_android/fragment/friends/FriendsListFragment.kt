package com.inmotion.in_motion_android.fragment.friends

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.LiveData
import com.inmotion.in_motion_android.adapter.FriendsAdapter
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.databinding.FragmentFriendsListBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel

class FriendsListFragment(
    private val friendsViewModel: FriendsViewModel,
    private val userViewModel: UserViewModel
) : Fragment() {

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
        friendsViewModel.acceptedFriends.observe(viewLifecycleOwner) {
            binding.rvFriends.adapter = FriendsAdapter(it, friendsViewModel, userViewModel)
        }
    }
}