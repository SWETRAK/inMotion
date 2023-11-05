package com.inmotion.in_motion_android.database.entity

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.sql.Timestamp
import java.time.LocalDateTime

@Entity(tableName = "jwt_token")
data class JwtToken(

    @PrimaryKey(autoGenerate = false)
    val nickname: String,
    val token: String,
    val expiresAt: String
)
