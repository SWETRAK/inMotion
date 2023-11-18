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
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.dto.auth.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.UserInfoDto
import com.inmotion.in_motion_android.data.dto.user.FullUserInfoDto
import com.inmotion.in_motion_android.data.dto.user.UserProfileVideoDto
import com.inmotion.in_motion_android.data.repository.AuthenticationRepository
import com.inmotion.in_motion_android.data.repository.RepositoryCallback
import com.inmotion.in_motion_android.data.repository.UserRepository
import com.inmotion.in_motion_android.database.dao.UserInfoDao
import com.inmotion.in_motion_android.databinding.FragmentLoginBinding
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch

class LoginFragment : Fragment() {

    private lateinit var binding: FragmentLoginBinding
    private lateinit var navController: NavController
    private lateinit var userInfoDao: UserInfoDao
    private lateinit var authenticationRepository: AuthenticationRepository
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentLoginBinding.inflate(layoutInflater)
        navController = this.findNavController()
        userInfoDao = (activity?.application as InMotionApp).db.userInfoDao()
        authenticationRepository = AuthenticationRepository(userInfoDao)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        lifecycleScope.launch {
            userInfoDao.get().collect { info ->
                if (info != null) {
                    authenticationRepository.validateAndRefreshUser(
                        info,
                        object : RepositoryCallback<UserInfoDto> {
                            override fun onResponse(response: UserInfoDto) {
                                Toast.makeText(
                                    activity,
                                    "Hello there ${response.nickname}!",
                                    Toast.LENGTH_SHORT
                                ).show()
                                navController.navigate(R.id.action_loginFragment_to_mainFragment)
                            }

                            override fun onFailure() {
                                Toast.makeText(activity, "You have to login!", Toast.LENGTH_SHORT)
                                    .show()
                            }
                        })
                }
            }

        }

        binding.tvRegister.setOnClickListener {
            navController.navigate(R.id.action_loginFragment_to_registerFragment)
        }

        binding.btnLoginWithEmail.setOnClickListener {
            val email = binding.etEmail.text.toString()
            val password = binding.etPassword.text.toString()
            val loginUserWithEmailAndPasswordDto = LoginUserWithEmailAndPasswordDto(email, password)
            authenticationRepository.loginWithEmail(
                loginUserWithEmailAndPasswordDto,
                object : RepositoryCallback<UserInfoDto> {
                    override fun onResponse(response: UserInfoDto) {
                        Toast.makeText(activity, response.nickname, Toast.LENGTH_LONG).show()

                        GlobalScope.launch {
                            updateUserInfo(response)
                        }

                        val userRepository = UserRepository()
                        userRepository.getFullUserInfoById(response.id, "Bearer ${response.token}",
                            object : RepositoryCallback<FullUserInfoDto> {
                                override fun onResponse(response: FullUserInfoDto) {
                                    val bundle = Bundle()
                                    bundle.putSerializable("USER", response)
                                    navController.navigate(
                                        R.id.action_loginFragment_to_mainFragment,
                                        bundle
                                    )
                                }

                                override fun onFailure() {
                                    Toast.makeText(
                                        activity,
                                        "Couldn't fetch user data!",
                                        Toast.LENGTH_SHORT
                                    ).show()
                                }
                            })
                    }

                    override fun onFailure() {
                        Toast.makeText(activity, "Wrong credentials!", Toast.LENGTH_SHORT).show()
                    }
                })
        }
    }

    private suspend fun updateUserInfo(dto: UserInfoDto) {
        userInfoDao.update(dto.toUserInfo())
    }
}