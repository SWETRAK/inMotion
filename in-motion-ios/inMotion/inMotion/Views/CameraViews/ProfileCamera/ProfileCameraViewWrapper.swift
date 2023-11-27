//
//  ProfileCameraViewWrapper.swift
//  inMotion
//
//  Created by Kamil Pietrak on 20/11/2023.
//

import SwiftUI

struct ProfileCameraViewWrapper: UIViewControllerRepresentable {
    typealias UIViewControllerType = ProfileCameraViewController
    @Environment(\.presentationMode) var presentationMode
    @EnvironmentObject var appState: AppState
    
    func makeUIViewController(context: Context) -> ProfileCameraViewController {
        let storyBoard = UIStoryboard(name: "ProfileCameraView", bundle: Bundle.main).instantiateViewController(withIdentifier: "ProfileCameraView") as! ProfileCameraViewController
        storyBoard.appState = appState
        return storyBoard
    }
    
    func updateUIViewController(_ uiViewController: ProfileCameraViewController, context: Context) { 
//        print (uiViewController.finished)
//        if uiViewController.finished == true {
//            self.presentationMode.wrappedValue.dismiss()
//        }
    }
}
