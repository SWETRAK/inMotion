//
//  RequestFriendRowView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI

struct RequestFriendRowView: View {
    @Binding
    var friend: Friend
    var body: some View {
        HStack{
            Image(friend.avatar).resizable().frame(width:50, height:50)
            VStack{
                Text(friend.username).fontWeight(Font.Weight.bold).frame(maxWidth: .infinity, alignment: .leading)
                Text("Last seen: \(friend.lastseen)").font(.system(size: 12)).frame(maxWidth: .infinity, alignment: .leading)
                
            }
            Spacer()
            Button{} label: {
                Image(systemName: "checkmark").foregroundColor(.green)     } .padding(.horizontal)
            Button{} label: {
                Image(systemName: "multiply").foregroundColor(.red)     }  .padding(.horizontal)      }.frame(maxWidth: .infinity, alignment: .leading) .padding(.horizontal)
    }
}

struct RequestFriendRowView_Previews: PreviewProvider {
    static var previews: some View {
        RequestFriendRowView(friend: .constant(Friend(nickname: "kamil", lastseen: "1233142", avatar: "google-logo")))
    }
}
