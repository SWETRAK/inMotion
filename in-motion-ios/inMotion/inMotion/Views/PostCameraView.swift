//
//  PostCameraScreen.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/11/2023.
//

import SwiftUI

struct PostCameraView: View {
    var post: CreatePostRequestDto
    @EnvironmentObject public var appState: AppState
    
    var body: some View {
        PostCameraViewWrapper(postRequestData: post)
            .environmentObject(appState)
            .navigationBarHidden(true)
    }
}

//struct PostCameraScreen_Previews: PreviewProvider {
//    static var previews: some View {
//        PostCameraView()
//    }
//}
