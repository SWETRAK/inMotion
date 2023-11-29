package com.inmotion.in_motion_android.data.database.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import kotlinx.coroutines.flow.Flow

@Dao
interface AcceptedFriendDao {

    @Query("SELECT * FROM accepted_friend")
    fun getAll(): Flow<List<AcceptedFriend>>

    @Query("SELECT * FROM accepted_friend WHERE id = :id")
    fun getById(id: String): Flow<AcceptedFriend>

    @Delete
    suspend fun delete(acceptedFriend: AcceptedFriend)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun upsert(acceptedFriend: AcceptedFriend)

    @Transaction
    suspend fun upsertAll(acceptedFriends: List<AcceptedFriend>) {
        acceptedFriends.forEach {
            upsert(it)
        }
    }

    @Query("DELETE FROM accepted_friend")
    suspend fun deleteAll()
}