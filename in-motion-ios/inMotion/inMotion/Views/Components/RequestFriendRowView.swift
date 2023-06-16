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
                Image(systemName: "checkmark").foregroundColor(.green)     } .padding()
            Button{} label: {
                Image(systemName: "multiply").foregroundColor(.red)     }  .padding()      }.frame(maxWidth: .infinity, alignment: .leading) .padding()
    }
   }

//struct RequestFriendRowView_Previews: PreviewProvider {
//    static var previews: some View {
//        RequestFriendRowView()
//    }
//}
