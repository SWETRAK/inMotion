package com.inmotion.in_motion_android.fragment.user

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.google.gson.Gson
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.remote.FriendDto
import com.inmotion.in_motion_android.data.remote.FriendRequestDto
import com.inmotion.in_motion_android.data.remote.api.ApiConstants
import com.inmotion.in_motion_android.data.remote.api.ImsUserApi
import com.inmotion.in_motion_android.databinding.FragmentUserDetailsBinding
import com.inmotion.in_motion_android.state.UserViewModel
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.time.LocalDateTime
import java.time.Month

class UserDetailsFragment : Fragment() {

    private lateinit var binding: FragmentUserDetailsBinding
    private var isLoggedInUser: Boolean = false
    @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        Retrofit.Builder()
                            .baseUrl(ApiConstants.BASE_URL)
                            .addConverterFactory(GsonConverterFactory.create())
                            .build()
                            .create(ImsUserApi::class.java)
                    ) as T
                }
            }
        }
    )

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            isLoggedInUser = it.getBoolean("IS_LOGGED_IN_USER")
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentUserDetailsBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        val user = userViewModel.state.value.user!!
        val fullUser = userViewModel.state.value.fullUserInfo!!
        binding.tvNickname.text = user.nickname
        binding.tvBio.text = fullUser.bio ?: ""
        binding.userDetailsToolbar.setLogo(R.drawable.ic_in_motion_logo)
        binding.userDetailsToolbar.setNavigationOnClickListener {
            activity?.onBackPressed()
        }

        if (isLoggedInUser) {
            binding.btnFriends.setOnClickListener {
                val gson = Gson()
                val bundle = Bundle()
                bundle.putString("FRIENDS", gson.toJson(getFriends()))
                bundle.putString("REQUESTS", gson.toJson(getRequests()))
                findNavController()
                    .navigate(R.id.action_userDetailsFragment_to_friendsManagementFragment, bundle)
            }

            binding.btnEditProfile.setOnClickListener {
                findNavController().navigate(
                    R.id.action_userDetailsFragment_to_editUserDetailsFragment,
                    arguments
                )

            }

            binding.btnLogout.setOnClickListener {

                findNavController().navigate(R.id.action_userDetailsFragment_to_loginFragment)
            }
        } else {
            binding.btnFriends.visibility = View.GONE
            binding.btnLogout.visibility = View.GONE
            binding.btnEditProfile.visibility = View.GONE
        }
    }

    private fun getFriends(): List<FriendDto> {
        return arrayListOf(
            FriendDto(
                "Nickname",
                LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()
            ),
            FriendDto(
                "Nickname2",
                LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()
            ),
            FriendDto(
                "Nickname3",
                LocalDateTime.of(2023, Month.OCTOBER, 29, 18, 30).toString()
            )
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