package com.inmotion.in_motion_android

import android.net.Uri
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.inmotion.in_motion_android.databinding.ActivityMainBinding
import com.inmotion.in_motion_android.utils.FocusedItemFinder

class MainActivity : AppCompatActivity() {

    private var binding: ActivityMainBinding? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding?.root)

        setSupportActionBar(binding?.mainActivityToolbar)

        if (supportActionBar != null) {
            supportActionBar?.setDisplayHomeAsUpEnabled(false)
            supportActionBar?.setIcon(R.drawable.ic_in_motion_logo)
            supportActionBar?.setDisplayShowTitleEnabled(false)
        }

        val adapter = PostsAdapter(getPosts())
        binding?.rvPosts?.adapter = adapter

        binding?.rvPosts?.setOnScrollListener(
            FocusedItemFinder(
                this,
                (binding?.rvPosts?.layoutManager as LinearLayoutManager),
                { centerItemPosition ->
                    run {
                        playVideoAtPosition(centerItemPosition)
                    }
                })
        )
        binding?.rvPosts?.smoothScrollBy(0, -1)
    }

    private fun playVideoAtPosition(itemPosition: Int) {
        try {
            val viewHolder =
                (binding?.rvPosts?.findViewHolderForAdapterPosition(itemPosition) as PostsAdapter.PostsViewHolder)
            viewHolder.startPlayback()
        } catch (_: Exception) {

        }


    }

    override fun onDestroy() {
        super.onDestroy()
        binding = null
    }

    private fun getPosts(): List<PostDto> {

        val video: Uri = Uri.parse("android.resource://" + packageName + "/raw/" + R.raw.video)

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