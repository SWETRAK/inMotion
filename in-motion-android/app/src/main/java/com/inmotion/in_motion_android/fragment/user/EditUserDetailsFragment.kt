package com.inmotion.in_motion_android.fragment.user

import android.app.ActionBar.LayoutParams
import android.app.Dialog
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
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.database.event.UserEvent
import com.inmotion.in_motion_android.data.remote.ApiConstants
import com.inmotion.in_motion_android.databinding.DialogEditBioBinding
import com.inmotion.in_motion_android.databinding.DialogEditEmailBinding
import com.inmotion.in_motion_android.databinding.DialogEditNicknameBinding
import com.inmotion.in_motion_android.databinding.DialogEditPasswordBinding
import com.inmotion.in_motion_android.databinding.FragmentEditUserDetailsBinding
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class EditUserDetailsFragment : Fragment() {

    private lateinit var binding: FragmentEditUserDetailsBinding

    private val imsUsersApi: com.inmotion.in_motion_android.data.remote.api.ImsUsersApi =
        Retrofit.Builder()
            .baseUrl(ApiConstants.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
            .create(com.inmotion.in_motion_android.data.remote.api.ImsUsersApi::class.java)

    private val imsAuthApi: com.inmotion.in_motion_android.data.remote.api.ImsAuthApi =
        Retrofit.Builder()
            .baseUrl(ApiConstants.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
            .create(com.inmotion.in_motion_android.data.remote.api.ImsAuthApi::class.java)

    @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        imsUsersApi
                    ) as T
                }
            }
        }
    )

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentEditUserDetailsBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        binding.editUserDetailsToolbar.setLogo(R.drawable.ic_in_motion_logo)
        binding.editUserDetailsToolbar.setNavigationOnClickListener {
            activity?.onBackPressed()
        }

        binding.tvNickname.text = userViewModel.user.value?.nickname
        binding.tvBio.text = userViewModel.fullUserInfo.value?.bio
        binding.tvEmail.text = userViewModel.user.value?.email

        binding.btnEditEmail.setOnClickListener {
            showEditEmailDialog()
        }

        binding.btnEditPassword.setOnClickListener {
            showEditPasswordDialog()
        }

        binding.btnEditNickname.setOnClickListener {
            showEditNicknameDialog()
        }

        binding.btnEditBio.setOnClickListener {
            showEditBioDialog()
        }
    }

    private fun showEditEmailDialog() {
        val dialog = Dialog(this.requireActivity())
        val dialogBinding = DialogEditEmailBinding.inflate(layoutInflater)

        dialogBinding.etEmail.setText(userViewModel.user.value?.email)

        dialogBinding.btnSaveEmail.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                val userState = userViewModel.user.value
                val response = imsAuthApi.updateEmail(
                    "Bearer ${userState!!.token}",
                    com.inmotion.in_motion_android.data.remote.dto.auth.UpdateEmailDto(dialogBinding.etEmail.text.toString())
                )

                if (response.code() < 400) {
                    userViewModel.onEvent(UserEvent.SaveUser(response.body()!!.data.toUserInfo()))
                    Toast.makeText(activity, "Successfully updated email!", Toast.LENGTH_SHORT)
                        .show()
                    dialog.cancel()
                } else {
                    showErrorToast(response.code())
                }
            }
        }

        dialog.setContentView(dialogBinding.root)
        dialog.window?.setLayout(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT)
        dialog.show()
    }

    private fun showEditBioDialog() {
        val dialog = Dialog(this.requireActivity())
        val dialogBinding = DialogEditBioBinding.inflate(layoutInflater)

        dialogBinding.etBio.setText(userViewModel.fullUserInfo.value?.bio)

        dialogBinding.btnSaveBio.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                val response = imsUsersApi.updateLoggedInUserBio(
                    "Bearer ${userViewModel.user.value?.token}",
                    com.inmotion.in_motion_android.data.remote.dto.user.UpdateUserBioDto(
                        dialogBinding.etBio.text.toString()
                    )
                )

                if (response.code() < 400) {
                    userViewModel.onEvent(UserEvent.UpdateFullUserInfo)
                    activity?.runOnUiThread {
                        Toast.makeText(activity, "BIO successfully updated!", Toast.LENGTH_SHORT)
                            .show()
                    }
                } else {
                    showErrorToast(response.code())
                }

                dialog.cancel()
            }
        }

        dialog.setContentView(dialogBinding.root)
        dialog.window?.setLayout(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT)
        dialog.show()
    }

    private fun showEditNicknameDialog() {
        val dialog = Dialog(this.requireActivity())
        val dialogBinding = DialogEditNicknameBinding.inflate(layoutInflater)
        dialogBinding.etNickname.setText(userViewModel.user.value?.nickname)
        dialogBinding.btnSaveNickname.setOnClickListener {
            lifecycleScope.launch {
                val response = imsAuthApi.updateNickname(
                    "Bearer ${userViewModel.user.value?.token}",
                    com.inmotion.in_motion_android.data.remote.dto.auth.UpdateNicknameDto(
                        dialogBinding.etNickname.text.toString()
                    )
                )

                if (response.code() < 400) {
                    userViewModel.onEvent(UserEvent.SaveUser(response.body()!!.data.toUserInfo()))
                    Toast.makeText(activity, "Nickname successfully updated!", Toast.LENGTH_SHORT)
                        .show()
                } else {
                    showErrorToast(response.code())
                }
            }
            dialog.cancel()
        }

        dialog.setContentView(dialogBinding.root)
        dialog.window?.setLayout(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT)
        dialog.show()
    }

    private fun showEditPasswordDialog() {
        val dialog = Dialog(this.requireActivity())
        val dialogBinding = DialogEditPasswordBinding.inflate(layoutInflater)

        dialogBinding.btnSavePassword.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                val response = imsAuthApi.updatePassword(
                    "Bearer ${userViewModel.user.value?.token}",
                    com.inmotion.in_motion_android.data.remote.dto.auth.UpdatePasswordDto(
                        dialogBinding.etOldPassword.text.toString(),
                        dialogBinding.etNewPassword.text.toString(),
                        dialogBinding.etRepeatPassword.text.toString()
                    )
                )

                if (response.code() < 400) {
                    Toast.makeText(activity, "Password successfully updated!", Toast.LENGTH_SHORT)
                        .show()
                } else {
                    showErrorToast(response.code())
                }
                dialog.cancel()
            }
        }

        dialog.setContentView(dialogBinding.root)
        dialog.window?.setLayout(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT)
        dialog.show()
    }

    private fun showErrorToast(code: Int) {
        Toast.makeText(activity, "Error $code", Toast.LENGTH_SHORT).show()
    }
}