package com.inmotion.in_motion_android.database

import android.content.Context
import android.provider.CalendarContract.Instances
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import com.inmotion.in_motion_android.database.dao.UserInfoDao
import com.inmotion.in_motion_android.database.entity.UserInfo

@Database(entities = [UserInfo::class], version = 1)
abstract class InMotionDatabase: RoomDatabase() {
    abstract fun jwtTokenDao(): UserInfoDao

    companion object{

        @Volatile
        private var INSTANCE: InMotionDatabase? = null

        fun getInstance(context: Context): InMotionDatabase {
            synchronized(this) {
                var instance = INSTANCE
                if(instance == null) {
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