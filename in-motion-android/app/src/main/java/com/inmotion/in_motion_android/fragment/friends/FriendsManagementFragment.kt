package com.inmotion.in_motion_android.fragment.friends

import android.os.Build
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.viewpager2.widget.ViewPager2
import com.google.android.material.tabs.TabLayout
import com.google.gson.Gson
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.adapter.FriendsManagementViewPageAdapter
import com.inmotion.in_motion_android.data.FriendDto
import com.inmotion.in_motion_android.data.FriendRequestDto
import com.inmotion.in_motion_android.databinding.FragmentFriendsManagementBinding


class FriendsManagementFragment : Fragment() {

    private lateinit var binding: FragmentFriendsManagementBinding
    private var viewPageAdapter: FriendsManagementViewPageAdapter? = null
    private var friends: List<FriendDto>? = null
    private var requests: List<FriendRequestDto>? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            val gson = Gson()
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
                friends =
                    gson.fromJson(it.getString("FRIENDS"), Array<FriendDto>::class.java).toList()
                requests =
                    gson.fromJson(it.getString("REQUESTS"), Array<FriendRequestDto>::class.java)
                        .toList()
            }
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentFriendsManagementBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        viewPageAdapter = FriendsManagementViewPageAdapter(
            activity?.supportFragmentManager!!,
            lifecycle,
            friends!!,
            requests!!
        )
        binding.viewPager.adapter = this.viewPageAdapter
        binding.tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                binding.viewPager.setCurrentItem(tab!!.position)
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