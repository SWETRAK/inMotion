package com.inmotion.in_motion_android.fragment.authentication

import android.app.ProgressDialog
import android.content.Intent
import android.os.Bundle
import android.util.Log
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
import com.google.android.gms.auth.api.signin.GoogleSignIn
import com.google.android.gms.auth.api.signin.GoogleSignInAccount
import com.google.android.gms.auth.api.signin.GoogleSignInOptions
import com.google.android.gms.common.SignInButton
import com.google.android.gms.common.api.ApiException
import com.google.android.gms.tasks.Task
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.database.event.FriendEvent
import com.inmotion.in_motion_android.data.database.event.UserEvent
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.data.remote.api.ImsAuthApi
import com.inmotion.in_motion_android.data.remote.api.ImsFriendsApi
import com.inmotion.in_motion_android.data.remote.api.ImsUserApi
import com.inmotion.in_motion_android.data.remote.dto.auth.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.databinding.FragmentLoginBinding
import com.inmotion.in_motion_android.state.FriendsViewModel
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking


class LoginFragment : Fragment() {

    private lateinit var binding: FragmentLoginBinding
    private lateinit var navController: NavController
    private val imsUserApi: ImsUserApi = ApiUtils.imsUserApi
    private val imsAuthApi: ImsAuthApi = ApiUtils.imsAuthApi
    private val imsFriendsApi: ImsFriendsApi = ApiUtils.imsFriendsApi

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

    @Suppress("UNCHECKED_CAST")
    private val friendsViewModel: FriendsViewModel by activityViewModels(
        factoryProducer = {
            object: ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return FriendsViewModel(
                        (activity?.application as InMotionApp).db.acceptedFriendDao(),
                        (activity?.application as InMotionApp).db.invitedFriendDao(),
                        (activity?.application as InMotionApp).db.requestedFriendDao(),
                        imsFriendsApi
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
        binding.tvRegister.setOnClickListener()
        {
            navController.navigate(R.id.action_loginFragment_to_registerFragment)
        }

        binding.btnLoginWithEmail.setOnClickListener()
        {
            loginWithEmail()
        }

        prepareLoginWithGoogleButton()
        checkForExistingUserAndTryToLogin()
    }

    private fun prepareLoginWithGoogleButton() {
        binding.btnLoginWithGoogle.setSize(SignInButton.SIZE_WIDE)
        binding.btnLoginWithGoogle.setColorScheme(SignInButton.COLOR_LIGHT)

        binding.btnLoginWithGoogle.setOnClickListener {
            val gso = GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestEmail()
                .build()
            val mGoogleSignInClient = GoogleSignIn.getClient(activity!!, gso);
            val signInIntent = mGoogleSignInClient.signInIntent
            startActivityForResult(signInIntent, 420)
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (requestCode == 420) {
            // The Task returned from this call is always completed, no need to attach
            // a listener.
            val task = GoogleSignIn.getSignedInAccountFromIntent(data)
            handleSignInResult(task)
        }
    }

    private fun handleSignInResult(completedTask: Task<GoogleSignInAccount>) {
        try {
            val account = completedTask.getResult(ApiException::class.java)
            Log.i("Tag", "g")
        } catch (e: ApiException) {
            // The ApiException status code indicates the detailed failure reason.
            // Please refer to the GoogleSignInStatusCodes class reference for more information.
            Log.w("LOGIN FRAGMENT", "signInResult:failed code=" + e.statusCode)
        }
    }

    private fun loginWithEmail() {
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

    private fun checkForExistingUserAndTryToLogin() {
        val dialog = ProgressDialog.show(activity, "", "Loading...", true)
        dialog.show()
        runBlocking {
            val stateUser = userViewModel.state.value.user
            if (stateUser != null) {
                val response = imsAuthApi.getUser("Bearer ${stateUser.token}")
                if (response.code() < 400) {
                    val responseUser = response.body()!!.data
                    userViewModel.onEvent(UserEvent.SetToken(responseUser.token))
                    userViewModel.onEvent(UserEvent.SaveUser)
                    friendsViewModel.onEvent(FriendEvent.FetchAcceptedFriends(responseUser.token))
                    friendsViewModel.onEvent(FriendEvent.FetchInvitedFriends(responseUser.token))
                    friendsViewModel.onEvent(FriendEvent.FetchRequestedFriends(responseUser.token))

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
    }
}