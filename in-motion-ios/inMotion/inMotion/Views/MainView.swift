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
            
            ScrollView() {
                if let safeUserPost = self.userPost {
                    VStack(alignment: .center) {
                        Text("YOUR POST")
                        MainWallPost(post: safeUserPost)
                    }
                } else {
                    NavigationLink {
                        CreatePostView().environmentObject(appState)
                    } label: {
                        Text("RECORD POST")
                    }
                }
                
                ForEach(posts, id:\.id) { post in
                    MainWallPost(post: post)
                        .environmentObject(appState)
                }.blur(radius: self.userPost == nil ? 10 : 0)
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
        self.LoadCurrentUserPost()
        self.LoadPosts()
    }
    
    private func LoadCurrentUserPost() {
        self.appState.GetUserPost { (data: GetPostResponseDto) in
            print(data)
            self.userPost = data
        } onFailure: { (error: ImsHttpError) in }
    }
    
    private func LoadPosts() {
        self.appState.GetFriendsPosts { (data: [GetPostResponseDto]) in
            print("Kamil")
            self.posts = data
        } onFailure: { (error: ImsHttpError) in
            print(error)
            self.posts = []
        }
    }
    
    private func LoadFriends() {
        self.appState.getListOfAcceptedFriendshipHttpRequest(
            successGetRelations: {(friends: [AcceptedFriendshipDto]) in },
            failureGetRelations: {(error: ImsHttpError) in })
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
