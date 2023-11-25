//
//  MainView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct MainView: View {

    @EnvironmentObject public var appState: AppState
    @State var posts: [GetPostResponseDto] = []
    
    @State var userPost: GetPostResponseDto? = nil
    
    var body: some View {
        VStack{
            if let safeUserPost = self.userPost {
                //TODO; use safe UserPost here
            } else {
                // Post camera view
                NavigationLink {
                    PostCameraView().environmentObject(appState)
                } label: {
                    Text("RECORD POST")
                }
            }
            
            ScrollView() {
                ForEach(posts, id:\.id) { post in
                    MainWallPost(post: post)
                        .environmentObject(appState)
                }
            }.blur(radius: 9)
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
        self.LoadPosts()
        self.LoadCurrentUserPost()
    }
    
    private func LoadCurrentUserPost() {
        self.appState.GetUserPost { (data: GetPostResponseDto) in
            
        } onFailure: { (error: ImsHttpError) in
            
        }
    }
    
    private func LoadPosts() {
        self.appState.GetFriendsPosts { (data: [GetPostResponseDto]) in
            self.posts = data
        } onFailure: { (error: ImsHttpError) in
            self.posts = []
        }

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
