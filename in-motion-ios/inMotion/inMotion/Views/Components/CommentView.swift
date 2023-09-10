//
//  Comment.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct CommentView: View {
    
    @EnvironmentObject var comment: Comment
    
    var body: some View {
        HStack{
            Image(comment.author?.profile_photo ?? "avatar-placeholder").resizable().frame(width: 50, height: 50)
            VStack{
                HStack{
                    Text(comment.author?.nickname ?? "nickname").fontWeight(Font.Weight.bold)
                    Text(comment.time?.formatted(date: .numeric, time: .shortened) ?? "Monday: 12.43").font(.system(size: 12)).foregroundColor(Color.blue)
                }.frame(maxWidth: .infinity, alignment: .leading)
                Text(comment.comment ?? "description").frame(maxWidth: .infinity, alignment: .leading)
            }
        }.padding(.horizontal)
    }
}

struct CommentView_Previews: PreviewProvider {
    static var previews: some View {
        CommentView()
    }
}
