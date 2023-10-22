package com.inmotion.in_motion_android

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.inmotion.in_motion_android.databinding.ActivityRegisterBinding

class RegisterActivity : AppCompatActivity() {

    private var binding: ActivityRegisterBinding? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRegisterBinding.inflate(layoutInflater)
        setContentView(binding?.root)

        binding?.tvLogin?.setOnClickListener {
            finish()
        }

        binding?.btnRegister?.setOnClickListener {
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        binding = null
    }
}