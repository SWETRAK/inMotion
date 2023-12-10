package com.inmotion.in_motion_android.fragment.user

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.VideoView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.database.event.UserEvent
import com.inmotion.in_motion_android.data.remote.ApiConstants
import com.inmotion.in_motion_android.data.remote.api.ImsUsersApi
import com.inmotion.in_motion_android.databinding.FragmentUserDetailsBinding
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.runBlocking
import okhttp3.OkHttpClient
import okhttp3.Request
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.File
import java.io.FileOutputStream

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
                            .create(ImsUsersApi::class.java)
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
        runBlocking {
            userViewModel.onEvent(UserEvent.UpdateFullUserInfo)
        }
        val user = userViewModel.user.value
        val fullUser = userViewModel.fullUserInfo.value
        binding.tvNickname.text = user?.nickname
        binding.tvBio.text = fullUser?.bio ?: ""

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
                runBlocking {
                    userViewModel.onEvent(UserEvent.DeleteUser)

                    val path = requireContext().filesDir
                    try {
                        val file = File(path, "${userViewModel.user.value?.id}.mp4")
                        file.delete()
                    }catch (e: Exception) {
                        Log.i("USER_DETAILS_FRAGMENT", "Couldn't delete non existing file!")
                    }
                }

                if(binding.ivAvatarVideo.isPlaying) {
                    binding.ivAvatarVideo.stopPlayback()
                }
                val bundle = Bundle()
                bundle.putBoolean("LOGOUT", true)
                findNavController().navigate(R.id.action_userDetailsFragment_to_loginFragment, bundle)
            }
        } else {
            binding.btnFriends.visibility = View.GONE
            binding.btnLogout.visibility = View.GONE
            binding.btnEditProfile.visibility = View.GONE
        }

        initProfileVideo()
    }

    private fun initProfileVideo() {
        runBlocking(Dispatchers.IO) {
            val client = OkHttpClient()
            val request = Request.Builder()
                .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video/mp4/${userViewModel.user.value?.id}")
                .addHeader("authentication", "token")
                .addHeader("Authorization", "Bearer ${userViewModel.user.value?.token}")
                .build()
            val response = client.newCall(request).execute()

            if (response.code() < 400) {
                try {
                    val path = requireContext().filesDir
                    val file = File(path, "${userViewModel.user.value?.id}.mp4")
                    val stream = FileOutputStream(file)
                    try {
                        stream.write(response.body()?.bytes())
                    } finally {
                        stream.close()
                    }

                    binding.ivAvatarVideo.setVideoPath(file.path)
                    binding.ivAvatar.visibility = View.INVISIBLE
                    binding.ivAvatarVideo.visibility = View.VISIBLE

                    binding.ivAvatarVideo.setOnPreparedListener {
                        it.isLooping = true
                        it.seekTo(1)
                        it.start()
                    }

                    binding.ivAvatarVideo.setOnClickListener {
                        if ((it as VideoView).isPlaying) {
                            it.pause()
                        } else {
                            it.start()
                        }
                    }

                } catch (e: Exception) {
                    Log.e("USER_DETAILS_FRAGMENT", "Couldn't read avatar video!")
                }
            } else {
                activity?.runOnUiThread {
                    binding.ivAvatar.visibility = View.VISIBLE
                    binding.ivAvatarVideo.visibility = View.INVISIBLE
                }
            }
        }
    }

}