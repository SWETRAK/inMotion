package com.inmotion.in_motion_android.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.adapter.PostsAdapter
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.PostDto
import com.inmotion.in_motion_android.databinding.FragmentMainBinding
import com.inmotion.in_motion_android.state.UserViewModel
import com.inmotion.in_motion_android.util.FocusedItemFinder
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.runBlocking
import okhttp3.OkHttpClient
import okhttp3.Request
import pl.droidsonroids.gif.GifDrawable

class MainFragment : Fragment() {

    private lateinit var binding: FragmentMainBinding
    @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        ApiUtils.imsUsersApi
                    ) as T
                }
            }
        }
    )

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentMainBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.mainFragmentToolbar.setLogo(R.drawable.ic_in_motion_logo)


        runBlocking(Dispatchers.IO) {

            val client = OkHttpClient()
            val request = Request.Builder()
                .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video/gif/${userViewModel.user.value?.id}")
                .addHeader("authentication", "token")
                .addHeader("Authorization", "Bearer ${userViewModel.user.value?.token}")
                .build()
            val response = client.newCall(request).execute()

            if(response.code() < 400){
                response.body()?.bytes().let {
                    binding.gibAvatar.setImageDrawable(GifDrawable(it!!))
                    binding.gibAvatar.rotation = 90F
                }

            }

        }

        val posts = getPosts()
        if(posts.isNotEmpty()) {
            val adapter = PostsAdapter(getPosts())
            binding.rvPosts.adapter = adapter

            binding.rvPosts.setOnScrollListener(
                context?.let {
                    FocusedItemFinder(
                        it,
                        (binding.rvPosts.layoutManager as LinearLayoutManager),
                        { centerItemPosition ->
                            run {
                                playVideoAtPosition(centerItemPosition)
                            }
                        })
                }
            )
            binding.rvPosts.smoothScrollBy(0, -1)


            binding.rvPosts.visibility = View.VISIBLE
            binding.tvNoPosts.visibility = View.GONE
        } else {
            binding.rvPosts.visibility = View.GONE
            binding.tvNoPosts.visibility = View.VISIBLE
        }

        binding.gibAvatar.setOnClickListener {
            val bundle = Bundle()
            bundle.putBoolean("IS_LOGGED_IN_USER", true)
            findNavController().navigate(
                R.id.action_mainFragment_to_userDetailsFragment,
                bundle
            )
        }
    }

    private fun playVideoAtPosition(itemPosition: Int) {
        try {
            val viewHolder =
                (binding.rvPosts.findViewHolderForAdapterPosition(itemPosition) as PostsAdapter.PostsViewHolder)
            viewHolder.startPlayback()
        } catch (_: Exception) {

        }
    }

    private fun getPosts(): List<PostDto> {
        return listOf()
    }
}