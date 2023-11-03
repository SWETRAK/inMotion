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
    
    @State var newNickname: String = ""
    @State var newEmail: String = ""
    
    var body: some View {
        Form {
            
            
            Section(header: Text("User details")) {
                // TODO: Add changing user profile video
                
                TextField("Nickname", text: self.$newNickname)
                
                TextField("Email", text: self.$newEmail)
                
                // TODO: Update bio of logged user
                
                Button("Save changes") {
                    
                }
            }
            
            Section(header: Text("Security")) {
                
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
                }
            
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
//            else if (self.passwordError) {
//                return Alert(
//                        title: Text("Incorrect password"),
//                        dismissButton: .default(Text("Ok")) {
//                            self.passwordError = false
//                        }
//                )
//            } else {
//                return Alert(
//                        title: Text("Incorrect login data or user already exists"),
//                        dismissButton: .default(Text("Ok")) {
//                            self.loginError = false
//                        }
//                )
//            }
            else {
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
