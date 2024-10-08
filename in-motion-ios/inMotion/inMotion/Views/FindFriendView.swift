//
//  FindFriendView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import CoreData

struct FindFriendView: View {
    @EnvironmentObject public var appState: AppState
    @State private var nickname: String = "";
    
    @State private var persons: [FullUserInfoDto] = []
    @State private var refresh: Bool = false
    
    var body: some View {
        VStack {
            TextField("Find a friend...", text: $nickname)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(.horizontal)
                .textInputAutocapitalization(.never)
                .onChange(of: nickname) { newValue in
                    self.FindUsers()
                }
            
            List(persons, id: \.id){person in
                
                NavigationLink {
                    if (GetFriendshipStatus(person: person) == FriendshipStatusEnum.IsSelf) {
                        LoggedUserDetailsView().environmentObject(appState)
                            .navigationBarTitleDisplayMode(.inline)
                    } else {
                        OtherUserDetailsView(user: person).environmentObject(appState)
                            .navigationBarTitleDisplayMode(.inline)
                    }
                } label: {
                    PersonRowView(fullUserInfo: person)
                        .environmentObject(self.appState)
                        .swipeActions {
                            if(GetFriendshipStatus(person: person) == FriendshipStatusEnum.Unknown){
                                HStack {
                                    Button {
                                        self.SendInvitation(person: person)
                                    } label: {
                                        Image(systemName: "plus")
                                    }
                                    .tint(.blue)
                                }
                            } else if (GetFriendshipStatus(person: person) == FriendshipStatusEnum.Invited){
                                HStack {
                                    Button {
                                        self.InvertRequest(person: person)
                                    } label: {
                                        Image(systemName: "trash")
                                    }.tint(.red)
                                }
                            }
                        }
                }
            }
        }
    }
    
    private func FindUsers() {
        self.appState.getUsersByNicknameHttpRequest(
            nickname: self.nickname,
            successGetUserAction: {(data: [FullUserInfoDto]) in
                self.persons = data
            },
            failureGetUserAction: {(error: ImsHttpError) in
                
            })
    }
    
    private func SendInvitation(person: FullUserInfoDto) {
        appState.createFriendshipHttpRequest(
            otherUserId: person.id,
            successCreateFriendshipAction: {(data: InvitationFriendshipDto) in },
            failureCreateFriendshipAction: {(error: ImsHttpError) in })
    }
    
    private func InvertRequest(person: FullUserInfoDto) {
        
        let request = self.appState.invitedFriendships.first { x in
            return x.externalUserId == person.id
        }
        
        if let requestSafe = request {
            self.appState.revertFriendshipHttpRequest(
                friendshipId: requestSafe.id,
                successRevertFriendshipAction: {(data: Bool) in },
                failureRevertFriendshipAction: {(error: ImsHttpError) in })
        }
    }
    
    private func GetFriendshipStatus(person: FullUserInfoDto) -> FriendshipStatusEnum {
        if(self.appState.user!.id == person.id) {
            return FriendshipStatusEnum.IsSelf
        }
        
        if (self.appState.invitedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            return FriendshipStatusEnum.Invited
        }
        
        if (self.appState.acceptedFriendships.first { x in return x.externalUserId == person.id } != nil)
        {
            return FriendshipStatusEnum.Accepted
        }
        
        if (self.appState.requestedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            return FriendshipStatusEnum.Requested
        }
        
        return FriendshipStatusEnum.Unknown
    }
}
