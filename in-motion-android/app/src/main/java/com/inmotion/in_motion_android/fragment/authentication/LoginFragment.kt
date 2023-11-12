package com.inmotion.in_motion_android.fragment.authentication

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.dto.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.repository.AuthenticationRepository
import com.inmotion.in_motion_android.databinding.FragmentLoginBinding

class LoginFragment : Fragment() {

    private lateinit var binding: FragmentLoginBinding
    private lateinit var navController: NavController
    private val authenticationRepository: AuthenticationRepository  = AuthenticationRepository(
        (activity?.application as InMotionApp).db.jwtTokenDao()
    )
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentLoginBinding.inflate(layoutInflater)
        navController = this.findNavController()

        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvRegister.setOnClickListener {
            navController.navigate(R.id.action_loginFragment_to_registerFragment)
        }

        binding.btnLoginWithEmail.setOnClickListener {
            val email = binding.etEmail.text.toString()
            val password = binding.etPassword.text.toString()
            val loginUserWithEmailAndPasswordDto = LoginUserWithEmailAndPasswordDto(email, password)
            if (authenticationRepository.loginWithEmail(loginUserWithEmailAndPasswordDto, activity))
                navController.navigate(R.id.action_loginFragment_to_mainFragment)
        }
    }
}