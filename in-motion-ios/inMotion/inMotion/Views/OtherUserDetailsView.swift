//
//  OtherUserDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 11/11/2023.
//

import SwiftUI

struct OtherUserDetailsView: View {
    
    @EnvironmentObject private var appState: AppState
    var user: FullUserInfoDto
    
    @State private var friendshipStatus: FriendshipStatusEnum = .Unknown
    @State var data: Data? = nil
    @State var imageSize: Double = 100.0
    
    var body: some View {
        Form {
            
            GeometryReader { proxy in
                if let safeData = data, let uiImage = UIImage(data: safeData) {
                    VStack (alignment: .center){
                        Image(uiImage: uiImage)
                            .resizable()
                            .frame(width: proxy.size.width/1.5, height: proxy.size.width/1.5, alignment: .center)
                            .position()
                            .onAppear{
                                self.imageSize = proxy.size.width/1.5 + 10.0
                            }
                    }.frame(width: proxy.size.width)
                } else {
                    VStack (alignment: .center) {
                        Image("avatar-placeholder")
                            .resizable()
                            .frame(width: proxy.size.width/1.5, height: proxy.size.width/1.5, alignment: .center)
                            .onAppear{
                                self.imageSize = proxy.size.width/1.5 + 10.0
                            }
                    }.frame(width: proxy.size.width)
                }
            }.frame(height: imageSize)
        
            Section(header: Text("User details")) {
                
                // TODO: Add user profile video
                
                LabeledContent {
                    Text(user.nickname)
                } label: {
                    Text("Nickname")
                }
                
                LabeledContent {
                    Text(user.bio ?? "")
                } label: {
                    Text("Bio")
                }
            }
            Section(header: Text("Friendship")) {
                
                if (self.friendshipStatus == .Accepted) {
                    Button {
                        self.UnfriendFriendshipRequest(person: user)
                    } label: {
                        Text ("Unfriend").foregroundColor(.red)
                    }
                } else if (self.friendshipStatus == .Requested) {
                    Button {
                        self.AcceptFriendshipRequest(person: user)
                    } label: {
                        Text("Accept request").foregroundColor(.green)
                    }

                    Button {
                        self.RejectFriendshipRequest(person: user)
                    } label: {
                        Text("Reject request").foregroundColor(.red)
                    }
                } else if (self.friendshipStatus == .Invited) {
                    Button {
                        self.InvertRequest(person: user)
                    } label: {
                        Text("Revert invitation").foregroundColor(.red)
                    }
                } else if (self.friendshipStatus == .Unknown) {
                    Button {
                        self.SendInvitation(person: user)
                    } label: {
                        Text("Add friends").foregroundColor(.blue)
                    }
                }
            }
        }.onAppear{
            GetFriendshipStatus(person: user)
        }
    }
    
    private func AcceptFriendshipRequest(person: FullUserInfoDto) {
        let request = self.appState.requestedFriendships.first { x in
            return x.externalUserId == person.id
        }
        
        if let requestSafe = request {
            appState.acceptFriendshipHttpRequest(
                friendshipId: requestSafe.id,
                successAcceptUserAction: {(data: AcceptedFriendshipDto) in
                    DispatchQueue.main.async {
                        self.GetFriendshipStatus(person: person)
                    }
                },
                failureAcceptUserAction: {(error: ImsHttpError) in })
        }
    }
    
    private func RejectFriendshipRequest(person: FullUserInfoDto) {
        let request = self.appState.requestedFriendships.first { x in
            return x.externalUserId == person.id
        }
        
        if let requestSafe = request {
            appState.rejectFriendshipHttpRequest(
                friendshipId: requestSafe.id,
                successRejectFriendshipAction: {(data: RejectedFriendshipDto) in
                    DispatchQueue.main.async {
                        self.GetFriendshipStatus(person: person)
                    }
                },
                failureRejectFriendshipAction: {(error: ImsHttpError) in })
        }
    }
    
    private func UnfriendFriendshipRequest(person: FullUserInfoDto) {
        let request: AcceptedFriendshipDto? = self.appState.acceptedFriendships.first { x in
            return x.externalUserId == person.id
        }
        
        if let requestSafe = request {
            appState.unfiendsFriendshipHttpRequest(
                friendshipId: requestSafe.id,
                successUnfriendFriendshipAction: {(data: RejectedFriendshipDto) in
                    DispatchQueue.main.async {
                        self.GetFriendshipStatus(person: person)
                    }
                },
                failureUnfriendFriendshipAction: {(error: ImsHttpError) in })
        }
    }
    
    private func SendInvitation(person: FullUserInfoDto) {
        print(person.id.uuidString)
        appState.createFriendshipHttpRequest(
            otherUserId: person.id,
            successCreateFriendshipAction: {(data: InvitationFriendshipDto) in
                DispatchQueue.main.async {
                    self.GetFriendshipStatus(person: person)
                }
            },
            failureCreateFriendshipAction: {(error: ImsHttpError) in })
    }
    
    private func InvertRequest(person: FullUserInfoDto) {
        
        let request = self.appState.invitedFriendships.first { x in
            return x.externalUserId == person.id
        }
        
        if let requestSafe = request {
            self.appState.revertFriendshipHttpRequest(
                friendshipId: requestSafe.id,
                successRevertFriendshipAction: {(data: Bool) in
                    DispatchQueue.main.async {
                        self.GetFriendshipStatus(person: person)
                    }
                },
                failureRevertFriendshipAction: {(error: ImsHttpError) in })
        }
    }
    
    private func GetFriendshipStatus(person: FullUserInfoDto){
        if(self.appState.user!.id == person.id) {
            self.friendshipStatus = FriendshipStatusEnum.IsSelf
        } else if (self.appState.invitedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            self.friendshipStatus = FriendshipStatusEnum.Invited
        } else if (self.appState.acceptedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            self.friendshipStatus = FriendshipStatusEnum.Accepted
        } else if (self.appState.requestedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            self.friendshipStatus = FriendshipStatusEnum.Requested
        } else {
            self.friendshipStatus = FriendshipStatusEnum.Unknown
        }
    }
}

//struct OtherUserDetailsView_Previews: PreviewProvider {
//    static var previews: some View {
//        OtherUserDetailsView()
//    }
//}
