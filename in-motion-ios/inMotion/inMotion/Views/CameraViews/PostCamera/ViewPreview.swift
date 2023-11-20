//
//  ViewPreview.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/11/2023.
//

import Foundation

import AVFoundation
import UIKit
 
class ViewPreview: UIView {
    var videoPreviewLayer: AVCaptureVideoPreviewLayer {
        guard let layer = layer as? AVCaptureVideoPreviewLayer else {
            fatalError("Expected `AVCaptureVideoPreviewLayer` type for layer. Check PreviewView.layerClass implementation.")
        }
        
        layer.videoGravity = .resizeAspect
        return layer
    }
    
    override class var layerClass: AnyClass {
        return AVCaptureVideoPreviewLayer.self
    }
}
