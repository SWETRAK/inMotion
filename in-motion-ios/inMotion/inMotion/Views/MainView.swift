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
                        YourMainWallPostView(post: safeUserPost)
                            .environmentObject(appState)
                    }
                } else {
                    NavigationLink {
                        CreatePostView().environmentObject(appState)
                    } label: {
                        Text("RECORD POST")
                    }
                }
                
                if (posts.count > 0) {
                    Text("FRIENDS POSTS")
                    ForEach(posts, id:\.id) { post in
                        MainWallPostView(post: post)
                            .environmentObject(appState)
                    }.blur(radius: self.userPost == nil ? 10 : 0)
                }
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
                        .navigationBarTitleDisplayMode(.inline)
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
            self.userPost = data
        } onFailure: { (error: ImsHttpError) in }
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
