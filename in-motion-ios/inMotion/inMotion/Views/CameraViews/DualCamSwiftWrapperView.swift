//
//  DualCamSwiftWrapperView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/11/2023.
//

import SwiftUI

struct DualCamSwiftWrapperView: UIViewControllerRepresentable {
    @EnvironmentObject var appState: AppState
    
    typealias UIViewControllerType = ViewController
    
    func makeUIViewController(context: Context) -> ViewController {
        let controller = ViewController()
        return controller
    }
    
    func updateUIViewController(_ uiViewController: ViewController, context: Context) { }
}
