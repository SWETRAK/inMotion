//
//  OtherUserDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 11/11/2023.
//

import SwiftUI
import AVKit

struct OtherUserDetailsView: View {
    
    @EnvironmentObject public var appState: AppState
    
    @State private var friendshipStatus: FriendshipStatusEnum = .Unknown
    @State private var imageSize: Double = 100.0
    @State private var avPlayer: AVPlayer? = nil
    
    var user: FullUserInfoDto
    
    var body: some View {
        Form {
            GeometryReader { proxy in
                if(self.avPlayer != nil) {
                    VStack (alignment: .center) {
                        VideoPlayer(player: self.avPlayer)
                            .frame(width: proxy.size.width/1.5, height: proxy.size.width/1.5/(3/4), alignment: .center)
                            .onAppear{
                                OnVideoAppear()
                            }
                    }
                    .frame(width: proxy.size.width)
                } else {
                    VStack (alignment: .center) {
                        Image("avatar-placeholder")
                            .resizable()
                            .frame(width: proxy.size.width/1.5, height: proxy.size.width/1.5/(3/4), alignment: .center)
                            .onAppear{
                                self.imageSize = proxy.size.width/1.5 + 10.0
                            }
                    }
                    .frame(width: proxy.size.width)
                }
            }
            .frame(height: self.imageSize)
            
            Section(header: Text("User details")) {
                LabeledContent {
                    Text(self.user.nickname)
                } label: {
                    Text("Nickname")
                }
                
                LabeledContent {
                    Text(self.user.bio ?? "")
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
            self.OnViewAppear()
        }.onDisappear{
            self.OnViewDisappear()
        }
    }
    
    private func OnViewAppear() {
        self.GetFriendshipStatus(person: user)
        self.LoadProfilePicture()
    }
    
    private func OnViewDisappear() {
        self.avPlayer?.pause()
        self.avPlayer?.replaceCurrentItem(with: nil)
        self.avPlayer = nil
    }
    
    private func OnVideoAppear() {
        self.avPlayer?.play()
        NotificationCenter.default.addObserver(forName: .AVPlayerItemDidPlayToEndTime, object: nil, queue: .main) { _ in
            self.avPlayer?.seek(to: .zero)
            self.avPlayer?.play()
        }
    }
    
    private func AcceptFriendshipRequest(person: FullUserInfoDto) {
        let request = self.appState.requestedFriendships.first { x in
            return x.externalUserId == person.id
        }
        
        if let requestSafe = request {
            self.appState.acceptFriendshipHttpRequest(
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
            self.appState.rejectFriendshipHttpRequest(
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
            self.appState.unfiendsFriendshipHttpRequest(
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
        self.appState.createFriendshipHttpRequest(
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
    
    func LoadProfilePicture() {
        self.appState.getUserVideoHttpRequest(
            userId: self.user.id) { (data: Data) in
                self.PreparePlayer(data)
            } failureGetUserProfileVideoUrl: { (error: ImsHttpError) in }
    }
    
    func PreparePlayer(_ data: Data) {
        self.avPlayer = AVPlayer(playerItem: AVPlayerItem(asset: data.convertToAVAsset()))
        self.avPlayer?.isMuted = true
    }
}

//struct OtherUserDetailsView_Previews: PreviewProvider {
//    static var previews: some View {
//        OtherUserDetailsView()
//    }
//}
