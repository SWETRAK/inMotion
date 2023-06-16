//
//  YourFriendRowView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI

struct YourFriendRowView: View {
    @Binding
    var friend: Friend
    var body: some View {
        HStack{
            Image(friend.avatar).resizable().frame(width:50, height:50)
            VStack(alignment: .leading){
                Text(friend.username).fontWeight(Font.Weight.bold).frame(maxWidth: .infinity, alignment: .leading)
                Text("Last seen: \(friend.lastseen)").font(.system(size: 12)).frame(maxWidth: .infinity, alignment: .leading)
                
            }
            .frame(maxWidth: .infinity, alignment: .leading)
            Spacer()
        }
        .frame(maxWidth: .infinity, alignment: .leading)
        .padding(.horizontal)
    }
}

struct YourFriendRowView_Previews: PreviewProvider {
    static var previews: some View {
        YourFriendRowView(friend: .constant(Friend(nickname: "kamil Pietrak", lastseen: "12.343", avatar: "google-logo")))
    }
}
