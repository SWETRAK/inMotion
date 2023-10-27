package com.inmotion.in_motion_android

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.inmotion.in_motion_android.databinding.ActivityInMotionHostBinding

class InMotionHostActivity : AppCompatActivity() {

    private var binding: ActivityInMotionHostBinding? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityInMotionHostBinding.inflate(layoutInflater)
        setContentView(binding?.root)
    }
}