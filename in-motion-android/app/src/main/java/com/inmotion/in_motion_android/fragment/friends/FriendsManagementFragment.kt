package com.inmotion.in_motion_android.fragment.friends

import android.app.ActionBar
import android.app.Dialog
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.addTextChangedListener
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.lifecycle.map
import androidx.viewpager2.widget.ViewPager2
import com.google.android.material.tabs.TabLayout
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.adapter.FoundUsersAdapter
import com.inmotion.in_motion_android.adapter.FriendsManagementViewPageAdapter
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.data.remote.api.ImsUsersApi
import com.inmotion.in_motion_android.data.remote.dto.user.FullUserInfoDto
import com.inmotion.in_motion_android.databinding.DialogFindFriendBinding
import com.inmotion.in_motion_android.databinding.FragmentFriendsManagementBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch


class FriendsManagementFragment : Fragment() {

    private lateinit var binding: FragmentFriendsManagementBinding
    private var viewPageAdapter: FriendsManagementViewPageAdapter? = null
    private val imsFriendsApi: ImsFriendsApi = ApiUtils.imsFriendsApi
    private val imsUsersApi: ImsUsersApi = ApiUtils.imsUsersApi

    @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        imsUsersApi
                    ) as T
                }
            }
        }
    )

    @Suppress("UNCHECKED_CAST")
    private val friendsViewModel: FriendsViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return FriendsViewModel(
                        (activity?.application as InMotionApp).db.acceptedFriendDao(),
                        (activity?.application as InMotionApp).db.invitedFriendDao(),
                        (activity?.application as InMotionApp).db.requestedFriendDao(),
                        imsFriendsApi
                    ) as T
                }
            }
        }
    )

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentFriendsManagementBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        friendsViewModel.onEvent(FriendEvent.FetchAcceptedFriends(userViewModel.user.value?.token.toString()))
        friendsViewModel.onEvent(FriendEvent.FetchInvitedFriends(userViewModel.user.value?.token.toString()))
        friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(userViewModel.user.value?.token.toString()))

        viewPageAdapter = FriendsManagementViewPageAdapter(
            activity?.supportFragmentManager!!,
            lifecycle,
            friendsViewModel,
            userViewModel
        )
        binding.viewPager.adapter = this.viewPageAdapter
        binding.tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                binding.viewPager.currentItem = tab!!.position
            }

            override fun onTabUnselected(tab: TabLayout.Tab?) {
            }

            override fun onTabReselected(tab: TabLayout.Tab?) {
            }
        })

        binding.friendsToolbar.setLogo(R.drawable.ic_in_motion_logo)
        binding.friendsToolbar.setNavigationOnClickListener {
            activity?.onBackPressed()
        }

        binding.viewPager.registerOnPageChangeCallback(object : ViewPager2.OnPageChangeCallback() {
            override fun onPageSelected(position: Int) {
                super.onPageSelected(position)
                binding.tabLayout.getTabAt(position)?.select()
            }
        })

        binding.btnAddFriend.setOnClickListener {
            showFindFriendDialog()
        }
    }

    private fun showFindFriendDialog() {
        val dialog = Dialog(this.requireActivity())
        val dialogBinding = DialogFindFriendBinding.inflate(layoutInflater)

        var foundUsers = listOf<FullUserInfoDto>()

        var adapter = FoundUsersAdapter(foundUsers, imsFriendsApi, userViewModel, friendsViewModel)
        dialogBinding.rvUsers.adapter = adapter

        dialogBinding.etNickname.addTextChangedListener {
            lifecycleScope.launch(Dispatchers.IO) {
                val response = imsUsersApi.searchUsersByNickname(
                    "Bearer ${userViewModel.user.value?.token}",
                    dialogBinding.etNickname.text.toString()
                )
                if (response.code() < 400) {
                    if (response.body()?.data != null) {
                        activity?.runOnUiThread {

                            foundUsers = response.body()!!.data
                            val usersToFilterOut = ArrayList<String>()
                            if(friendsViewModel.acceptedFriends.value != null){
                                usersToFilterOut.addAll(friendsViewModel.acceptedFriends.value?.map { friend ->
                                    friend.nickname
                                }!!.toList())
                            }
                            if(friendsViewModel.invitedFriends.value != null){
                                usersToFilterOut.addAll(friendsViewModel.invitedFriends.value?.map { friend ->
                                    friend.nickname
                                }!!.toList())
                            }

                            if(friendsViewModel.requestedFriends.value != null){
                                usersToFilterOut.addAll(friendsViewModel.requestedFriends.value?.map { friend ->
                                    friend.nickname
                                }!!.toList())
                            }
                            usersToFilterOut.add(userViewModel.user.value!!.nickname)

                            foundUsers = foundUsers.filter { user -> !usersToFilterOut.contains(user.nickname) }
                            adapter = FoundUsersAdapter(
                                foundUsers,
                                imsFriendsApi,
                                userViewModel,
                                friendsViewModel
                            )
                            dialogBinding.rvUsers.adapter = adapter
                        }

                    }
                }

            }
        }

        dialog.setContentView(dialogBinding.root)
        dialog.window?.setLayout(
            ActionBar.LayoutParams.MATCH_PARENT,
            ActionBar.LayoutParams.WRAP_CONTENT
        )
        dialog.show()
    }
}