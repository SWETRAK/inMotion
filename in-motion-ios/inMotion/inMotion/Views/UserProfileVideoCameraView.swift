//
//  UserProfileVideoCameraView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 18/11/2023.
//

import SwiftUI

struct UserProfileVideoCameraView: View {
    @EnvironmentObject public var appState: AppState

    
    
    var body: some View {
        ProfileCameraViewWrapper().environmentObject(appState)
//            .navigationBarHidden(true)
            .navigationBarTitleDisplayMode(.inline)
    }
}

struct UserProfileVideoCameraView_Previews: PreviewProvider {
    static var previews: some View {
        UserProfileVideoCameraView()
    }
}
