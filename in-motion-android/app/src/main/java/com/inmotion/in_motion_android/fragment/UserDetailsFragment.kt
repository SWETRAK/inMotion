package com.inmotion.in_motion_android.fragment

import android.os.Build
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.google.gson.Gson
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.FriendDto
import com.inmotion.in_motion_android.data.FriendRequestDto
import com.inmotion.in_motion_android.data.UserDto
import com.inmotion.in_motion_android.databinding.FragmentUserDetailsBinding
import java.time.LocalDateTime
import java.time.Month

class UserDetailsFragment : Fragment() {

    private lateinit var binding: FragmentUserDetailsBinding
    private var user: UserDto? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            user = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
                it.getSerializable("USER", UserDto::class.java)
            } else {
                it.getSerializable("USER") as UserDto
            }
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentUserDetailsBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        binding.tvUsername.text = user?.nickname
        binding.tvBio.text = user?.bio
        binding.userDetailsToolbar.setLogo(R.drawable.ic_in_motion_logo)
        binding.userDetailsToolbar.setNavigationOnClickListener {
            activity?.onBackPressed()
        }


        binding.btnFriends.setOnClickListener {
            val gson = Gson()
            val bundle = Bundle()
            bundle.putString("FRIENDS", gson.toJson(getFriends()))
            bundle.putString("REQUESTS", gson.toJson(getRequests()))
            findNavController()
                .navigate(R.id.action_userDetailsFragment_to_friendsManagementFragment, bundle)
        }
    }

    private fun getFriends(): List<FriendDto> {
        return arrayListOf(
            FriendDto("Nickname", LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()),
            FriendDto("Nickname2", LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()),
            FriendDto("Nickname3", LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString())
        )
    }

    private fun getRequests(): List<FriendRequestDto> {
        return arrayListOf(
            FriendRequestDto(
                "Nickname4",
                LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()
            ),
            FriendRequestDto(
                "Nickname5",
                LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()
            ),
            FriendRequestDto(
                "Nickname6",
                LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()
            )
        )
    }
}