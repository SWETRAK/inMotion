package com.inmotion.in_motion_android.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.databinding.RegisterFragmentBinding

class RegisterFragment : Fragment() {

    private lateinit var binding: RegisterFragmentBinding
    private lateinit var navController: NavController
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = RegisterFragmentBinding.inflate(layoutInflater)
        navController = this.findNavController()
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvLogin.setOnClickListener {
            navController.navigate(R.id.action_registerFragment_to_loginFragment2)
        }

        binding.btnRegister.setOnClickListener {
            navController.navigate(R.id.action_registerFragment_to_loginFragment2)
        }
    }
}