//
//  OtherUserDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 11/11/2023.
//

import SwiftUI

struct OtherUserDetailsView: View {
    
    @EnvironmentObject private var appState: AppState
    var user: FullUserInfoDto
    
    var body: some View {
        Form {
            Section(header: Text("User details")) {
                
                // TODO: Add user profile video
                
                LabeledContent {
                    Text(user.nickname)
                } label: {
                    Text("Nickname")
                }
                
                LabeledContent {
                    Text(user.bio ?? "")
                } label: {
                    Text("Bio")
                }
            }
        }.onAppear{
            
        }
    }
}
//
//struct OtherUserDetailsView_Previews: PreviewProvider {
//    static var previews: some View {
//        OtherUserDetailsView()
//    }
//}
