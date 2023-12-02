package com.inmotion.in_motion_android

import android.app.Application
import androidx.room.Room
import com.inmotion.in_motion_android.data.database.InMotionDatabase

class InMotionApp : Application() {

    val db by lazy {
        Room.databaseBuilder(
            applicationContext,
            InMotionDatabase::class.java,
            "inmotion.db"
        ).build()
    }
}