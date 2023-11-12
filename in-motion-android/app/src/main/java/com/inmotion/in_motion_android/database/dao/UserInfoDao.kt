package com.inmotion.in_motion_android.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query
import com.inmotion.in_motion_android.database.entity.UserInfo

@Dao
interface UserInfoDao {
    @Query("SELECT * FROM user_info WHERE nickname = :nickname")
    fun getByNickname(nickname: String): UserInfo

    @Insert
    fun insert(record: UserInfo)

    @Delete
    fun delete(record: UserInfo)
}