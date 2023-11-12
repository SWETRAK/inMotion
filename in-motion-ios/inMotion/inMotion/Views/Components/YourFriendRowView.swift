//
//  YourFriendRowView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI
import CoreData

struct YourFriendRowView: View {
    var friendship: FriendInfoDto

    var body: some View {
        HStack{
             // TODO: Change this to video in the future
            Image("avatar-placeholder").resizable().frame(width:50, height:50)
            VStack(alignment: .leading){
                Text(friendship.nickname).fontWeight(Font.Weight.bold).frame(maxWidth: .infinity, alignment: .leading)
            }
            .frame(maxWidth: .infinity, alignment: .leading)
            Spacer()
        }
        .frame(alignment: .leading)
    }
}

//struct YourFriendRowView_Previews: PreviewProvider {
//    static var previews: some View {
//        YourFriendRowView(friendship: .constant())
//    }
//}
