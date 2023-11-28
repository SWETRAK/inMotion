package com.inmotion.in_motion_android.fragment.authentication

import android.app.ProgressDialog
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.google.android.gms.common.SignInButton
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.database.UserEvent
import com.inmotion.in_motion_android.data.remote.api.ApiConstants
import com.inmotion.in_motion_android.data.remote.api.ImsAuthApi
import com.inmotion.in_motion_android.data.remote.api.ImsUserApi
import com.inmotion.in_motion_android.data.remote.dto.auth.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.databinding.FragmentLoginBinding
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class LoginFragment : Fragment() {

    private lateinit var binding: FragmentLoginBinding
    private lateinit var navController: NavController
    private val imsUserApi: ImsUserApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsUserApi::class.java)

    private val imsAuthApi: ImsAuthApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsAuthApi::class.java)

    @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        imsUserApi
                    ) as T
                }
            }
        }
    )


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentLoginBinding.inflate(layoutInflater)
        navController = this.findNavController()
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        val dialog = ProgressDialog.show(activity, "", "Loading...", true)
        dialog.show()
        runBlocking {
            val stateUser = userViewModel.state.value.user
            if (stateUser != null) {
                val response = imsAuthApi.getUser(mapOf(Pair("Authentication", stateUser.token)))
                if (response.code() < 400) {
                    val responseUser = response.body()!!.data
                    userViewModel.onEvent(UserEvent.SetToken(responseUser.token))
                    userViewModel.onEvent(UserEvent.SaveUser)
                    activity?.runOnUiThread {
                        Toast.makeText(
                            activity,
                            "Welcome back ${responseUser.nickname}!",
                            Toast.LENGTH_SHORT
                        ).show()
                    }


                    navController.navigate(R.id.action_loginFragment_to_mainFragment)
                } else {
                    activity?.runOnUiThread {
                        Toast.makeText(
                            activity,
                            "Session expired, please login again.",
                            Toast.LENGTH_LONG
                        ).show()
                    }
                }
            }
            dialog.cancel()
        }

        binding.tvRegister.setOnClickListener()
        {
            navController.navigate(R.id.action_loginFragment_to_registerFragment)
        }

        binding.btnLoginWithEmail.setOnClickListener()
        {
            val loadingDialog = ProgressDialog.show(activity, "", "Loading...", true)
            loadingDialog.show()
            val email = binding.etEmail.text.toString()
            val password = binding.etPassword.text.toString()
            val loginUserWithEmailAndPasswordDto = LoginUserWithEmailAndPasswordDto(email, password)

            lifecycleScope.launch(Dispatchers.IO) {
                val response = imsAuthApi.loginWithEmail(loginUserWithEmailAndPasswordDto)
                if(response.code() < 400) {
                    val userInfoDto = response.body()!!.data
                    userViewModel.onEvent(UserEvent.SetUser(userInfoDto.toUserInfo()))
                    userViewModel.onEvent(UserEvent.UpdateFullUserInfo)

                    loadingDialog.cancel()

                    activity?.runOnUiThread {
                        Toast.makeText(activity, "Successfully logged in!", Toast.LENGTH_SHORT).show()
                        navController.navigate(R.id.action_loginFragment_to_mainFragment)
                    }
                } else {
                    activity?.runOnUiThread {
                        Toast.makeText(activity, "Wrong credentials!", Toast.LENGTH_SHORT).show()
                    }
                    loadingDialog.cancel()
                }
            }
        }

        binding.btnLoginWithGoogle.setSize(SignInButton.SIZE_WIDE)
        binding.btnLoginWithGoogle.setColorScheme(SignInButton.COLOR_LIGHT)

        binding.btnLoginWithGoogle.setOnClickListener {
            TODO("LOGIC IMPLEMENTATION")
        }
    }
}