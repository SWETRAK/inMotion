package com.inmotion.in_motion_android.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query
import androidx.room.Update
import com.inmotion.in_motion_android.database.entity.UserInfo
import kotlinx.coroutines.flow.Flow

@Dao
interface UserInfoDao {

    @Query("SELECT * FROM user_info LIMIT 1")
    fun get(): Flow<UserInfo>

    @Query("SELECT * FROM user_info WHERE nickname = :nickname")
    fun getByNickname(nickname: String): Flow<UserInfo>

    @Update
    fun update(userInfo: UserInfo)

    @Insert
    fun insert(record: UserInfo)

    @Delete
    fun delete(record: UserInfo)
}