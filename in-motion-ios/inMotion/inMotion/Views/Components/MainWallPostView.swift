import SwiftUI
import SDWebImageSwiftUI
import CoreData
import AVKit

struct MainWallPostView: View {
    @EnvironmentObject private var appState: AppState
    @State var post: GetPostResponseDto
    
    @State private var authorFrontData: Data?
    
    @State private var frontUrl: URL?
    @State private var backUrl: URL?

    @State private var avPlayerBig: AVPlayer? = nil
    @State private var avPlayerSmall: AVPlayer? = nil

    var body: some View {
        NavigationLink {
            if let safeFrontUrl = self.frontUrl, let safeBackUrl = self.backUrl {
                PostDetailsView(post: self.$post, frontVideoURL: safeFrontUrl, backVideoURL: safeBackUrl)
                    .environmentObject(appState)
            }

        } label: {
            VStack {
                HStack{
                    if let safeData = authorFrontData {
                        AnimatedImage(data: safeData)
                            .resizable()
                            .frame(width: 50, height: 50, alignment: .center)
                        
                    } else {
                        Image("avatar-placeholder")
                            .resizable()
                            .frame(width: 50, height: 50, alignment: .center)
                        
                    }
                    VStack(alignment: .leading){
                        Text(self.post.author.nickname)
                            .fontWeight(Font.Weight.bold)
                            .frame(maxWidth: .infinity, alignment: .leading)
                    }
                    .frame(maxWidth: .infinity, alignment: .leading)
                    Spacer()
                }
                .frame(alignment: .leading)
                .onAppear {
                    self.LoadProfilePicture()
                }
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
                                    self.avPlayerBig?.isMuted = false
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
                                    self.avPlayerSmall?.isMuted = false
                                    self.avPlayerSmall?.seek(to: .zero)
                                    self.avPlayerSmall?.play()
                                }
                            }
                    }
                }

                HStack{
                    HStack{
                        Image(systemName: self.post.isLikedByUser ? "heart.fill" : "heart") // heart.fill if liked
                            .resizable()
                            .foregroundColor(self.post.isLikedByUser ? .red : .black) // .red if liked
                            .frame(width: 20, height: 20)
                            .onTapGesture {
                                if (self.post.isLikedByUser) {
                                    UnlikePost()
                                } else {
                                    LikePost()
                                }
                            }
                        Text(String(self.post.postReactionsCount))
                    }

                    Spacer()

                    HStack{
                        Image(systemName: "text.bubble.rtl")
                            .resizable()
                            .frame(width: 20, height: 20)
                        Text(String(self.post.postCommentsCount))
                    }
                }
                Divider()

            }.padding()
                .onAppear {
                    GetPostVideo()
                    LoadProfilePicture()
                }.onDisappear{

                }
        }.buttonStyle(.plain)
    }
    
    private func GetPostVideo() {
        appState.getPostVideosUrls(
            postId: self.post.id,
            successGetPostVideoUrls: {(data: PostDto) in
                self.PreparePlayer(data: data)
            },
            failureGetPostVideoUrls: {(error: ImsHttpError) in })
    }
    
    private func PreparePlayer(data: PostDto) {
        self.frontUrl = data.frontVideo.convertToURL()
        self.backUrl = data.backVideo.convertToURL()
        self.avPlayerBig = AVPlayer(playerItem: AVPlayerItem(asset: AVAsset(url: self.frontUrl!)))
        self.avPlayerSmall = AVPlayer(playerItem: AVPlayerItem(asset: AVAsset(url: self.backUrl!)))
        self.avPlayerBig?.isMuted = false
        self.avPlayerSmall?.isMuted = false
    }

    private func KillPlayers() {
        self.avPlayerBig?.pause()
        self.avPlayerBig?.replaceCurrentItem(with: nil)
        self.avPlayerBig = nil
        
        self.avPlayerSmall?.pause()
        self.avPlayerSmall?.replaceCurrentItem(with: nil)
        self.avPlayerSmall = nil
    }
    
    private func LoadProfilePicture() {
        self.appState.getUserGifHttpRequest(
            userId: self.post.author.id ) { (data: Data) in
                DispatchQueue.main.async {
                    self.authorFrontData = data
                }
            } failureGetUserProfileGifUrl: { (error: ImsHttpError) in }
    }

    private func LikePost() {
        self.appState.createPostReacionHttpMethod(
            requestData: CreatePostReactionDto(
                postId: self.post.id,
                emoji: "heart")) { (data: PostReactionDto) in
                    var tempPost = self.post
                    tempPost.isLikedByUser = true
                    tempPost.postReaction = PostReactionWithoutAuthorDto(
                        id: data.id,
                        authorId: self.appState.user!.id,
                        emoji: data.emoji,
                        createdAt: data.createdAt)
                    tempPost.postReactionsCount += 1
                    self.post = tempPost
                } onFailure: { (error: ImsHttpError) in }
    }

    private func UnlikePost() {
        if let safePostReaction = self.post.postReaction {
            self.appState.deletePostReactionHttpMethod(
                postReactionId: safePostReaction.id) { (data: Bool) in
                    var tempPost = self.post
                    tempPost.isLikedByUser = false
                    tempPost.postReaction = nil
                    tempPost.postReactionsCount -= 1
                    self.post = tempPost
                } onFailure: { (error: ImsHttpError) in }
        }
    }
}

//struct MainWallPost_Previews: PreviewProvider {
//    static var previews: some View {
//        MainWallPost()
//    }
//}
