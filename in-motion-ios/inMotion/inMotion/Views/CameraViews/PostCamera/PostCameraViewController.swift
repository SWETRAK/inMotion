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
    
    var frontCameraUrl: URL?
    var backCameraUrl: URL?
    
    var session: AVCaptureSession?
    
    @IBOutlet weak var previewView: UIView!
    @IBOutlet weak var recorderVideoPrewiew: UIView!
    
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
                self.goBackToMainPage()
            }
        }
    }
    
    func prepareCameras() {
        self.captureSession = AVCaptureSession();
        self.captureSession.sessionPreset = .hd1920x1080
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
    
    func setupRecorderLayer(displayVideo: URL) {
        recorderVideoPrewiewLayer = AVPlayerLayer(player: AVPlayer(url: displayVideo))
        recorderVideoPrewiewLayer.videoGravity = .resizeAspect
        recorderVideoPrewiew.layer.addSublayer(recorderVideoPrewiewLayer)
        DispatchQueue.main.async {
            self.recorderVideoPrewiewLayer.frame = self.recorderVideoPrewiew.bounds
        }
    }
    
    func switchCameras() {
        let currentCameraInput: AVCaptureInput = captureSession.inputs[0]
        captureSession.removeInput(currentCameraInput)
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
    
    func goBackToMainPage() {
        
    }
    
    func cameraWithPosition(_ position: AVCaptureDevice.Position) -> AVCaptureDevice? {
        let deviceDescoverySession = AVCaptureDevice.DiscoverySession.init(deviceTypes: [AVCaptureDevice.DeviceType.builtInWideAngleCamera], mediaType: AVMediaType.video, position: AVCaptureDevice.Position.unspecified)

        for device in deviceDescoverySession.devices {
            if device.position == position {
                return device
            }
        }
        return nil
    }

    
    func fileOutput(_ output: AVCaptureFileOutput, didFinishRecordingTo outputFileURL: URL, from connections: [AVCaptureConnection], error: Error?) {
        if error == nil {
        
            // save file links
            
        }
    }
    
    func sendToServer() {
        self.appState?.uploadPostVideoAlamofire(
            frontFilePath: self.frontCameraUrl!,
            backFilePath: self.backCameraUrl!,
            postId: UUID(),
            onSuccess: { (data: PostUploadInfoDto) in
                self.goBackToMainPage()
            },
            onFailure: { (error: ImsHttpError) in
                print(error)
            })
    }
    
    // TODO: This should run automaticaly
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
}
