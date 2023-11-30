import SwiftUI
import SDWebImageSwiftUI
import CoreData
import AVKit

struct MainWallPost: View {
    @EnvironmentObject private var appState: AppState
    var post: GetPostResponseDto
    
    @State private var liked: Bool = false
    
    @State private var authorFrontData: Data?

    @State private var avPlayerBig: AVPlayer? = nil
    @State private var avPlayerSmall: AVPlayer? = nil

    var body: some View {
        NavigationLink {
            PostDetailsView(post: post)
                .environmentObject(appState)
        } label: {
            VStack {

                if (self.avPlayerBig != nil && self.avPlayerSmall != nil) {
                    HStack(alignment: .top) {
                        VideoPlayer(player: self.avPlayerBig)
                            .frame(
                                width: UIScreen.main.bounds.width-20,
                                height: (UIScreen.main.bounds.width-20)/9*16,
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
                    GetCommentsCount()
                    GetLikesCount()
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
        self.avPlayerBig = AVPlayer(playerItem: AVPlayerItem(asset: data.backVideo.convertToAVAsset()))
        self.avPlayerSmall = AVPlayer(playerItem: AVPlayerItem(asset: data.frontVideo.convertToAVAsset()))
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
    

    private func GetCommentsCount() {
    }

    private func GetLikesCount() {
    }

    private func LikePost() {
    }

    private func UnlikePost() {
    }
}

//struct MainWallPost_Previews: PreviewProvider {
//    static var previews: some View {
//        MainWallPost()
//    }
//}
