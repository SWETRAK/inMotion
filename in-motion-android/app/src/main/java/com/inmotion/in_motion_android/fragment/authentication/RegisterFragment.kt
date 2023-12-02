package com.inmotion.in_motion_android.fragment.authentication

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.remote.ApiConstants
import com.inmotion.in_motion_android.data.remote.api.ImsAuthApi
import com.inmotion.in_motion_android.data.remote.dto.auth.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.databinding.FragmentRegisterBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class RegisterFragment : Fragment() {

    private lateinit var binding: FragmentRegisterBinding
    private lateinit var navController: NavController
    private val imsAuthApi: ImsAuthApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsAuthApi::class.java)

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
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
                RegisterUserWithEmailAndPasswordDto(
                    email,
                    password,
                    repeatPassword,
                    nickname
                )

            lifecycleScope.launch(Dispatchers.IO) {
                val response = imsAuthApi.register(registerUserWithEmailAndPasswordDto)
                if (response.code() < 400) {
                    activity?.runOnUiThread {
                        Toast.makeText(
                            activity,
                            "Check your email ${response.body()!!.data.email} to finish registration!",
                            Toast.LENGTH_LONG
                        )
                            .show()
                        navController.navigate(R.id.action_registerFragment_to_loginFragment2)
                    }

                } else {
                    activity?.runOnUiThread {
                        Toast.makeText(activity, "Invalid data provided!", Toast.LENGTH_SHORT)
                            .show()
                    }
                }
            }
        }
    }
}