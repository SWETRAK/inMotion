package com.inmotion.in_motion_android.fragment.authorisation

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.api.ApiConstants
import com.inmotion.in_motion_android.api.ImsAuthApi
import com.inmotion.in_motion_android.data.ImsHttpMessage
import com.inmotion.in_motion_android.data.LoggedInUserDto
import com.inmotion.in_motion_android.data.LoginWithEmailDto
import com.inmotion.in_motion_android.data.RegisterDto
import com.inmotion.in_motion_android.data.SuccessFullRegistrationDto
import com.inmotion.in_motion_android.databinding.FragmentRegisterBinding
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import retrofit2.Call
import retrofit2.Callback
import retrofit2.HttpException
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class RegisterFragment : Fragment() {

    private lateinit var binding: FragmentRegisterBinding
    private lateinit var navController: NavController
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
            register()
        }
    }

    private fun register() {
        val nickname = binding.etNickname.text.toString()
        val email = binding.etEmail.text.toString()
        val password = binding.etPassword.text.toString()
        val repeatPassword = binding.etRepeatPassword.text.toString()


        val retrofit = Retrofit.Builder()
            .baseUrl(ApiConstants.AUTH_BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build();

        val imsAuthApi = retrofit.create(ImsAuthApi::class.java)
        val registerDto = RegisterDto(email, password, repeatPassword, nickname)

        imsAuthApi.register(registerDto).enqueue(object :
            Callback<ImsHttpMessage<SuccessFullRegistrationDto>> {
            override fun onResponse(
                call: Call<ImsHttpMessage<SuccessFullRegistrationDto>>,
                response: Response<ImsHttpMessage<SuccessFullRegistrationDto>>
            ) {
                if(response.code() >= 400) {
                    return
                }

                val successfullRegistrationDto = response.body()?.data
                Toast.makeText(activity, successfullRegistrationDto?.email, Toast.LENGTH_LONG).show()
                navController.navigate(R.id.action_loginFragment_to_mainFragment)
            }

            override fun onFailure(
                call: Call<ImsHttpMessage<SuccessFullRegistrationDto>>,
                t: Throwable
            ) {
                Toast.makeText(activity, t.message, Toast.LENGTH_SHORT).show()
            }
        })
    }
}