//
//  PostCameraScreen.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/11/2023.
//

import SwiftUI

struct PostCameraScreen: View {
    @EnvironmentObject var appState: AppState
    
    var body: some View {
        DualCamSwiftWrapperView()
    }
}

struct PostCameraScreen_Previews: PreviewProvider {
    static var previews: some View {
        PostCameraScreen()
    }
}
