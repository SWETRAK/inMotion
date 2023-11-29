package com.inmotion.in_motion_android.data.database.entity

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "requested_friend")
data class RequestedFriend(
    @PrimaryKey(autoGenerate = false)
    val id: String,
    val friendshipId: String,
    val nickname: String,
    val bio: String,
    val requested: String
)
