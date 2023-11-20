//
//  DataExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 18/11/2023.
//

import Foundation
import AVKit


extension Data {
    func convertToAVAsset() -> AVAsset {
        let directory = NSTemporaryDirectory()
        let fileName = "\(NSUUID().uuidString).mp4"
        let fullURL = NSURL.fileURL(withPathComponents: [directory, fileName])
        do {
            try self.write(to: fullURL!)
        } catch {
            print()
        }
        let asset = AVAsset(url: fullURL!)
        return asset
    }
}
