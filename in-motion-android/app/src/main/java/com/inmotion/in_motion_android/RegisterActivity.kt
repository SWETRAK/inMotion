package com.inmotion.in_motion_android

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.inmotion.in_motion_android.databinding.ActivityRegisterBinding

class RegisterActivity : AppCompatActivity() {

    var binding: ActivityRegisterBinding? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRegisterBinding.inflate(layoutInflater)
        setContentView(binding?.root)

        binding?.tvLogin?.setOnClickListener {
            finish()
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        binding = null
    }
}