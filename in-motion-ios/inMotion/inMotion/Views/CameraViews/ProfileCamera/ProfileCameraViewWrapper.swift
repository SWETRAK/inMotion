//
//  ProfileCameraViewWrapper.swift
//  inMotion
//
//  Created by Kamil Pietrak on 20/11/2023.
//

import SwiftUI

struct ProfileCameraViewWrapper: UIViewControllerRepresentable {
    typealias UIViewControllerType = ProfileCameraViewController
    
    @EnvironmentObject var appState: AppState
    
    func makeUIViewController(context: Context) -> ProfileCameraViewController {
        let storyBoard = UIStoryboard(name: "ProfileCameraView", bundle: Bundle.main).instantiateViewController(withIdentifier: "ProfileCameraView") as! ProfileCameraViewController
//        let profileCameraView = ProfileCameraViewController()
        
        storyBoard.appState = appState
       
//        profileCameraView.appState = appState
        return storyBoard
    }
    
    func updateUIViewController(_ uiViewController: ProfileCameraViewController, context: Context) { }
}
