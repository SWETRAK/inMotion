package com.inmotion.in_motion_android.data.database

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import com.inmotion.in_motion_android.data.database.dao.AcceptedFriendDao
import com.inmotion.in_motion_android.data.database.dao.InvitedFriendDao
import com.inmotion.in_motion_android.data.database.dao.RequestedFriendDao
import com.inmotion.in_motion_android.data.database.dao.UserInfoDao
import com.inmotion.in_motion_android.data.database.entity.AcceptedFriend
import com.inmotion.in_motion_android.data.database.entity.InvitedFriend
import com.inmotion.in_motion_android.data.database.entity.RequestedFriend
import com.inmotion.in_motion_android.data.database.entity.UserInfo

@Database(
    entities = [
        UserInfo::class,
        AcceptedFriend::class,
        InvitedFriend::class,
        RequestedFriend::class
    ],
    version = 1
)
abstract class InMotionDatabase : RoomDatabase() {
    abstract fun userInfoDao(): UserInfoDao
    abstract fun acceptedFriendDao(): AcceptedFriendDao
    abstract fun invitedFriendDao(): InvitedFriendDao
    abstract fun requestedFriendDao(): RequestedFriendDao

    companion object {

        @Volatile
        private var INSTANCE: InMotionDatabase? = null

        fun getInstance(context: Context): InMotionDatabase {
            synchronized(this) {
                var instance = INSTANCE
                if (instance == null) {
                    instance = Room.databaseBuilder(
                        context.applicationContext, InMotionDatabase::class.java,
                        "in_motion_database"
                    ).fallbackToDestructiveMigration().build()
                    INSTANCE = instance
                }
                return instance
            }
        }
    }
}