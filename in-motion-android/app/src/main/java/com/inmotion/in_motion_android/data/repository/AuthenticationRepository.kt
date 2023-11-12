package com.inmotion.in_motion_android.data.repository

import android.content.Context
import android.widget.Toast
import com.inmotion.in_motion_android.data.remote.ImsAuthApi
import com.inmotion.in_motion_android.data.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.dto.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.SuccessfullRegistrationResponseDto
import com.inmotion.in_motion_android.data.dto.UserInfoDto
import com.inmotion.in_motion_android.database.dao.UserInfoDao
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class AuthenticationRepository(val dao: UserInfoDao) {

    private val imsAuthApi: ImsAuthApi = Retrofit.Builder()
        .baseUrl(ApiConstants.AUTH_BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsAuthApi::class.java)

    fun loginWithEmail(loginUserWithEmailAndPasswordDto: LoginUserWithEmailAndPasswordDto,
                       activity: Context?): Boolean {
        var isLoggedIn = false;

        imsAuthApi.loginWithEmail(loginUserWithEmailAndPasswordDto).enqueue(object :
            Callback<ImsHttpMessage<UserInfoDto>> {
            override fun onResponse(
                call: Call<ImsHttpMessage<UserInfoDto>>,
                response: Response<ImsHttpMessage<UserInfoDto>>
            ) {
                if (response.code() >= 400) {
                    isLoggedIn = false;
                    Toast.makeText(activity, "Wrong credentials!", Toast.LENGTH_SHORT).show()
                    return
                }

                val loggedInUserDto = response.body()?.data

                loggedInUserDto?.let {
                    dao.insert(it.toUserInfo())
                    Toast.makeText(activity, it.nickname, Toast.LENGTH_LONG).show()
                    isLoggedIn = true;
                }

            }

            override fun onFailure(
                call: Call<ImsHttpMessage<UserInfoDto>>,
                t: Throwable
            ) {
                Toast.makeText(activity, t.message, Toast.LENGTH_SHORT).show()
                isLoggedIn = false;
            }
        })

        return isLoggedIn;
    }

    fun registerWithEmail(registerUserWithEmailAndPasswordDto: RegisterUserWithEmailAndPasswordDto,
                          activity: Context?): Boolean {
        var isRegistered = false

        imsAuthApi.register(registerUserWithEmailAndPasswordDto).enqueue(object :
            Callback<ImsHttpMessage<SuccessfullRegistrationResponseDto>> {
            override fun onResponse(
                call: Call<ImsHttpMessage<SuccessfullRegistrationResponseDto>>,
                response: Response<ImsHttpMessage<SuccessfullRegistrationResponseDto>>
            ) {
                if (response.code() >= 400) {
                    isRegistered = false
                    return
                }

                val successfullRegistrationDto = response.body()?.data
                Toast.makeText(
                    activity,
                    "Activate account by link in email sent to ${successfullRegistrationDto?.email}",
                    Toast.LENGTH_LONG
                ).show()
                isRegistered = true
            }

            override fun onFailure(
                call: Call<ImsHttpMessage<SuccessfullRegistrationResponseDto>>,
                t: Throwable
            ) {
                Toast.makeText(activity, t.message, Toast.LENGTH_SHORT).show()
                isRegistered = false
            }
        })

        return isRegistered
    }
}