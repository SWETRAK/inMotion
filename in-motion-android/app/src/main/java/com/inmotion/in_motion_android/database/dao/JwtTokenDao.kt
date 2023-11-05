package com.inmotion.in_motion_android.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query
import com.inmotion.in_motion_android.database.entity.JwtToken

@Dao
interface JwtTokenDao {
    @Query("SELECT * FROM jwt_token WHERE nickname = :nickname")
    fun getByNickname(nickname: String): JwtToken

    @Insert
    fun insert(record: JwtToken)

    @Delete
    fun delete(record: JwtToken)
}