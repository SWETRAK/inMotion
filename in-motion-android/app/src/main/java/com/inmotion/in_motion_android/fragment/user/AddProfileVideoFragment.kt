package com.inmotion.in_motion_android.fragment.user

import android.app.ProgressDialog
import android.content.ContentResolver
import android.content.ContentValues
import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.database.Cursor
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.os.Environment
import android.provider.MediaStore
import android.provider.Settings
import android.util.Log
import android.view.LayoutInflater
import android.view.Surface.ROTATION_90
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.activity.result.ActivityResultLauncher
import androidx.activity.result.contract.ActivityResultContracts
import androidx.camera.core.CameraSelector
import androidx.camera.core.Preview
import androidx.camera.core.resolutionselector.AspectRatioStrategy
import androidx.camera.core.resolutionselector.ResolutionSelector
import androidx.camera.lifecycle.ProcessCameraProvider
import androidx.camera.video.MediaStoreOutputOptions
import androidx.camera.video.Quality
import androidx.camera.video.QualitySelector
import androidx.camera.video.Recorder
import androidx.camera.video.Recording
import androidx.camera.video.VideoCapture
import androidx.camera.video.VideoRecordEvent
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.remote.ApiConstants
import com.inmotion.in_motion_android.data.remote.ApiUtils
import com.inmotion.in_motion_android.databinding.FragmentAddProfileVideoBinding
import com.inmotion.in_motion_android.state.UserViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.MediaType
import okhttp3.MultipartBody
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.File
import java.text.SimpleDateFormat
import java.util.Locale
import java.util.concurrent.TimeUnit


class AddProfileVideoFragment : Fragment() {

    private lateinit var binding: FragmentAddProfileVideoBinding
    private var cameraSelector = CameraSelector.DEFAULT_BACK_CAMERA

    private var recording: Recording? = null
    private var videoCapture: VideoCapture<Recorder>? = null

        @Suppress("UNCHECKED_CAST")
    private val userViewModel: UserViewModel by activityViewModels(
        factoryProducer = {
            object : ViewModelProvider.Factory {
                override fun <T : ViewModel> create(modelClass: Class<T>): T {
                    return UserViewModel(
                        (activity?.application as InMotionApp).db.userInfoDao(),
                        ApiUtils.imsUsersApi
                    ) as T
                }
            }
        }
    )

