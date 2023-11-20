//
//  DualCamSwiftWrapperView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/11/2023.
//

import SwiftUI

struct PostCameraViewWrapper: UIViewControllerRepresentable {
    @EnvironmentObject var appState: AppState
    
    typealias UIViewControllerType = PostCameraViewController
    
    func makeUIViewController(context: Context) -> PostCameraViewController {
        let storyBoard = UIStoryboard(name: "PostCameraView", bundle: Bundle.main).instantiateViewController(withIdentifier: "PostCameraView") as! PostCameraViewController
        storyBoard.appState = appState
        return storyBoard
    }
    
    func updateUIViewController(_ uiViewController: PostCameraViewController, context: Context) { }
}
