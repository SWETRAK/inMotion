//
//  CreatePostView.swift
//  inMotion
//
//  Created by student on 27/11/2023.
//

import SwiftUI

struct CreatePostView: View {
    
    @EnvironmentObject private var appState: AppState
    
    @State private var postTitle: String = ""
    @State private var postDecription: String = ""
    
    var body: some View {
        Form{
            Section {
                
                LabeledContent {
                    TextField("Title", text: self.$postTitle)
                } label: {
                    Text("Title")
                }
                
                LabeledContent {
                    TextField("Description", text: self.$postDecription)
                } label: {
                    Text("Description")
                }
                
                NavigationLink {
                    PostCameraView(post: CreatePostRequestDto(title: self.postTitle, description: self.postDecription))
                        .environmentObject(appState)
                        
                } label: {
                    Text("Record video")
                }
            }
        }
    }
}

#Preview {
    CreatePostView()
}
