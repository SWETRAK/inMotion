//
//  MainView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct MainView: View {

    @EnvironmentObject public var appState: AppState
    
    var body: some View {
        VStack{
            Text(appState.user?.email ?? "")
            
            // Post camera view
//            NavigationLink {
//                PostCameraScreen().environmentObject(appState)
//            } label: {
//                Text("CamScreen")
//            }

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
            OnViewAppear()
        }
    }
    
    private func OnViewAppear() {
        self.LoadFriends()
        self.LoadRequests()
        self.LoadInvitations()
    }
    
    private func LoadFriends() {
        self.appState.getListOfAcceptedFriendshipHttpRequest(
            successGetRelations: {(friends: [AcceptedFriendshipDto]) in
            },
            failureGetRelations: {(error: ImsHttpError) in
                
            })
    }
    
    private func LoadRequests() {
        self.appState.getListOfRequestedFriendshipHttpRequest(
            successGetRelations: {(data: [RequestFriendshipDto]) in },
            failureGetRelations: {(error: ImsHttpError) in })
    }
    
    private func LoadInvitations() {
        self.appState.getListOfInvitedFriendshipHttpRequest(
            successGetRelations: {(data: [InvitationFriendshipDto]) in },
            failureGetRelations: {(error: ImsHttpError) in })
    }

}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
