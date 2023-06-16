import SwiftUI
import CoreData

struct MainWallPost: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject var post: Post
    @EnvironmentObject var appState: AppState
    
    @State var myLike: Like? = nil
    @State var likesCount: Int = 0
    @State var liked: Bool = true
    
    @State var commentsCount: Int = 0
    
    var body: some View {
        NavigationLink {
            PostDetailsView()
                .environmentObject(post)
                .environmentObject(appState)
        } label: {
            VStack {
                HStack{
                    Image(post.author?.profile_photo ?? "google-login")
                        .resizable()
                        .frame(width: 70, height: 70)
                    VStack(alignment: .leading){
                        Text(post.author?.nickname ?? "nickname")
                        Text(post.localization_name ?? "Lublin")
                    }
                }.frame(maxWidth: .infinity, alignment: .leading)
                
                Image(post.video_link ?? "google-logo")
                    .resizable()
                    .frame(width: UIScreen.main.bounds.width-20, height: UIScreen.main.bounds.width-20)
                
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
        }
        .buttonStyle(.plain)
        .onAppear {
            GetCommentsCount()
            GetLikesCount()
        }
    }
    
    private func GetCommentsCount() {
        let request: NSFetchRequest<Comment> = Comment.fetchRequest()
        let prediction = NSPredicate()
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
        let prediction = NSPredicate()
        request.predicate = prediction
        do {
            let result = try viewContext.fetch(request)
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
