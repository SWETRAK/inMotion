package com.inmotion.in_motion_android.data.database.entity

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "user_info")
data class UserInfo(

    @PrimaryKey(autoGenerate = false)
    val id: String,
    val email: String,
    val nickname: String,
    val token: String
)