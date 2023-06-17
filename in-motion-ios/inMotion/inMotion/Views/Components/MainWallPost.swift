import SwiftUI
import CoreData
import AVKit

struct MainWallPost: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var post: Post
    @EnvironmentObject private var appState: AppState
    
    @State private var myLike: Like? = nil
    @State private var likesCount: Int = 0
    @State private var liked: Bool = false
    
    @State private var commentsCount: Int = 0
    @State private var avPlayer: AVPlayer? = nil
    
    var body: some View {
        NavigationLink {
            PostDetailsView()
                .environmentObject(post)
                .environmentObject(appState)
        } label: {
            VStack {
                
                HStack{
                    Image(post.author?.profile_photo ?? "avatar-placeholder")
                        .resizable()
                        .frame(width: 70, height: 70)
                    VStack(alignment: .leading){
                        Text(post.author?.nickname ?? "nickname")
                        Text(post.localization_name ?? "Lublin")
                    }
                }.frame(maxWidth: .infinity, alignment: .leading)
                
                
                if(self.avPlayer != nil){
                    VideoPlayer(player: self.avPlayer)
                        .frame(width: UIScreen.main.bounds.width-20, height: (UIScreen.main.bounds.width-20)/16*9)
                        .onDisappear{
                            self.avPlayer?.pause()
                        }
                        .onAppear{
                            self.avPlayer?.play()
                            NotificationCenter.default.addObserver(forName: .AVPlayerItemDidPlayToEndTime, object: nil, queue: .main) { _ in
                                self.avPlayer?.seek(to: .zero)
                                self.avPlayer?.play()
                            }
                        }
                        .onTapGesture(count: 2) {
                            if(!self.liked) {
                                LikePost()
                                GetLikesCount()
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
                        Text(String(self.likesCount))
                    }
                    
                    Spacer()
                    
                    HStack{
                        Image(systemName: "text.bubble.rtl")
                            .resizable()
                            .frame(width: 20, height: 20)
                        Text(String(self.commentsCount))
                    }
                }
                Divider()
                
            }.padding()
                .onAppear {
                    GetCommentsCount()
                    GetLikesCount()
                    PreparePlayer()
                }.onDisappear{
                    self.avPlayer?.pause()
                    self.avPlayer?.replaceCurrentItem(with: nil)
                    self.avPlayer = nil
                }
        }.buttonStyle(.plain)
    }
    
    private func PreparePlayer() {
        self.avPlayer = AVPlayer(url: Bundle.main.url(forResource: post.video_link ?? "warsaw", withExtension: "mp4")!)
        self.avPlayer?.isMuted = true
    }
    
    
    private func GetCommentsCount() {
        let request: NSFetchRequest<Comment> = Comment.fetchRequest()
        let prediction = NSPredicate(format: "post.id == %@", post.id! as CVarArg)
        request.predicate = prediction
        do {
            let result = try viewContext.fetch(request)
            self.commentsCount = result.count
        } catch {
            print("Error fetching data from context \(error)")
        }
    }
    
    private func GetLikesCount() {
        let request: NSFetchRequest<Like> = Like.fetchRequest()
        let prediction = NSPredicate(format: "post.id == %@", post.id! as CVarArg)
        request.predicate = prediction
        do {
            let result = try viewContext.fetch(request)
            if let mySafeLike = result.first(where: {$0.author?.id == appState.user?.id}) {
                self.liked = true;
                self.myLike = mySafeLike
            }
            self.likesCount = result.count
        } catch {
            print("Error fetching data from context \(error)")
        }
    }
    
    private func LikePost() {
        let like = Like(context: viewContext)
        like.post = post
        like.author = appState.user
        like.time = Date.now
        if (viewContext.hasChanges) {
            do {
                try viewContext.save()
                self.myLike = like
                self.liked = true
            } catch {
                let nserror = error as NSError
                fatalError("Unresolved error \(nserror), \(nserror.userInfo)")
            }
        }
    }
    
    private func UnlikePost() {
        if let safeLike = myLike {
            viewContext.delete(safeLike)
            self.myLike = nil
            self.liked = false
            do {
                try viewContext.save()
            } catch {
                print("error while saving \(error)")
            }
        }
    }
}

struct MainWallPost_Previews: PreviewProvider {
    static var previews: some View {
        MainWallPost()
    }
}
