//
//  YourFriendRowView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI
import CoreData

struct YourFriendRowView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject var appState: AppState
    @EnvironmentObject var friendship: Friendship
    
    var body: some View {
        HStack{
            Image((appState.user?.id == friendship.userOne?.id ? friendship.userTwo?.profile_photo : friendship.userOne?.profile_photo) ?? "google-logo").resizable().frame(width:50, height:50)
            VStack(alignment: .leading){
                Text((appState.user?.id == friendship.userOne?.id ? friendship.userTwo?.nickname : friendship.userOne?.nickname) ?? "nickname").fontWeight(Font.Weight.bold).frame(maxWidth: .infinity, alignment: .leading)
            }
            .frame(maxWidth: .infinity, alignment: .leading)
            Spacer()
        }
        .frame(alignment: .leading)
    }
}

struct YourFriendRowView_Previews: PreviewProvider {
    static var previews: some View {
        YourFriendRowView()
    }
}
