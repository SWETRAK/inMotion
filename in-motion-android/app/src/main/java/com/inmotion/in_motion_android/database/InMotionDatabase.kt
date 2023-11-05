package com.inmotion.in_motion_android.database

import androidx.room.Database
import androidx.room.RoomDatabase
import com.inmotion.in_motion_android.database.dao.JwtTokenDao
import com.inmotion.in_motion_android.database.entity.JwtToken

@Database(entities = [JwtToken::class], version = 1)
abstract class InMotionDatabase: RoomDatabase() {
    abstract fun jwtTokenDao(): JwtTokenDao
}