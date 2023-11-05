package com.inmotion.in_motion_android.fragment.authorisation

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.google.gson.GsonBuilder
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.api.ApiConstants
import com.inmotion.in_motion_android.api.ImsAuthApi
import com.inmotion.in_motion_android.data.ImsHttpMessage
import com.inmotion.in_motion_android.data.LoggedInUserDto
import com.inmotion.in_motion_android.data.LoginWithEmailDto
import com.inmotion.in_motion_android.databinding.FragmentLoginBinding
import okhttp3.OkHttpClient
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class LoginFragment : Fragment() {

    private lateinit var binding: FragmentLoginBinding
    private lateinit var navController: NavController

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
            loginWithEmail()
        }
    }

    private fun loginWithEmail() {
        val email = binding.etEmail.text.toString()
        val password = binding.etPassword.text.toString()

        val gson = GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ssZ").create();
        val httpClient = OkHttpClient.Builder().build()

        val retrofit = Retrofit.Builder()
            .baseUrl(ApiConstants.AUTH_BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(httpClient)
            .build();

        val imsAuthApi = retrofit.create(ImsAuthApi::class.java)
        val loginWithEmailDto = LoginWithEmailDto(email, password)

        imsAuthApi.loginWithEmail(loginWithEmailDto).enqueue(object : Callback<ImsHttpMessage<LoggedInUserDto>> {
            override fun onResponse(
                call: Call<ImsHttpMessage<LoggedInUserDto>>,
                response: Response<ImsHttpMessage<LoggedInUserDto>>
            ) {
                if(response.code() >= 400) {
                    return
                }

                val loggedInUserDto = response.body()?.data
                Toast.makeText(activity, loggedInUserDto?.nickname, Toast.LENGTH_LONG).show()
                navController.navigate(R.id.action_loginFragment_to_mainFragment)
            }

            override fun onFailure(
                call: Call<ImsHttpMessage<LoggedInUserDto>>,
                t: Throwable
            ) {
                Toast.makeText(activity, t.message, Toast.LENGTH_SHORT).show()
            }
        })
    }
}