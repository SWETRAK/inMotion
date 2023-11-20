//
//  LoggedUserDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 03/11/2023.
//

import SwiftUI
import GoogleSignIn
import SDWebImageSwiftUI
import AVKit

struct LoggedUserDetailsView: View {
    
    @EnvironmentObject public var appState: AppState
    
    @State private var addGoogleProviderAlert: Bool = false
    @State private var communicationError: Bool = false
    @State private var emailError: Bool = false
    @State private var showAddPassword: Bool = false
    @State private var showChangePassword:Bool = false
    
    @State private var newNickname: String = ""
    @State private var newEmail: String = ""
    @State private var newBio: String = ""
    
    @State private var imageSize: Double = 100.0
    @State private var avPlayer: AVPlayer? = nil
    
    var body: some View {
        Form {
            Section(header: Text("User details")) {
                
                NavigationLink {
                    UserProfileVideoCameraView().environmentObject(appState)
                } label: {
                    GeometryReader { proxy in
                        if(self.avPlayer != nil) {
                            VStack (alignment: .center) {
                                VideoPlayer(player: self.avPlayer)
                                    .frame(width: proxy.size.width/1.5, height: proxy.size.width/1.5/(3/4), alignment: .center)
                                    .onAppear{
                                        self.imageSize = proxy.size.width/1.5/(3/4)
                                        self.OnVideoAppear()
                                    }
                            }
                            .frame(width: proxy.size.width)
                        } else {
                            VStack (alignment: .center) {
                                Image("avatar-placeholder")
                                    .resizable()
                                    .frame(width: proxy.size.width/1.5, height: proxy.size.width/1.5/(3/4), alignment: .center)
                                    .onAppear{
                                        self.imageSize = proxy.size.width/1.5/(3/4)
                                    }
                            }
                            .frame(width: proxy.size.width)
                        }
                    }
                    .frame(height: imageSize)
                }

                LabeledContent {
                    HStack{
                        TextField("Nickname", text: self.$newNickname)
                        Button {
                            self.UpdateNickname()
                        } label: {
                            Image(systemName: "shift.fill")
                                .resizable()
                                .foregroundColor(self.newNickname == self.appState.user?.nickname ? .gray : .green)
                                .frame(width: 15, height: 15)
                        }
                        .disabled(self.newNickname == self.appState.user?.nickname)
                    }
                } label: {
                    Text("Nickname")
                }
                
                LabeledContent {
                    HStack {
                        TextField("Email", text: self.$newEmail)
                            .keyboardType(.emailAddress)
                            .autocorrectionDisabled(true)
                        
                        Button {
                            self.UpdateUserEmail()
                        } label: {
                            Image(systemName: "shift.fill")
                                .resizable()
                                .foregroundColor(self.newEmail == self.appState.user?.email ? .gray : .green)
                                .frame(width: 15, height: 15)
                        }
                        .disabled(self.newEmail == self.appState.user?.email)
                        .alert("This email is taken by other user", isPresented: self.$emailError) {
                            Button("Ok", role: .cancel) {}
                        }
                    }
                } label: {
                    Text("Email")
                }
                
                VStack(alignment: .leading) {
                    Text("BIO")
                    HStack {
                        TextField("Bio", text: self.$newBio, axis: .vertical)
                        
                        Button {
                            self.UpdateUserBio()
                        } label: {
                            Image(systemName: "shift.fill")
                                .resizable()
                                .foregroundColor(self.newBio == self.appState.fullUserInfo?.bio ? .gray : .green)
                                .frame(width: 15, height: 15)
                        }.disabled(self.newBio == self.appState.fullUserInfo?.bio)
                    }
                }
            }
            
            Section(header: Text("Security")) {
                if (self.appState.user?.providers.contains("Password") ?? false) {
                    Button {
                        self.showChangePassword = true
                    } label: {
                        Text("Change Password")
                    }.sheet(isPresented: self.$showChangePassword) {
                        ChangePassword().environmentObject(self.appState)
                    }
                } else {
                    Button {
                        self.showAddPassword = true
                    } label: {
                        Text("Add password to account")
                    }.sheet(isPresented: self.$showAddPassword) {
                        AddPasswordToAccount().environmentObject(self.appState)
                    }
                }
                
                // MARK: - GOOGLE Button
                
                Button {
                    self.AddGoogleProvider()
                } label: {
                    HStack{
                        Image("google-logo")
                            .resizable()
                            .frame(width: 50, height: 50)
                        Text("Add google provider")
                    }
                    .frame(alignment: .center)
                }
                .disabled(self.appState.user?.providers.contains("Google") ?? false)
                .alert("This provider is connected to other account", isPresented: self.$addGoogleProviderAlert) {
                    Button("OK", role: .cancel) {}
                }
            }
            Section {
                Button("Logout") {
                    self.appState.logOut()
                }.foregroundColor(.red)
            }
        }.alert("No communication with server", isPresented: self.$communicationError) {
            Button("OK", role: .cancel) {}
        }.onAppear{
            self.OnViewAppear()
        }.onDisappear{
            self.OnViewDisappear()
        }
    }
    
