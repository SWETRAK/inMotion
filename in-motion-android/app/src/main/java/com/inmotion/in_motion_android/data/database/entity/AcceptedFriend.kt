package com.inmotion.in_motion_android.data.database.entity

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "accepted_friend")
data class AcceptedFriend(
    @PrimaryKey(autoGenerate = false)
    val id: String,
    val friendshipId: String,
    val nickname: String,
    val bio: String,
    val friendsSince: String
)
