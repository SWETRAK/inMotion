package com.inmotion.in_motion_android.data.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend
import kotlinx.coroutines.flow.Flow

@Dao
interface RequestedFriendDao {

    @Query("SELECT * FROM requested_friend")
    fun getAll(): Flow<List<RequestedFriend>>

    @Query("SELECT * FROM requested_friend WHERE id = :id")
    fun getById(id: String): Flow<RequestedFriend>

    @Delete
    suspend fun delete(requestedFriend: RequestedFriend)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun upsert(requestedFriend: RequestedFriend)

    @Transaction
    suspend fun upsertAll(requestedFriends: List<RequestedFriend>) {
        requestedFriends.forEach {
            upsert(it)
        }
    }
    @Query("DELETE FROM requested_friend")
    suspend fun deleteAll()
}