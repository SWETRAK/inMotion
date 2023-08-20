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
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var post: Post
    @EnvironmentObject private var appState: AppState
    
    @State private var comments: [Comment] = []
    @State private var newComment: String = ""
    
    @State private var liked: Bool = false
    @State private var myLike: Like? = nil
    @State private var likesCount: Int = 0
    
    @State private var showingMap: Bool = false
    @State private var mapDetails = MapDetail(name: "Test", latitude: 12321.43, longitude: 23432.23)
    
    @State private var avPlayer: AVPlayer? = nil
    
    var body: some View {
        VStack{
            VStack {
                ScrollView {
                    VStack{
                        Text(post.localization_name ?? "Warsaw")
                            .font(.system(size: 12))
                            .foregroundColor(Color.blue)
                            .onTapGesture {
                                showingMap.toggle()
                            }.sheet(isPresented: $showingMap) {
                                MapView(mapDetails: $mapDetails, isPresented: $showingMap)
                            }
                        
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
                                Text(String(self.comments.count))
                            }
                        }
                        Divider()
                    }
                    
                    ForEach(self.comments, id:\.id) { comment in
                        CommentView().environmentObject(comment)
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
        .navigationBarTitle(post.author?.nickname ?? "nickname", displayMode: .inline)
        .onAppear{
            GetMapDetails()
            GetLikesCount()
            LoadComments()
            PreparePlayer()
        }
        .onDisappear{
            self.avPlayer?.pause()
            self.avPlayer?.replaceCurrentItem(with: nil)
            self.avPlayer = nil
        }
    }
    
    private func PreparePlayer() {
        self.avPlayer = AVPlayer(url: Bundle.main.url(forResource: post.video_link ?? "warsaw", withExtension: "mp4")!)
        self.avPlayer?.isMuted = false
    }
    
    
    private func GetMapDetails() {
        self.mapDetails = MapDetail(name: "Test", latitude: self.post.localization_latitude, longitude: post.localization_longitude)
    }
    
    private func LoadComments() {
        let request: NSFetchRequest<Comment> = Comment.fetchRequest()
        let predictate = NSPredicate(format: "post.id == %@", post.id! as CVarArg)
        request.predicate = predictate
        do {
            self.comments = try viewContext.fetch(request)
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

    private func AddComment() {
        let comment = Comment(context: viewContext)
        comment.id = UUID()
        comment.post = post
        comment.author = appState.user
        comment.comment = self.newComment
        
        if (viewContext.hasChanges) {
            do {
                try viewContext.save()
            } catch {
                let nserror = error as NSError
                print("Unresolved error \(nserror), \(nserror.userInfo)")
            }
        }
    }
}

struct PostDetailsView_Previews: PreviewProvider {
    static var previews: some View {
        PostDetailsView()
    }
}
