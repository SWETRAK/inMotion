//
//  AVPlayerViewControllerExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 17/06/2023.
//

import Foundation
import AVKit

extension AVPlayerViewController {
    override open func viewDidLoad() {
        super.viewDidLoad()
        self.showsPlaybackControls = false
    }
}
