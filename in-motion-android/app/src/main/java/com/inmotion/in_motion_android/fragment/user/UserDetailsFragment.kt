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
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.remote.ApiConstants
import com.inmotion.in_motion_android.data.remote.api.ImsUserApi
import com.inmotion.in_motion_android.databinding.FragmentUserDetailsBinding
import com.inmotion.in_motion_android.state.UserViewModel
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

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
                findNavController().navigate(R.id.action_userDetailsFragment_to_friendsManagementFragment)
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

}