//
//  Comment.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct CommentView: View {
    
    @Binding
    var comment: MyComment
    
    var body: some View {
        HStack{
            Image(comment.avatar).resizable().frame(width: 50, height: 50)
            VStack{
                HStack{
                    Text(comment.username).fontWeight(Font.Weight.bold)
                    Text("\(comment.location), \(comment.time)").font(.system(size: 12)).foregroundColor(Color.blue)
                }.frame(maxWidth: .infinity, alignment: .leading)
                Text(comment.comment).frame(maxWidth: .infinity, alignment: .leading)
            }
        }.padding(.horizontal)
    }
}

struct CommentView_Previews: PreviewProvider {
    static var previews: some View {
        CommentView(comment: .constant(MyComment(username: "kamil_pietrak", comment: "test comment", location: "Sieprawki", time: "12.23", avatar: "google-logo")))
    }
}
