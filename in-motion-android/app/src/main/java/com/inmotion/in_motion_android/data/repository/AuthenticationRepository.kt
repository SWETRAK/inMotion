package com.inmotion.in_motion_android.data.repository

import com.inmotion.in_motion_android.data.dto.ImsHttpMessage
import com.inmotion.in_motion_android.data.dto.auth.LoginUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.SuccessfullRegistrationResponseDto
import com.inmotion.in_motion_android.data.dto.auth.UserInfoDto
import com.inmotion.in_motion_android.data.remote.ImsAuthApi
import com.inmotion.in_motion_android.database.dao.UserInfoDao
import com.inmotion.in_motion_android.database.entity.UserInfo
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class AuthenticationRepository(private val dao: UserInfoDao) {

    private val imsAuthApi: ImsAuthApi = Retrofit.Builder()
        .baseUrl(ApiConstants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
        .create(ImsAuthApi::class.java)

    fun loginWithEmail(
        loginUserWithEmailAndPasswordDto: LoginUserWithEmailAndPasswordDto,
        callback: RepositoryCallback<UserInfoDto>
    ) {
        imsAuthApi.loginWithEmail(loginUserWithEmailAndPasswordDto).enqueue(object :
            Callback<ImsHttpMessage<UserInfoDto>> {
            override fun onResponse(
                call: Call<ImsHttpMessage<UserInfoDto>>,
                response: Response<ImsHttpMessage<UserInfoDto>>
            ) {
                if (response.code() >= 400) {
                    return callback.onFailure()
                }

                val loggedInUserDto = response.body()?.data

                loggedInUserDto?.let {
                    GlobalScope.launch {
                        saveUser(it)
                    }
                    callback.onResponse(loggedInUserDto)

                }
            }

            override fun onFailure(
                call: Call<ImsHttpMessage<UserInfoDto>>,
                t: Throwable
            ) {
                callback.onFailure()
            }
        })
    }

    fun registerWithEmail(
        registerUserWithEmailAndPasswordDto: RegisterUserWithEmailAndPasswordDto,
        callback: RepositoryCallback<SuccessfullRegistrationResponseDto>
    ) {
        imsAuthApi.register(registerUserWithEmailAndPasswordDto).enqueue(object :
            Callback<ImsHttpMessage<SuccessfullRegistrationResponseDto>> {
            override fun onResponse(
                call: Call<ImsHttpMessage<SuccessfullRegistrationResponseDto>>,
                response: Response<ImsHttpMessage<SuccessfullRegistrationResponseDto>>
            ) {
                if (response.code() >= 400) {
                    return callback.onFailure()
                }

                val successfullRegistrationDto = response.body()?.data
                successfullRegistrationDto.let {
                    callback.onResponse(it!!)
                }

            }

            override fun onFailure(
                call: Call<ImsHttpMessage<SuccessfullRegistrationResponseDto>>,
                t: Throwable
            ) {
                callback.onFailure()
            }
        })
    }

    fun validateAndRefreshUser(
        userInfo: UserInfo,
        callback: RepositoryCallback<UserInfoDto>
    ) {
        imsAuthApi.getUser(mapOf(Pair("Authorization", "Bearer ${userInfo.token}"))).enqueue(
            object : Callback<ImsHttpMessage<UserInfoDto>> {
                override fun onResponse(
                    call: Call<ImsHttpMessage<UserInfoDto>>,
                    response: Response<ImsHttpMessage<UserInfoDto>>
                ) {
                    if (response.code() >= 400) {
                        return callback.onFailure()
                    }

                    val user = response.body()?.data

                    user.let {
                        callback.onResponse(it!!)
                    }
                }

                override fun onFailure(call: Call<ImsHttpMessage<UserInfoDto>>, t: Throwable) {
                    callback.onFailure()
                }
            }
        )
    }

    private suspend fun saveUser(userInfoDto: UserInfoDto) {
        if (dao.get() != null) {
            dao.update(userInfoDto.toUserInfo())
        } else {
            dao.insert(userInfoDto.toUserInfo())
        }
    }
}