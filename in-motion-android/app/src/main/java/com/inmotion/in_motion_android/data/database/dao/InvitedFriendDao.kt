package com.inmotion.in_motion_android.data.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import com.inmotion.in_motion_android.data.database.entity.InvitedFriend
import kotlinx.coroutines.flow.Flow

@Dao
interface InvitedFriendDao {

    @Query("SELECT * FROM invited_friend")
    fun getAll(): Flow<List<InvitedFriend>>

    @Query("SELECT * FROM invited_friend WHERE id = :id")
    fun getById(id: String): Flow<InvitedFriend>

    @Delete
    suspend fun delete(invitedFriend: InvitedFriend)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun upsert(invitedFriend: InvitedFriend)

    @Transaction
    suspend fun upsertAll(invitedFriends: List<InvitedFriend>) {
        invitedFriends.forEach {
            upsert(it)
        }
    }

    @Query("DELETE FROM invited_friend")
    suspend fun deleteAll()
}