//
//  PostDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import CoreData
import AVKit

struct PostDetailsView: View {
    
    var post: GetPostResponseDto
    
    var frontVideoURL: URL
    var backVideoURL: URL
    
    @EnvironmentObject private var appState: AppState
    @State private var authorGifData: Data? = nil
    
    @State private var comments: [PostCommentDto] = []
    @State private var newComment: String = ""
    @State private var liked: Bool = false
    
    @State private var avPlayerBig: AVPlayer? = nil
    @State private var avPlayerSmall: AVPlayer? = nil
    
    var body: some View {
        VStack{
            VStack {
                ScrollView {
                    VStack(alignment: .leading){
                        Text(post.title)
                            .font(.system(size: 25))
                        
                        if (self.avPlayerBig != nil && self.avPlayerSmall != nil) {
                            HStack(alignment: .top) {
                                VideoPlayer(player: self.avPlayerBig)
                                    .frame(
                                        width: (UIScreen.main.bounds.width-20)/2,
                                        height: ((UIScreen.main.bounds.width-20)/2)/9*16,
                                        alignment: .topTrailing)
                                    .onDisappear{
                                        self.avPlayerBig?.pause()
                                    }
                                    .onAppear{
                                        self.avPlayerBig?.play()
                                        NotificationCenter.default.addObserver(forName: .AVPlayerItemDidPlayToEndTime, object: nil, queue: .main) { _ in
                                            self.avPlayerBig?.seek(to: .zero)
                                            self.avPlayerBig?.play()
                                        }
                                    }
                                
                                VideoPlayer(player: self.avPlayerSmall)
                                    .frame(
                                        width: (UIScreen.main.bounds.width-20)/2,
                                        height: ((UIScreen.main.bounds.width-20)/2)/9*16,
                                        alignment: .bottomTrailing)
                                    .onDisappear{
                                        self.avPlayerSmall?.pause()
                                    }
                                    .onAppear{
                                        self.avPlayerSmall?.play()
                                        NotificationCenter.default.addObserver(forName: .AVPlayerItemDidPlayToEndTime, object: nil, queue: .main) { _ in
                                            self.avPlayerSmall?.seek(to: .zero)
                                            self.avPlayerSmall?.play()
                                        }
                                    }
                            }
                        }
                        
                        HStack{
                            HStack{
                                Image(systemName: liked ? "heart.fill" : "heart") // heart.fill if liked
                                    .resizable()
                                    .foregroundColor(liked ? .red : .black) // .red if liked
                                    .frame(width: 20, height: 20)
                                    .onTapGesture {
                                        if (self.liked) {
                                            UnlikePost()
                                        } else {
                                            LikePost()
                                        }
                                        GetLikesCount()
                                    }
                                Text(String(self.post.postReactionsCount))
                            }
                            
                            Spacer()
                            
                            HStack{
                                Image(systemName: "text.bubble.rtl")
                                    .resizable()
                                    .frame(width: 20, height: 20)
                                Text(String(self.comments.count))
                            }
                        }
                        Divider()
                        
                        if (post.description != String.Empty) {
                            Text(post.description)
                            Divider()
                        }
                    }
                    
                    
                    ForEach(self.comments, id:\.id) { comment in
                        CommentView(comment: comment).environmentObject(appState)
                    }
                }
                
                Divider()
                HStack{
                    TextField(text: $newComment){
                        Text("Add a comment...")
                    }
                    Button {
                        if(!self.newComment.isEmpty) {
                            AddComment()
                            LoadComments()
                            self.newComment = ""
                        }
                    } label: {
                        Image(systemName: "paperplane.fill")
                    }
                }
            }
        }
        .padding()
        .navigationBarTitle(post.author.nickname, displayMode: .inline)
        .onAppear{
            GetLikesCount()
            LoadComments()
            LoadProfilePicture()
            PreparePlayer()
        }
        .onDisappear{
            self.avPlayerBig?.pause()
            self.avPlayerBig?.replaceCurrentItem(with: nil)
            self.avPlayerBig = nil
            
            self.avPlayerSmall?.pause()
            self.avPlayerSmall?.replaceCurrentItem(with: nil)
            self.avPlayerSmall = nil
        }
    }
    
    private func PreparePlayer() {
        self.avPlayerBig = AVPlayer(playerItem: AVPlayerItem(asset: AVAsset(url: self.frontVideoURL)))
        self.avPlayerSmall = AVPlayer(playerItem: AVPlayerItem(asset: AVAsset(url: self.backVideoURL)))
        self.avPlayerBig?.isMuted = false
        self.avPlayerSmall?.isMuted = false
    }
    
    private func LoadProfilePicture() {
        self.appState.getUserGifHttpRequest(
            userId: self.post.author.id) { (data: Data) in
                DispatchQueue.main.async {
                    self.authorGifData = data
                }
            } failureGetUserProfileGifUrl: { (error: ImsHttpError) in }
        
    }
    
    private func LoadComments() {
        self.appState.GetPostCommentsForPostHttpRequest(postId: self.post.id) { (data: [PostCommentDto]) in
            self.comments = data
            self.comments.sort{ (x1, x2) in
                x1.createdAt.compare(x2.createdAt) == .orderedAscending
            }
        } onFailure: { ( error: ImsHttpError) in }
    }
    
    private func GetLikesCount() {
    }
    
    private func LikePost() {
    }
    
    private func UnlikePost() {
    }
    
    private func AddComment() {
        if self.newComment != String.Empty {
            self.appState.CreatePostForPostHttpRequest(
                requestData: CreatePostCommentDto(
                    content: self.newComment,
                    postId: self.post.id))
            { (data: PostCommentDto) in
                print("Kamil")
                self.newComment = String.Empty
                self.comments.append(data)
                self.comments.sort{ (x1, x2) in
                    x1.createdAt.compare(x2.createdAt) == .orderedAscending
                }
            } onFailure: { (error: ImsHttpError) in }
        }
    }
}

//struct PostDetailsView_Previews: PreviewProvider {
//    static var previews: some View {
//        PostDetailsView()
//    }
//}
