//
//  MainView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct MainView: View {

    @EnvironmentObject private var appState: AppState
    
    var body: some View {
        VStack{
            Text(appState.user?.email ?? "")
            ScrollView() {
//                ForEach(posts, id:\.id) { post in
//                    MainWallPost().environmentObject(appState).environmentObject(post)
//                }
            }
        }.toolbar {
            ToolbarItem(placement: .primaryAction) {
                NavigationLink {
                    FriendsView().environmentObject(appState)
                } label: {
                    Image(systemName: "person.2.fill")
                }.buttonStyle(.plain)
            }
            ToolbarItem(placement: .secondaryAction) {
                NavigationLink {
                    LoggedUserDetailsView().environmentObject(appState)
                } label: {
                    Text("User")
                }
            }
        }.onAppear{
            //TODO: Load Posts
        }
    }

}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
