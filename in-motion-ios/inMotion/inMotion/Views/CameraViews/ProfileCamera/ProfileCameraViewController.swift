//
//  ProfileCameraViewController.swift
//  inMotion
//
//  Created by Kamil Pietrak on 18/11/2023.
//

import Foundation
import UIKit
import AVKit
import AVFoundation
import Photos

class ProfileCameraViewController: UIViewController, AVCaptureFileOutputRecordingDelegate {
    
    var captureSession: AVCaptureSession!
    var videoPreviewLayer: AVCaptureVideoPreviewLayer!
    var movieOutput = AVCaptureMovieFileOutput()
    var videoCaptureDevice : AVCaptureDevice?
    var appState: AppState?
    
    var session: AVCaptureSession?
    
    @IBOutlet weak var previewView: UIView!
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        addPrivilagesToCameras()
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        self.captureSession.stopRunning()
    }
    
    func addPrivilagesToCameras() {
        AVCaptureDevice.requestAccess(for: AVMediaType.video) { response in
            if response {
                self.prepareCameras()
            } else {
                self.goBackToUserPage()
            }
        }
    }
    
    func prepareCameras() {
        self.captureSession = AVCaptureSession();
        self.captureSession.sessionPreset = .hd1280x720
        guard let frontCamera = AVCaptureDevice.default(for: AVMediaType.video) else {
            print("Unable to access back camera!")
            return
        }
        
        do {
            let input = try AVCaptureDeviceInput(device: frontCamera)
            if captureSession.canAddInput(input) && captureSession.canAddOutput(movieOutput) {
                captureSession.addInput(input)
                captureSession.addOutput(movieOutput)
                setupLivePreview()
                
                if movieOutput.availableVideoCodecTypes.contains(.h264),  let connection = movieOutput.connection(with: .video) {
                    // Use the H.264 codec to encode the video.
                    movieOutput.setOutputSettings([AVVideoCodecKey: AVVideoCodecType.h264], for: connection)
                }
            }
        } catch let error {
            print ("Error Unable to initialize back camera:  \(error.localizedDescription)")
        }
    }
    
    func setupLivePreview() {
        videoPreviewLayer = AVCaptureVideoPreviewLayer(session: captureSession)
        
        videoPreviewLayer.videoGravity = .resizeAspect
        videoPreviewLayer.connection?.videoOrientation = .portrait
        
        previewView.layer.addSublayer(videoPreviewLayer)
        
        
        DispatchQueue.global(qos: .userInitiated).async {
            self.captureSession.startRunning()
            DispatchQueue.main.async {
                self.videoPreviewLayer.frame = self.previewView.bounds
            }
        }
    }
    
    func goBackToUserPage() {
        
    }
    
    @IBAction func recordVideoAction(_ sender: UIButton) {
        if movieOutput.isRecording {
            movieOutput.stopRecording()
        } else {
            let paths = FileManager.default.urls(for: .documentDirectory, in: .userDomainMask)
            let fileUrl = paths[0].appendingPathComponent("\(UUID().uuidString.lowercased()).mp4")
            try? FileManager.default.removeItem(at: fileUrl)
            self.movieOutput.startRecording(to: fileUrl, recordingDelegate: self as AVCaptureFileOutputRecordingDelegate)
            print("Rozpoczeto nagrywanie")
            Timer.scheduledTimer(withTimeInterval: 5, repeats: false, block: { _ in
                print("Zakończono nagrywanie")
                self.movieOutput.stopRecording()
            })
        }
    }
    
    func fileOutput(_ output: AVCaptureFileOutput, didFinishRecordingTo outputFileURL: URL, from connections: [AVCaptureConnection], error: Error?) {
        if error == nil {
            self.appState?.uploadProfileVideoAlamofire(
                filePath: outputFileURL,
                onSuccess: { (data: ProfileVideoUploadInfoDto) in
                    self.goBackToUserPage()
                },
                onFailure: { (error: ImsHttpError) in })
        }
    }
}
