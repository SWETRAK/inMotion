//
//  FriendsView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI

struct FriendsView: View {
    @Environment(\.managedObjectContext) private var viewContext
    
    @State var selected: String = "friends"
    @State var nickname: String = "";
    @State var name: String = "inMotion";
    @State var friends = [
        Friend(nickname: "Macius12", lastseen: "4 minutes ago", avatar: "google-logo"),
        Friend(nickname: "stephen_moustache", lastseen: "Active now", avatar: "google-logo"),
        Friend(nickname: "Eloelo", lastseen: "5 hours ago", avatar: "google-logo"),]
    var body: some View {
        VStack {
            VStack {
                Picker("Topping", selection: $selected) {
                    Text("Friends").tag("friends")
                    Text("Requests").tag("requests")
                }.pickerStyle(.segmented)
            }.padding(.horizontal)
            ScrollView{
                if(selected == "friends") {
                    YourFriendRowView(friend: $friends[0])
                    YourFriendRowView(friend: $friends[1])
                    YourFriendRowView(friend: $friends[2])
                } else {
                    RequestFriendRowView(friend: $friends[0])
                    RequestFriendRowView(friend: $friends[1])
                    RequestFriendRowView(friend: $friends[2])
                }

            }
        }.navigationTitle("Friends")
            .navigationBarTitleDisplayMode(.inline)
            .toolbar {
                ToolbarItem(placement: .primaryAction) {
                    NavigationLink {
                        FindFriendView()
                    } label: {
                        Image(systemName: "person.fill.badge.plus")
                    }.buttonStyle(.plain)
                }
            }
    }
}

struct FriendsView_Previews: PreviewProvider {
    static var previews: some View {
        FriendsView()
    }
}
