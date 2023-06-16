//
//  PostDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct PostDetailsView: View {
    @Environment(\.managedObjectContext) private var viewContext
    
    @State var nickname : String = "stephen_mustache";
    @State var imageFront: String = "Post";
    @State var imageBack: String = "Post";
    @State var comments = [
        MyComment(username: "Pavelek69", comment: "Fajne fajne", location: "Lublin", time: "13:07", avatar: "google-logo"),
        MyComment(username: "Andrzejek01", comment: "No ciekawe", location: "Lublin", time: "21:37", avatar: "google-logo"),
        MyComment(username: "Gibon024", comment: "Wooow", location: "Lublin", time: "4:20", avatar: "google-logo")
    ]
    @State var newComment: String = ""
    @State var liked: Bool = true
    @State var showingMap: Bool = false
    
    @State var mapDetails = MapDetail(name: "Test", latitude: 12321.43, longitude: 23432.23)
    
    var body: some View {
        VStack{
            VStack {
                ScrollView {
                    VStack{
                        Text("\(comments[0].location), \(comments[0].time)")
                            .font(.system(size: 12))
                            .foregroundColor(Color.blue)
                            .onTapGesture {
                                showingMap.toggle()
                            }.sheet(isPresented: $showingMap) {
                                MapView(mapDetails: $mapDetails)
                            }
                        
                        Image("google-logo")
                            .resizable()
                            .frame(width: UIScreen.main.bounds.width-20, height: UIScreen.main.bounds.width-20)
                        
                        HStack{
                            HStack{
                                Image(systemName: liked ? "heart.fill" : "heart") // heart.fill if liked
                                    .resizable()
                                    .foregroundColor(liked ? .red : .black) // .red if liked
                                    .frame(width: 20, height: 20)
                                    .onTapGesture {
                                        self.liked = !self.liked
                                    }
                                Text("12")
                            }
                            
                            Spacer()
                            
                            HStack{
                                Image(systemName: "text.bubble.rtl")
                                    .resizable()
                                    .frame(width: 20, height: 20)
                                Text("12")
                            }
                        }
                        Divider()
                    }
                    
                    CommentView(comment: $comments[0])
                    CommentView(comment: $comments[1])
                    CommentView(comment: $comments[2])
                    CommentView(comment: $comments[2])
                    CommentView(comment: $comments[2])
                    CommentView(comment: $comments[2])
                    
                }
                
                
                Divider()
                HStack{
                    TextField(text: $newComment){
                        Text("Add a comment...")
                    }
                    Image(systemName: "paperplane.fill")
                }
            }
        }
        .padding()
        .navigationBarTitle(nickname, displayMode: .inline)
    }
}



struct PostDetailsView_Previews: PreviewProvider {
    static var previews: some View {
        PostDetailsView()
    }
}
