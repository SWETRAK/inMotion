package com.inmotion.in_motion_android.fragment.friends

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.viewpager2.widget.ViewPager2
import com.google.android.material.tabs.TabLayout
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.adapter.FriendsManagementViewPageAdapter
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.databinding.FragmentFriendsManagementBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel


class FriendsManagementFragment : Fragment() {

    private lateinit var binding: FragmentFriendsManagementBinding
    private var viewPageAdapter: FriendsManagementViewPageAdapter? = null
    private val imsFriendsApi: ImsFriendsApi = ApiUtils.imsFriendsApi

    @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        ApiUtils.imsUserApi
                    ) as T
                }
            }
        }
    )

    @Suppress("UNCHECKED_CAST")
    private val friendsViewModel: FriendsViewModel by activityViewModels(
        factoryProducer = {
            object: ViewModelProvider.Factory {
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
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        friendsViewModel.onEvent(FriendEvent.FetchAcceptedFriends(userViewModel.getBearerToken()))
        friendsViewModel.onEvent(FriendEvent.FetchInvitedFriends(userViewModel.getBearerToken()))
        friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(userViewModel.getBearerToken()))
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentFriendsManagementBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        viewPageAdapter = FriendsManagementViewPageAdapter(
            activity?.supportFragmentManager!!,
            lifecycle,
            friendsViewModel.state.value.accepted,
            friendsViewModel.state.value.requested
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
    }
}