package com.inmotion.in_motion_android.fragment

import android.os.Build
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.inmotion.in_motion_android.adapter.CommentsAdapter
import com.inmotion.in_motion_android.data.CommentDto
import com.inmotion.in_motion_android.data.PostDto
import com.inmotion.in_motion_android.databinding.FragmentPostDetailsBinding

class PostDetailsFragment : Fragment() {

    private lateinit var binding: FragmentPostDetailsBinding
    private lateinit var post: PostDto

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            post = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
                it.getSerializable("POST", PostDto::class.java)!!
            } else {
                it.getSerializable("POST") as PostDto
            }
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentPostDetailsBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvUsername.text = post.username
        binding.tvLocationAndTime.text = "${post.location}, ${post.time}"

        binding.defaultBackPostVideo.setVideoURI(post.backVideoPath)
        binding.defaultFrontPostVideo.setVideoURI(post.frontVideoPath)

        binding.btnLove.text = post.loveCount.toString()
        binding.btnLaugh.text = post.laughCount.toString()
        binding.btnWow.text = post.wowCount.toString()
        binding.btnCrying.text = post.cryingCount.toString()

        binding.postDetailsToolbar.setNavigationOnClickListener {
            activity?.onBackPressed()
        }

        val adapter = CommentsAdapter(getComments())
        binding.rvComments.adapter = adapter

        binding.defaultBackPostVideo.setOnPreparedListener {
            it.isLooping = true
            it.start()
        }


        binding.defaultFrontPostVideo.setOnPreparedListener {
            it.isLooping = true
            it.start()
        }
    }

    private fun getComments(): List<CommentDto> {
        return listOf(
            CommentDto("kakauko12", "Katowice", "13:09", "No niezłe fajne"),
            CommentDto("Stephen_mustache", "Królewiec", "13:12", "Nie prawda bo niefajne"),
            CommentDto("kornik112", "Uganda", "13:09", "No dobre"),
            CommentDto("kakauko12", "Katowice", "13:09", "No niezłe fajne"),
            CommentDto("Stephen_mustache", "Królewiec", "13:12", "Nie prawda bo niefajne"),
            CommentDto("kornik112", "Uganda", "13:09", "No dobre"),
            CommentDto("kakauko12", "Katowice", "13:09", "No niezłe fajne"),
            CommentDto("Stephen_mustache", "Królewiec", "13:12", "Nie prawda bo niefajne"),
            CommentDto("kornik112", "Uganda", "13:09", "No dobre")
        )
    }
}