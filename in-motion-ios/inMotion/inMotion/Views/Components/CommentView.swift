//
//  Comment.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct CommentView: View {
    
    @Binding
    var comment: Comment
    
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
        }.padding()
    }
}

struct Comment_Previews: PreviewProvider {
    static var previews: some View {
        Comment()
    }
}
