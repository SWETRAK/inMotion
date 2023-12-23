package com.inmotion.in_motion_android.fragment.posts

import android.os.Build
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.adapter.CommentsAdapter
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.dto.posts.GetPostResponseDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.CreatePostCommentDto
import com.inmotion.in_motion_android.data.remote.dto.posts.comments.PostCommentDto
import com.inmotion.in_motion_android.data.remote.dto.posts.reactions.CreatePostReactionDto
import com.inmotion.in_motion_android.databinding.FragmentPostDetailsBinding
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class PostDetailsFragment : Fragment() {

    private lateinit var binding: FragmentPostDetailsBinding
    private lateinit var post: GetPostResponseDto
    private lateinit var frontVideoPath: String
    private lateinit var backVideoPath: String

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

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            post = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
                it.getSerializable("POST", GetPostResponseDto::class.java)!!
            } else {
                it.getSerializable("POST") as GetPostResponseDto
            }
            frontVideoPath = it.getString("FRONT_VIDEO").toString()
            backVideoPath = it.getString("BACK_VIDEO").toString()
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentPostDetailsBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvUsername.text = post.author.nickname
        binding.tvLocationAndTime.text = "${post.createdAt.substring(0, 19)}"

        binding.defaultBackPostVideo.setVideoPath(backVideoPath)
        binding.defaultFrontPostVideo.setVideoPath(frontVideoPath)

        binding.btnLove.text = post.postReactionsCount.toString()

        if(post.isLikedByUser){
            binding.btnLove.background =
                ContextCompat.getDrawable(requireActivity(), R.drawable.border_rounded_filled)
        } else {
            binding.btnLove.setOnClickListener {
                lovePost(post)
            }
        }


        binding.postDetailsToolbar.setNavigationOnClickListener {
            activity?.onBackPressed()
        }

        binding.defaultBackPostVideo.setOnPreparedListener {
            it.isLooping = true
            it.start()
        }


        binding.defaultFrontPostVideo.setOnPreparedListener {
            it.isLooping = true
            it.start()
        }

        binding.btnAddComment.setOnClickListener {
            addComment()
        }

        initComments()
    }

    private fun addComment() {
        lifecycleScope.launch(Dispatchers.IO){
            val comment = binding.etComment.text.toString()
            if(comment.isNotEmpty()) {
                val commentResponse = ApiUtils.imsPostsApi.createPostComment(
                    "Bearer ${userViewModel.user.value?.token}",
                    CreatePostCommentDto(
                        comment,
                        post.id
                    )
                )

                if(commentResponse.code() < 400) {
                    initComments()
                }
            }
        }

    }

    private fun initComments() {
        lifecycleScope.launch(Dispatchers.IO) {
            val commentsResponse = ApiUtils.imsPostsApi.getCommentsForPost(
                "Bearer ${userViewModel.user.value?.token}",
                post.id
            )
            var comments = listOf<PostCommentDto>()

            if(commentsResponse.code() < 400 && commentsResponse.body()?.data != null) {
                comments = commentsResponse.body()!!.data.sortedBy {
                    val df = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")
                     LocalDateTime.parse(it.createdAt.substring(0, 19))
                }
            }

            activity?.runOnUiThread {
                val adapter = CommentsAdapter(comments, userViewModel, requireActivity())
                binding.rvComments.adapter = adapter
            }

        }
    }

    private fun lovePost(post: GetPostResponseDto) {
        CoroutineScope(Dispatchers.IO).launch {
            val reactionResponse = ApiUtils.imsPostsApi.createPostReaction(
                "Bearer ${userViewModel.user.value?.token}",
                CreatePostReactionDto(
                    post.id,
                    "U+2764"
                )
            )
            if (reactionResponse.code() < 400) {
                activity?.runOnUiThread {
                    binding.btnLove.background =
                        ContextCompat.getDrawable(requireActivity(), R.drawable.border_rounded_filled)
                }
                binding.btnLove.text = (post.postReactionsCount + 1).toString()
            }
        }
    }
}