package com.inmotion.in_motion_android.data.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import com.inmotion.in_motion_android.data.database.entity.UserInfo
import kotlinx.coroutines.flow.Flow

@Dao
interface UserInfoDao {

    @Query("SELECT * FROM user_info LIMIT 1")
    fun get(): Flow<UserInfo>

    @Query("SELECT * FROM user_info WHERE nickname = :nickname")
    fun getByNickname(nickname: String): Flow<UserInfo>

    @Insert(entity = UserInfo::class, onConflict = OnConflictStrategy.REPLACE)
    suspend fun upsertUser(userInfo: UserInfo)

    @Query("DELETE FROM user_info")
    suspend fun deleteAll()

    @Delete
    suspend fun delete(userInfo: UserInfo)
}