    private val resultLauncher: ActivityResultLauncher<Intent> =
        registerForActivityResult(
            ActivityResultContracts.StartActivityForResult()
        ) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.R) {
                //Android is 11 (R) or above
                if (Environment.isExternalStorageManager()) {
                    //Manage External Storage Permissions Granted
                    Log.d(
                        "ADD_PROFILE_VIDEO_FRAGMENT",
                        "onActivityResult: Manage External Storage Permissions Granted"
                    )
                } else {
                    Toast.makeText(
                        activity,
                        "Storage Permissions Denied",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            } else {
                //Below android 11
            }
        }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        if (allPermissionsGranted()) {
            startCamera()
        } else {
            requestForStoragePermissions()
            requestPermissions(CAMERAX_PERMISSIONS, REQUEST_CODE_PERMISSIONS)
        }

        binding = FragmentAddProfileVideoBinding.inflate(layoutInflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.btnFlip.setOnClickListener {
            flipCamera()
        }

        binding.btnRecord.setOnClickListener {
            captureVideo()
        }
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<String>,
        grantResults: IntArray
    ) {
        if (requestCode == STORAGE_PERMISSION_CODE) {
            if (grantResults.isNotEmpty()) {
                val write = grantResults[0] == PackageManager.PERMISSION_GRANTED
                val read = grantResults[1] == PackageManager.PERMISSION_GRANTED
                if (read && write) {
                    Toast.makeText(
                        activity,
                        "Storage Permissions Granted",
                        Toast.LENGTH_SHORT
                    ).show()
                } else {
                    Toast.makeText(
                        activity,
                        "Storage Permissions Denied",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }
        }

        if (requestCode == REQUEST_CODE_PERMISSIONS) {
            if (allPermissionsGranted()) {
                startCamera()
            } else {
                Toast.makeText(
                    requireActivity(),
                    "You must allow camera permissions!",
                    Toast.LENGTH_LONG
                ).show()
                activity?.onBackPressed()
            }
        }
    }

    private fun captureVideo() {
        if (recording != null) {
            recording!!.stop()
            recording = null
        }

        val name = SimpleDateFormat(
            "yyyy-MM-dd-HH-mm-ss-SSS",
            Locale.getDefault()
        ).format(System.currentTimeMillis())
        val contentValues = ContentValues()
        contentValues.put(MediaStore.MediaColumns.DISPLAY_NAME, name)
        contentValues.put(MediaStore.MediaColumns.MIME_TYPE, "video/mp4")
        contentValues.put(MediaStore.Video.Media.RELATIVE_PATH, "videos/CameraX-Recorder")
        val options = MediaStoreOutputOptions.Builder(
            requireActivity().contentResolver,
            MediaStore.Video.Media.EXTERNAL_CONTENT_URI
        ).setContentValues(contentValues)
            .setDurationLimitMillis(5000)
            .build()

        recording = videoCapture?.output?.prepareRecording(requireActivity(), options)
            ?.start(ContextCompat.getMainExecutor(requireActivity())
            ) {
                when (it) {
                    is VideoRecordEvent.Start -> {
                        Toast.makeText(activity, "Capture Started", Toast.LENGTH_SHORT).show()
                    }

                    is VideoRecordEvent.Finalize -> {
                        if (it.hasError()) {
                            Log.i("TUUUUUUUUUU",it.error.toString())
                            Log.i("TUUUUUUUUUU",it.cause.toString())
                            if(arrayOf(9, 10).contains(it.error)) {
                                Toast.makeText(
                                    activity,
                                    "Video capture successfull!",
                                    Toast.LENGTH_SHORT
                                ).show()

                                try {
                                    val file = File("/storage/emulated/0/videos/CameraX-Recorder/" + name + ".mp4")
                                    val loadingDialog = ProgressDialog.show(activity, "", "Loading...", true)
                                    activity?.runOnUiThread{
                                        loadingDialog.show()
                                    }
                                    lifecycleScope.launch(Dispatchers.IO){

                                        val client = OkHttpClient.Builder()
                                            .connectTimeout(10000, TimeUnit.SECONDS)
                                            .writeTimeout(10000, TimeUnit.SECONDS)
                                            .readTimeout(10000, TimeUnit.SECONDS)
                                            .build()
                                        val mediaType = MediaType.parse("text/plain")
                                        val body = MultipartBody.Builder().setType(MultipartBody.FORM)
                                            .addFormDataPart("mp4File","$name.mp4",
                                                RequestBody.create(MediaType.parse("application/octet-stream"), file))
                                            .build()
                                        val request = Request.Builder()
                                            .url("https://grand-endless-hippo.ngrok-free.app/media/api/profile/video")
                                            .post(body)
                                            .addHeader("authentication", "token")
                                            .addHeader("Authorization", "Bearer ${userViewModel.user.value?.token}")
                                            .build()
                                        val response = client.newCall(request).execute()

                                        if(response.code() < 400) {
                                            activity?.runOnUiThread{
                                                Toast.makeText(requireContext(), "SUCCESSFULLY UPLOADED!", Toast.LENGTH_SHORT).show()
                                            }
                                        } else {
                                            activity?.runOnUiThread{
                                            Toast.makeText(requireContext(), "SUCCESSFULLY UPLOADED!", Toast.LENGTH_SHORT).show()
                                            }
                                        }

                                        activity?.runOnUiThread {
                                            loadingDialog.cancel()
                                            findNavController().navigate(R.id.action_addProfileVideoFragment_to_editUserDetailsFragment)
                                        }
                                    }


                                } catch (e: Exception) {
                                    Toast.makeText(activity, "Ale jaja", Toast.LENGTH_SHORT).show()
                                }

                            }
                            else {
                                Toast.makeText(activity, "Error recording!", Toast.LENGTH_LONG)
                                    .show()
                                recording?.close()
                                recording = null
                            }
                        } else {
                            Toast.makeText(
                                activity,
                                "Video capture successfull!",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                    }
                }
            }
    }

    fun getVideoPathFromURI(context: Context, contentUri: Uri?): String? {
        var cursor: Cursor? = null
        return try {
            val proj = arrayOf(MediaStore.Video.Media.DATA)
            cursor = context.contentResolver.query(contentUri!!, proj, null, null, null)
            val column_index = cursor!!.getColumnIndexOrThrow(MediaStore.Images.Media.DATA)
            cursor.moveToFirst()
            cursor.getString(column_index)
        } finally {
            cursor?.close()
        }
    }


    private fun startCamera() {
        val cameraProviderFeature = ProcessCameraProvider.getInstance(requireActivity())
        cameraProviderFeature.addListener({
            try {
                val cameraProvider: ProcessCameraProvider = cameraProviderFeature.get()

                val preview = Preview.Builder()
//                    .setTargetFrameRate(Range(20, 30))
                    .setResolutionSelector(
                        ResolutionSelector.Builder().setAspectRatioStrategy(
                            AspectRatioStrategy.RATIO_16_9_FALLBACK_AUTO_STRATEGY
                        ).build()
                    )
                    .setTargetRotation(ROTATION_90)
                    .build()
                    .also { mPreview ->
                        mPreview.setSurfaceProvider(binding.viewFinder.surfaceProvider)
                    }
                val recorder = Recorder.Builder()
                    .setQualitySelector(QualitySelector.from(Quality.HD))
                    .build()

                videoCapture = VideoCapture.withOutput(recorder)

                cameraProvider.unbindAll()
                val camera = cameraProvider.bindToLifecycle(
                    requireActivity(), cameraSelector, preview, videoCapture
                )
            } catch (e: Exception) {
                Log.d("ADD_PROFILE_VIDEO_FRAGMENT", "Start camera failed!", e)
            }
        }, ContextCompat.getMainExecutor(requireActivity()))
    }

    private fun flipCamera() {
        cameraSelector = if (cameraSelector == CameraSelector.DEFAULT_BACK_CAMERA)
            CameraSelector.DEFAULT_FRONT_CAMERA
        else
            CameraSelector.DEFAULT_BACK_CAMERA

        startCamera()
    }

    private fun allPermissionsGranted() =
        CAMERAX_PERMISSIONS.all {
            ContextCompat.checkSelfPermission(
                requireActivity(), it
            ) == PackageManager.PERMISSION_GRANTED
        } && checkStoragePermissions()

    private fun checkStoragePermissions(): Boolean {
        return if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.R) {
            //Android is 11 (R) or above
            Environment.isExternalStorageManager()
        } else {
            //Below android 11
            val write = ContextCompat.checkSelfPermission(requireContext(), android.Manifest.permission.WRITE_EXTERNAL_STORAGE)
            val read = ContextCompat.checkSelfPermission(requireContext(), android.Manifest.permission.READ_EXTERNAL_STORAGE)
            read == PackageManager.PERMISSION_GRANTED && write == PackageManager.PERMISSION_GRANTED
        }
    }

    private fun requestForStoragePermissions() {
        //Android is 11 (R) or above
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.R) {
            try {
                val intent = Intent()
                intent.setAction(Settings.ACTION_MANAGE_APP_ALL_FILES_ACCESS_PERMISSION)
                val uri = Uri.fromParts("package", activity?.packageName, null)
                intent.setData(uri)
                resultLauncher.launch(intent)
            } catch (e: java.lang.Exception) {
                val intent = Intent()
                intent.setAction(Settings.ACTION_MANAGE_ALL_FILES_ACCESS_PERMISSION)
                resultLauncher.launch(intent)
            }
        } else {
            //Below android 11
            requestPermissions(arrayOf(
                    android.Manifest.permission.WRITE_EXTERNAL_STORAGE,
                    android.Manifest.permission.READ_EXTERNAL_STORAGE
                ),
                STORAGE_PERMISSION_CODE
            )
        }
    }

    companion object {
        private val CAMERAX_PERMISSIONS = arrayOf(
            android.Manifest.permission.CAMERA,
            android.Manifest.permission.RECORD_AUDIO
        )

        const val REQUEST_CODE_PERMISSIONS = 420
        const val STORAGE_PERMISSION_CODE = 2137
    }
}