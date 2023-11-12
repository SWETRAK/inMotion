package com.inmotion.in_motion_android.fragment.authentication

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.dto.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.repository.AuthenticationRepository
import com.inmotion.in_motion_android.databinding.FragmentRegisterBinding

class RegisterFragment : Fragment() {

    private lateinit var binding: FragmentRegisterBinding
    private lateinit var navController: NavController
    private val authenticationRepository: AuthenticationRepository = AuthenticationRepository()
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentRegisterBinding.inflate(layoutInflater)
        navController = this.findNavController()
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvLogin.setOnClickListener {
            navController.navigate(R.id.action_registerFragment_to_loginFragment2)
        }

        binding.btnRegister.setOnClickListener {
            val nickname = binding.etNickname.text.toString()
            val email = binding.etEmail.text.toString()
            val password = binding.etPassword.text.toString()
            val repeatPassword = binding.etRepeatPassword.text.toString()
            val registerUserWithEmailAndPasswordDto =
                RegisterUserWithEmailAndPasswordDto(email, password, repeatPassword, nickname)
            if(authenticationRepository.registerWithEmail(registerUserWithEmailAndPasswordDto, activity))
                navController.navigate(R.id.action_loginFragment_to_mainFragment)
        }
    }
}