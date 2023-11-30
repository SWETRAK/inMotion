//
//  Comment.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import SDWebImageSwiftUI

struct CommentView: View {
    
    @EnvironmentObject var appState: AppState
    var comment: PostCommentDto
    
    @State private var authorFrontData: Data?
    
    var body: some View {
        HStack{
            if let safeData = self.authorFrontData {
                AnimatedImage(data: safeData)
                    .resizable()
                    .frame(width: 50, height: 50, alignment: .center)
                
            } else {
                Image("avatar-placeholder")
                    .resizable()
                    .frame(width: 50, height: 50, alignment: .center)
            }
            VStack{
                HStack{
                    Text(comment.author.nickname).fontWeight(Font.Weight.bold)
                    Text(comment.createdAt.formatted(date: .numeric, time: .shortened)).font(.system(size: 12)).foregroundColor(Color.blue)
                }.frame(maxWidth: .infinity, alignment: .leading)
                Text(comment.content).frame(maxWidth: .infinity, alignment: .leading)
            }
        }
        .padding(.horizontal)
        .onAppear {
            LoadProfilePicture()
        }
    }
    
    private func LoadProfilePicture() {
        self.appState.getUserGifHttpRequest(
            userId: self.comment.author.id ) { (data: Data) in
                DispatchQueue.main.async {
                    self.authorFrontData = data
                }
            } failureGetUserProfileGifUrl: { (error: ImsHttpError) in }
    }
}
//
//struct CommentView_Previews: PreviewProvider {
//    static var previews: some View {
//        CommentView()
//    }
//}
