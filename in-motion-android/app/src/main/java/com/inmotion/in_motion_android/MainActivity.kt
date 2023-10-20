package com.inmotion.in_motion_android

import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.inmotion.in_motion_android.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    var binding: ActivityMainBinding? = null

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
    }

    override fun onDestroy() {
        super.onDestroy()
        binding = null
    }

    fun getPosts(): List<PostDto> {

        val video: Uri = Uri.parse("android.resource://" + packageName + "/raw/" + R.raw.video)

        return listOf(
            PostDto("Stephen_mustache", "Lublin", "13:09",video, video, 10),
            PostDto("Bon_Jovi", "Kielce", "16:30",video, video, 1),
            PostDto("Endrju69", "Warszawa", "18:09",video, video, 0),
            PostDto("Jagienka123", "Phoenix", "21:37",video, video, 68),
            PostDto("Orzechowiec12", "Glasgow", "4:20",video, video, 41)
        )
    }
}