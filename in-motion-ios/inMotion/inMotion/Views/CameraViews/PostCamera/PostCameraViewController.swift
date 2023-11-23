//
//  ViewController.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/11/2023.
//

import Foundation
import UIKit
import AVFoundation
import Photos
import ReplayKit

class PostCameraViewController: UIViewController, AVCaptureFileOutputRecordingDelegate  {
    
    var captureSession: AVCaptureSession!
    var videoPreviewLayer: AVCaptureVideoPreviewLayer!
    var recorderVideoPrewiewLayer: AVPlayerLayer!
    var movieOutput = AVCaptureMovieFileOutput()
    var videoCaptureDevice : AVCaptureDevice?
    
    var appState: AppState?
    var postId: UUID?
    
    var frontCameraUrl: URL?
    var backCameraUrl: URL?
    
    var currentlyActiveCamera: AVCaptureDevice.Position?
    
    var session: AVCaptureSession?
    
    @IBOutlet weak var previewView: UIView!
    @IBOutlet weak var recorderVideoPrewiew: UIView!
    @IBOutlet weak var flipButton: UIButton!
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        self.recorderVideoPrewiew.isHidden = true
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        addPrivilagesToCameras()
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        self.captureSession.stopRunning()
        
         // remove object
        self.recorderVideoPrewiewLayer.player?.pause()
        self.recorderVideoPrewiewLayer.player = nil
    }
    
    func addPrivilagesToCameras() {
        AVCaptureDevice.requestAccess(for: AVMediaType.video) { response in
            if response {
                self.prepareCameras()
            } else {
                self.goToHomeScreen()
            }
        }
    }
    
    func prepareCameras() {
        self.captureSession = AVCaptureSession();
        self.captureSession.sessionPreset = .hd1280x720
        guard let backCamera = AVCaptureDevice.default(for: AVMediaType.video) else {
            print("Unable to access back camera!")
            return
        }
        
        do {
            let input = try AVCaptureDeviceInput(device: backCamera)
            if captureSession.canAddInput(input) && captureSession.canAddOutput(movieOutput) {
                self.currentlyActiveCamera = .back
                self.captureSession.addInput(input)
                self.captureSession.addOutput(self.movieOutput)
                self.setupLivePreview()
                
                if self.movieOutput.availableVideoCodecTypes.contains(.h264),  let connection = self.movieOutput.connection(with: .video) {
                    // Use the H.264 codec to encode the video.
                    self.movieOutput.setOutputSettings([AVVideoCodecKey: AVVideoCodecType.h264], for: connection)
                }
            }
        } catch let error {
            print ("Error Unable to initialize back camera:  \(error.localizedDescription)")
        }
    }
    
    func setupLivePreview() {
        self.videoPreviewLayer = AVCaptureVideoPreviewLayer(session: captureSession)
        self.videoPreviewLayer.videoGravity = .resizeAspect
        self.videoPreviewLayer.connection?.videoOrientation = .portrait
        self.previewView.layer.addSublayer(self.videoPreviewLayer)
        
        DispatchQueue.global(qos: .userInitiated).async {
            self.captureSession.startRunning()
            DispatchQueue.main.async {
                self.videoPreviewLayer.frame = self.previewView.bounds
            }
        }
    }
    
    //MARK: - SETUP RECORDER LAYER
    
    func setupRecorderLayer(displayVideo: URL) {
        self.recorderVideoPrewiewLayer = AVPlayerLayer(player: AVPlayer(url: displayVideo))
        self.recorderVideoPrewiewLayer.videoGravity = .resizeAspect
        self.recorderVideoPrewiew.layer.addSublayer(self.recorderVideoPrewiewLayer)
        DispatchQueue.main.async {
            self.recorderVideoPrewiewLayer.frame = self.recorderVideoPrewiew.bounds
            self.recorderVideoPrewiew.isHidden = false // Show recorded video preview
        }
    }
    
    // MARK: -  SWITCH CAMERAS
    
    func switchCameras() {
        let currentCameraInput: AVCaptureInput = self.captureSession.inputs[0]
        self.captureSession.removeInput(currentCameraInput)
        var newCamera: AVCaptureDevice
        newCamera = AVCaptureDevice.default(for: .video)!;
        if (currentCameraInput as! AVCaptureDeviceInput).device.position == .back {
            UIView.transition(with: self.previewView, duration: 0.5, options: .transitionFlipFromLeft, animations: {
                newCamera = self.cameraWithPosition(.front)!
            }, completion: nil)
        } else {
            UIView.transition(with: self.previewView, duration: 0.5, options: .transitionFlipFromRight, animations: {
                newCamera = self.cameraWithPosition(.back)!
            }, completion: nil)
        }
        do {
            try self.captureSession?.addInput(AVCaptureDeviceInput(device: newCamera))
        }
        catch {
            print("error: \(error.localizedDescription)")
        }
    }
    
    func cameraWithPosition(_ position: AVCaptureDevice.Position) -> AVCaptureDevice? {
        let deviceDescoverySession = AVCaptureDevice.DiscoverySession.init(deviceTypes: [AVCaptureDevice.DeviceType.builtInWideAngleCamera], mediaType: AVMediaType.video, position: AVCaptureDevice.Position.unspecified)

        for device in deviceDescoverySession.devices {
            if device.position == position {
                self.currentlyActiveCamera = position
                return device
            }
        }
        return nil
    }

    // MARK: - SAVE FILE OR SWITCH CAMERA AND START RECORDING
    
    func fileOutput(_ output: AVCaptureFileOutput, didFinishRecordingTo outputFileURL: URL, from connections: [AVCaptureConnection], error: Error?) {
        if error == nil {
            
            if (currentlyActiveCamera == .back) {
                self.backCameraUrl = outputFileURL
            } else if (currentlyActiveCamera == .front) {
                self.frontCameraUrl = outputFileURL
            }
            
            if let safeBackCameraUrl = self.backCameraUrl, let safeFrontCameraUrl = self.frontCameraUrl {
                self.appState!.uploadPostVideoAlamofire(
                    frontFilePath: safeFrontCameraUrl,
                    backFilePath: safeBackCameraUrl,
                    postId: self.postId!,
                    onSuccess: { (data: PostUploadInfoDto) in
                        self.goToHomeScreen()
                    },
                    onFailure: { (error: ImsHttpError) in
                        print(error)
                    })
            } else {
                self.switchCameras()
                Timer.scheduledTimer(withTimeInterval: 0.5, repeats: false) { timer in
                    if let safeBackCameraUrl = self.backCameraUrl {
                        self.setupRecorderLayer(displayVideo: safeBackCameraUrl)
                    } else if let safeFrontCameraUrl = self.frontCameraUrl {
                        self.setupRecorderLayer(displayVideo: safeFrontCameraUrl)
                    }

                    // Record second video and show recorded previously
                    self.recordVideo()
                    self.recorderVideoPrewiewLayer.player?.play()
                }
            }
        }
    }
    
    // MARK: - RECORD VIDEO ACTION
    
    func recordVideo() {
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
    
    // MARK: - GO TO HOME SCREEN
    
    func goToHomeScreen() {
        
    }
    
    // MARK: - BUTTONS ACTION
    
    @IBAction func recordVideoAction(_ sender: UIButton) {
        self.flipButton.isEnabled = false
        self.recordVideo()
    }
    
    @IBAction func flipCameraAction(_ sender: UIButton) {
        self.switchCameras()
    }
}
