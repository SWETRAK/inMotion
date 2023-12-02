package com.inmotion.in_motion_android.fragment

import android.net.Uri
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.adapter.PostsAdapter
import com.inmotion.in_motion_android.data.remote.PostDto
import com.inmotion.in_motion_android.databinding.FragmentMainBinding
import com.inmotion.in_motion_android.util.FocusedItemFinder

class MainFragment : Fragment() {

    private lateinit var binding: FragmentMainBinding
//    @Suppress("UNCHECKED_CAST")
//    private val userViewModel: UserViewModel by activityViewModels(
//        factoryProducer = {
//            object : ViewModelProvider.Factory {
//                override fun <T : ViewModel> create(modelClass: Class<T>): T {
//                    return UserViewModel(
//                        (activity?.application as InMotionApp).db.userInfoDao(),
//                        Retrofit.Builder()
//                            .baseUrl(ApiConstants.BASE_URL)
//                            .addConverterFactory(GsonConverterFactory.create())
//                            .build()
//                            .create(ImsUserApi::class.java)
//                    ) as T
//                }
//            }
//        }
//    )

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

        binding.avatar.setOnClickListener {
            val bundle = Bundle()
            bundle.putBoolean("IS_LOGGED_IN_USER", true)
            findNavController().navigate(R.id.action_mainFragment_to_userDetailsFragment, bundle)
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

        val video: Uri =
            Uri.parse("android.resource://" + activity?.packageName + "/raw/" + R.raw.video)

        return listOf(
            PostDto(
                "Bon_Jovi",
                "Kielce",
                "16:30",
                video,
                video,
                1,
                8,
                1,
                2
            ),
            PostDto(
                "Endrju69",
                "Warszawa",
                "18:09",
                video,
                video,
                0,
                300,
                67,
                0
            )
        )
    }
}