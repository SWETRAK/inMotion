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
    
    // TODO: Add user page
    var body: some View {
        VStack {
            VStack {
                Picker("Topping", selection: $selected) {
                    Text("Friends").tag("friends")
                    Text("Requests").tag("requests")
                }.pickerStyle(.segmented)
            }.padding(.horizontal)
            if(selected == "friends") {
                List(appState.acceptedFriendships, id: \.id){
                    acceptedFriendship in
                    NavigationLink {
                        OtherUserDetailsView(user: acceptedFriendship.ParseToFullUserInfoDto()).environmentObject(appState)
                    } label: {
                        YourFriendRowView(friendship: acceptedFriendship.externalUser)
                            .swipeActions {
                                Button(role: .destructive) {
                                    UnfriendFriendshipRequest(acceptedFriendship: acceptedFriendship)
                                } label: {
                                    Image(systemName: "person.fill.xmark.rtl")
                                }
                                .tint(.red)
                            }
                    }
                }
            } else {
                List(appState.requestedFriendships, id:\.id) { requestedFriendship in
                    NavigationLink {
                        OtherUserDetailsView(user: requestedFriendship.ParseToFullUserInfoDto()).environmentObject(appState)
                    } label: {
                        YourFriendRowView(friendship: requestedFriendship.externalUser)
                            .swipeActions {
                                Button {
                                    AcceptFriendshipRequest(requestedFriendship: requestedFriendship)
                                } label: {
                                    Image(systemName: "person.fill.checkmark.rtl")
                                }.tint(.green)
                                
                                Button(role: .destructive) {
                                    RejectFriendshipRequest(requestedFriendship: requestedFriendship)
                                } label: {
                                    Image(systemName: "person.fill.xmark.rtl")
                                }
                                .tint(.red)
                            }
                    }
                }
            }
        }.navigationTitle("Friends")
            .navigationBarTitleDisplayMode(.inline)
            .toolbar {
                ToolbarItem(placement: .primaryAction) {
                    NavigationLink {
                        FindFriendView()
                            .environmentObject(appState)
                    } label: {
                        Image(systemName: "person.fill.badge.plus")
                    }.buttonStyle(.plain)
                }
            }
    }
        
    private func AcceptFriendshipRequest(requestedFriendship: RequestFriendshipDto) {
        appState.acceptFriendshipHttpRequest(
            friendshipId: requestedFriendship.id,
            successAcceptUserAction: {(data: AcceptedFriendshipDto) in },
            failureAcceptUserAction: {(error: ImsHttpError) in })
    }
    
    private func RejectFriendshipRequest(requestedFriendship: RequestFriendshipDto) {
        appState.rejectFriendshipHttpRequest(
            friendshipId: requestedFriendship.id,
            successRejectFriendshipAction: {(data: RejectedFriendshipDto) in },
            failureRejectFriendshipAction: {(error: ImsHttpError) in })
    }
    
    private func UnfriendFriendshipRequest(acceptedFriendship: AcceptedFriendshipDto) {
        appState.unfiendsFriendshipHttpRequest(
            friendshipId: acceptedFriendship.id,
            successUnfriendFriendshipAction: {(data: RejectedFriendshipDto) in },
            failureUnfriendFriendshipAction: {(error: ImsHttpError) in })
    }
}

struct FriendsView_Previews: PreviewProvider {
    static var previews: some View {
        FriendsView()
    }
}
