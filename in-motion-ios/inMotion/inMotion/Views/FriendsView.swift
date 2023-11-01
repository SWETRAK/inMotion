//
//  FriendsView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI
import CoreData

struct FriendsView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var appState: AppState
    
    @State private var selected: String = "friends"
    // @State private var friendships: [Friendship] = []
    
    var body: some View {
        VStack {
//            VStack {
//                Picker("Topping", selection: $selected) {
//                    Text("Friends").tag("friends")
//                    Text("Requests").tag("requests")
//                }.pickerStyle(.segmented)
//            }.padding(.horizontal)
//            if(selected == "friends") {
//                List(friendships, id: \.id){
//                    friendship in
//                    YourFriendRowView()
//                        .environmentObject(appState)
//                        .environmentObject(friendship)
//                        .swipeActions {
//                            Button(role: .destructive) {
//                                removeFriend(friendship: friendship)
//                            } label: {
//                                Image(systemName: "person.fill.xmark.rtl")
//                            }
//                            .tint(.red)
//                        }
//                }.onAppear{
//                    LoadFriends()
//                }
//            }else {
//                List(friendships, id:\.id) { friendship in
//                    YourFriendRowView()
//                        .environmentObject(appState)
//                        .environmentObject(friendship)
//                        .swipeActions {
//                            Button {
//                                acceptFriend(friendship: friendship)
//                            } label: {
//                                Image(systemName: "person.fill.checkmark.rtl")
//                            }.tint(.green)
//
//                            Button(role: .destructive) {
//                                rejectFriend(friendship: friendship)
//                            } label: {
//                                Image(systemName: "person.fill.xmark.rtl")
//                            }
//                            .tint(.red)
//                        }
//                }.onAppear{
//                    LoadFriendsRequest()
//                }
//
//            }
        }.navigationTitle("Friends")
            .navigationBarTitleDisplayMode(.inline)
            .toolbar {
                ToolbarItem(placement: .primaryAction) {
                    NavigationLink {
//                        FindFriendView()
//                            .environmentObject(appState)
                    } label: {
                        Image(systemName: "person.fill.badge.plus")
                    }.buttonStyle(.plain)
                }
            }
    }
    
//    private func LoadFriends() {
//        if let safeUser = appState.user {
//            let request: NSFetchRequest<Friendship> = Friendship.fetchRequest()
//            let predictate = NSPredicate(format: "(userOne.id == %@ || userTwo.id == %@) && status == %@", safeUser.id! as CVarArg, safeUser.id! as CVarArg, FriendshipStatusEnum.Accepted.rawValue)
//            request.predicate = predictate
//            do {
//                self.friendships = try viewContext.fetch(request)
//            } catch {
//                print("Error fetchig data from context \(error)")
//            }
//        }
//    }
    
//    private func LoadFriendsRequest() {
//        if let safeUser = appState.user {
//            let request: NSFetchRequest<Friendship> = Friendship.fetchRequest()
//            let predictate = NSPredicate(format: "userTwo.id == %@ && status == %@", safeUser.id! as CVarArg, FriendshipStatusEnum.Requested.rawValue)
//            request.predicate = predictate
//            do {
//                self.friendships = try viewContext.fetch(request)
//            } catch {
//                print("Error fetchig data from context \(error)")
//            }
//        }
//    }
//
//    private func removeFriend(friendship: Friendship) {
//        viewContext.delete(friendship)
//        LoadFriends()
//    }
//
//    private func rejectFriend(friendship: Friendship) {
//        viewContext.delete(friendship)
//        LoadFriendsRequest()
//    }
//
//    private func acceptFriend(friendship: Friendship) {
//        friendship.status = FriendshipStatusEnum.Accepted.rawValue
//        do {
//            try viewContext.save()
//        } catch {
//            print("\(error)")
//        }
//        LoadFriendsRequest()
//    }
}

struct FriendsView_Previews: PreviewProvider {
    static var previews: some View {
        FriendsView()
    }
}
