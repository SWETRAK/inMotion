package com.inmotion.in_motion_android.data.database.entity

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "invited_friend")
data class InvitedFriend(
    @PrimaryKey(autoGenerate = false)
    val id: String,
    val friendshipId: String,
    val nickname: String,
    val bio: String,
    val invited: String
)
