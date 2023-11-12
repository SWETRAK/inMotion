package com.inmotion.in_motion_android

import android.app.Application
import com.inmotion.in_motion_android.database.InMotionDatabase

class InMotionApp: Application() {

    val db by lazy {
        InMotionDatabase.getInstance(this)
    }
}