    private func OnViewAppear() {
        self.newEmail = self.appState.user!.email
        self.newNickname = self.appState.user!.nickname
        self.newBio = self.appState.fullUserInfo?.bio ?? ""
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
    
    private func UpdateNickname() {
        self.appState.updateUserNicknameHttpRequest(
            requestData: UpdateNicknameDto(nickname: self.newNickname),
            successNicknameUpdateAction: {(data: UserInfoDto) in },
            failureNicknameUpdateAction: {(error: ImsHttpError) in })
    }
    
    private func UpdateUserBio() {
        self.appState.updateUserBioHttpRequest(
            requestData: UpdateUserBioDto(bio: self.newBio),
            successUpdateUserBioAction: {(data: UpdatedUserBioDto) in
                self.newBio = data.newBio
            },
            failureUpdateUserBioAction: {(error: ImsHttpError) in })
    }
    
    private func UpdateUserEmail() {
        self.appState.updateUserEmailHttpRequest(
            requestData: UpdateEmailDto(email: self.newEmail),
            successEmailUpdateAction: {(user: UserInfoDto) in },
            failureEmailUpdateAction: {(error: ImsHttpError) in
                if (error.status == 401) {
                    self.emailError = true
                }
            })
    }
    
    private func AddGoogleProvider() {
        guard let windowScene = UIApplication.shared.connectedScenes.first as? UIWindowScene else { return }
        guard let rootViewController = windowScene.windows.first?.rootViewController else { return }
        let signInConfig = GIDConfiguration(clientID: "435519606946-0d3d75lo1askeorlrn21355csa1hsd9h.apps.googleusercontent.com")
        
        GIDSignIn.sharedInstance.configuration = signInConfig
        
        GIDSignIn.sharedInstance.signIn(withPresenting: rootViewController ) { (user, error )in
            
            guard let user = user else {
                return
            }
            
            if let userID = user.user.userID {
                if let idToken = user.user.idToken {
                    appState.addGoogleProviderHttpRequest(
                        requestData: AuthenticateWithGoogleProviderDto(userId: userID, token: idToken.tokenString),
                        successAddGoogleProvider: {(data: Bool) in
                            self.addGoogleProviderAlert = false
                        },
                        failureAddGoogleProvider: {(error: ImsHttpError) in
                            if(error.status == 401) {
                                self.addGoogleProviderAlert = true
                            } else if (error.status == 500) {
                                self.communicationError = true
                            }
                        })
                }
            }
        }
    }
    
    private func LoadProfilePicture() {
        if let safeUserId = self.appState.user?.id {
            self.appState.getUserVideoHttpRequest(
                userId: safeUserId) { (data: Data) in
                    self.PreparePlayer(data)
                } failureGetUserProfileVideoUrl: { (error: ImsHttpError) in }
        }
    }
    
    private func PreparePlayer(_ data: Data) {
        self.avPlayer = AVPlayer(playerItem: AVPlayerItem(asset: data.convertToAVAsset()))
        self.avPlayer?.isMuted = true
    }
}

//struct LoggedUserDetailsView_Previews: PreviewProvider {
//    static var previews: some View {
//        LoggedUserDetailsView()
//    }
//}
