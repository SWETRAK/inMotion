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

class ViewController: UIViewController, AVCaptureVideoDataOutputSampleBufferDelegate {
    
    var cameraRecorder = AVCaptureSession()
    
    

}
//
//struct AuthorizationChecker {
//    static func checkCaptureAuthorizationStatus() async -> Status {
//        switch AVCaptureDevice.authorizationStatus(for: .video) {
//        case .authorized:
//            return .permitted
//            
//        case .notDetermined:
//            let isPermissionGranted = await AVCaptureDevice.requestAccess(for: .video)
//            if isPermissionGranted {
//                return .permitted
//            } else {
//                fallthrough
//            }
//            
//        case .denied:
//            fallthrough
//            
//        case .restricted:
//            fallthrough
//            
//        @unknown default:
//            return .notPermitted
//        }
//    }
//}
//
//extension AuthorizationChecker {
//    enum Status {
//        case permitted
//        case notPermitted
//    }
//}
//
//
//extension AVCaptureSession {
//    var movieFileOutput: AVCaptureMovieFileOutput? {
//        let output = self.outputs.first as? AVCaptureMovieFileOutput
//        
//        return output
//    }
//    
//    func addMovieInput() throws -> Self {
//        // Add video input
//        guard let videoDevice = AVCaptureDevice.default(for: AVMediaType.video) else {
//            throw VideoError.device(reason: .unableToSetInput)
//        }
//        
//        let videoInput = try AVCaptureDeviceInput(device: videoDevice)
//        guard self.canAddInput(videoInput) else {
//            throw VideoError.device(reason: .unableToSetInput)
//        }
//        
//        self.addInput(videoInput)
//        
//        return self
//    }
//    
//    func addMovieFileOutput() throws -> Self {
//        guard self.movieFileOutput == nil else {
//            // return itself if output is already set
//            return self
//        }
//        
//        let fileOutput = AVCaptureMovieFileOutput()
//        guard self.canAddOutput(fileOutput) else {
//            throw VideoError.device(reason: .unableToSetOutput)
//        }
//        
//        self.addOutput(fileOutput)
//        
//        return self
//    }
//}
