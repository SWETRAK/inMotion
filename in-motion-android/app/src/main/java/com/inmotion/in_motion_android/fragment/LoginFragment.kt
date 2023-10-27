package com.inmotion.in_motion_android.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.databinding.LoginFragmentBinding

class LoginFragment : Fragment() {

    private lateinit var binding: LoginFragmentBinding
    private lateinit var navController: NavController

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = LoginFragmentBinding.inflate(layoutInflater)
        navController = this.findNavController()
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvRegister.setOnClickListener {
            navController.navigate(R.id.action_loginFragment_to_mainFragment)
        }

        binding.btnLoginWithEmail.setOnClickListener {
            navController.navigate(R.id.action_loginFragment_to_registerFragment)
        }
    }
}