//
//  LoggedUserDetailsView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 03/11/2023.
//

import SwiftUI
import GoogleSignIn

struct LoggedUserDetailsView: View {
    
    @EnvironmentObject private var appState: AppState
    
    @State var showAlert: Bool = false
    @State var addGoogleProviderAlert: Bool = false
    @State var communicationError: Bool = false
    @State var emailError: Bool = false
    @State var showAddPassword: Bool = false
    @State var showChangePassword:Bool = false
    
    @State var newNickname: String = ""
    @State var newEmail: String = ""
    
    var body: some View {
        Form {
            Section(header: Text("User details")) {
                
                // TODO: Add changing user profile video
                
                LabeledContent {
                    HStack{
                        TextField("Nickname", text: self.$newNickname)
                        Button {
                            
                            self.appState.updateUserNicknameHttpRequest(
                                requestData: UpdateNicknameDto(nickname: self.newNickname),
                                successNicknameUpdateAction: {(data: UserInfoDto) in },
                                failureNicknameUpdateAction: {(error: ImsHttpError) in })
                            
                        } label: {
                            Image(systemName: "shift.fill")
                                .resizable()
                                .foregroundColor(self.newNickname == self.appState.user?.nickname ? .gray : .green)
                                .frame(width: 15, height: 15)
                        }.disabled(self.newNickname == self.appState.user?.nickname)
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
                            
                            self.appState.updateUserEmailHttpRequest(
                                requestData: UpdateEmailDto(email: self.newEmail),
                                successEmailUpdateAction: {(user: UserInfoDto) in },
                                failureEmailUpdateAction: {(error: ImsHttpError) in
                                    if (error.status == 401) {
                                        self.emailError = true
                                        self.showAlert = true
                                    }
                                    print(error.status)
                                })
                            
                        } label: {
                            Image(systemName: "shift.fill")
                                .resizable()
                                .foregroundColor(self.newEmail == self.appState.user?.email ? .gray : .green)
                                .frame(width: 15, height: 15)
                        }.disabled(self.newEmail == self.appState.user?.email)
                    }
                } label: {
                    Text("Email")
                }

                // TODO: Update bio of logged user

            }
            
            Section(header: Text("Security")) {
                
                if (appState.user!.providers.contains("Password")) {
                    Button {
                        self.showChangePassword = true
                    } label: {
                        Text("Change Password")
                    }.sheet(isPresented: self.$showChangePassword) {
                        ChangePassword().environmentObject(appState)
                    }
                } else {
                    Button {
                        self.showAddPassword = true
                    } label: {
                        Text("Add password to account")
                    }.sheet(isPresented: self.$showAddPassword) {
                        AddPasswordToAccount().environmentObject(appState)
                    }
                }
                
            
                // MARK: - GOOGLE Button
                
                Button {
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
                                appState.addGoogleProviderHttpRequest(requestData: AuthenticateWithGoogleProviderDto(userId: userID, token: idToken.tokenString),
                                    successAddGoogleProvider: {(data: Bool) in
                                        self.addGoogleProviderAlert = false
                                    },
                                    failureAddGoogleProvider: {(error: ImsHttpError) in
                                        print(error.errorMessage)
                                        if(error.status == 409) {
                                            self.showAlert = true
                                            self.addGoogleProviderAlert = true
                                        } else if (error.status == 500) {
                                            self.showAlert = true
                                            self.communicationError = true
                                        }
                                    })
                            }
                        }
                    }
                } label: {
                    HStack{
                        Image("google-logo")
                                .resizable()
                                .frame(width: 50, height: 50)
                        Text("Login with google")
                    }.frame(alignment: .center)
                }.disabled(appState.user!.providers.contains("Google"))
            }
        }.alert(isPresented: $showAlert) {
            if (self.communicationError) {
                return Alert(
                        title: Text("No communication with server"),
                        message: Text("We have internal server problems, we work on them"),
                        dismissButton: .default(Text("Ok")) {
                            self.communicationError = false
                        }
                )
            } else if (self.addGoogleProviderAlert) {
                return Alert(
                        title: Text("This provider is connected to other account"),
                        dismissButton: .default(Text("Ok")) {
                            self.addGoogleProviderAlert = false
                        }
                )
            }
            else if (self.emailError) {
                return Alert(
                        title: Text("This email is taken by other user"),
                        dismissButton: .default(Text("Ok")) {
                            self.emailError = false
                        }
                )
            } else {
                return Alert (
                    title: Text("Unknown error")
                )
            }
        }.onAppear{
            self.newEmail = self.appState.user!.email
            self.newNickname = self.appState.user!.nickname
        }
        
    }
}

struct LoggedUserDetailsView_Previews: PreviewProvider {
    static var previews: some View {
        LoggedUserDetailsView()
    }
}
