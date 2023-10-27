package com.inmotion.in_motion_android.fragment

import android.net.Uri
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import com.inmotion.in_motion_android.data.PostDto
import com.inmotion.in_motion_android.PostsAdapter
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.databinding.MainFragmentBinding
import com.inmotion.in_motion_android.util.FocusedItemFinder

class MainFragment : Fragment() {

    private lateinit var binding: MainFragmentBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = MainFragmentBinding.inflate(layoutInflater)
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

        val video: Uri = Uri.parse("android.resource://" + activity?.packageName + "/raw/" + R.raw.video)

        return listOf(
            PostDto("Stephen_mustache", "Lublin", "13:09", video, video, 10),
            PostDto("Bon_Jovi", "Kielce", "16:30", video, video, 1),
            PostDto("Endrju69", "Warszawa", "18:09", video, video, 0),
            PostDto("Jagienka123", "Phoenix", "21:37", video, video, 68),
            PostDto("Orzechowiec12", "Glasgow", "4:20", video, video, 41),
            PostDto("Stephen_mustache", "Lublin", "13:09", video, video, 10),
            PostDto("Bon_Jovi", "Kielce", "16:30", video, video, 1),
            PostDto("Endrju69", "Warszawa", "18:09", video, video, 0),
            PostDto("Jagienka123", "Phoenix", "21:37", video, video, 68),
            PostDto("Orzechowiec12", "Glasgow", "4:20", video, video, 41),
            PostDto("Stephen_mustache", "Lublin", "13:09", video, video, 10),
            PostDto("Bon_Jovi", "Kielce", "16:30", video, video, 1),
            PostDto("Endrju69", "Warszawa", "18:09", video, video, 0),
            PostDto("Jagienka123", "Phoenix", "21:37", video, video, 68),
            PostDto("Orzechowiec12", "Glasgow", "4:20", video, video, 41)
        )
    }
}