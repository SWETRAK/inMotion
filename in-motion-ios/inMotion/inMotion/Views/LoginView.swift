import SwiftUI
import GoogleSignIn

struct LoginView: View {
    
    @EnvironmentObject private var appState: AppState
    
    @State private var email: String = ""
    @State private var password: String = ""
    
    @State private var showAlert: Bool = false
    
    @State private var emailError: Bool = false
    @State private var passwordError: Bool = false
    @State private var loginError: Bool = false
    @State private var communicationError: Bool = false
    
    
    
    var body: some View {
        VStack {
            Spacer()
            Text("inMotion").font(.custom("Roboto", fixedSize: 50))
            Spacer()
            
            Form {
                Section(header: Text("Login with email")) {
                    TextField("Email", text: $email)
                        .keyboardType(.emailAddress)
                        .textInputAutocapitalization(.never)
                        .disableAutocorrection(true)
                    
                    SecureField("Password", text: $password)
                    
                    Button("Login with email") {
                        self.ValidatePassword()
                        self.ValidateEmail()
                        if (!self.emailError && !self.passwordError) {
                            appState.loginWithEmailAndPasswordHttpRequest(requestData: LoginUserWithEmailAndPasswordDto(email: email, password: password),
                                                                          successLoginAction: { (userInfoDto) in
                                appState.getUserByIdHttpRequest(userId: appState.user!.id,
                                                                successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                    self.appState.fullUserInfo = fullUserInfo
                                }, failureGetUserAction: { (error: ImsHttpError) in
                                    
                                })
                                self.loginError = false
                            },
                                                                          failureLoginAction: { (httpError) in
                                if (httpError.status == 404) {
                                    self.showAlert = true
                                    self.loginError = true
                                } else if (httpError.status == 500) {
                                    self.showAlert = true
                                    self.communicationError = true
                                }
                            })
                        }
                    }
                }
                Section(header: Text("Social login")) {
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
                                    appState.loginWithGoogleHttpRequest(requestData: AuthenticateWithGoogleProviderDto(userId: userID, token: idToken.tokenString),
                                                                        successRegisterWithGoogle: {(data: UserInfoDto) in
                                        self.loginError = false
                                        
                                        appState.getUserByIdHttpRequest(userId: appState.user!.id,
                                                                        successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                            self.appState.fullUserInfo = fullUserInfo
                                        }, failureGetUserAction: { (error: ImsHttpError) in
                                            
                                        })
                                    },
                                                                        failureRegisterWithGoogle: {(error: ImsHttpError) in
                                        if(error.status == 401) {
                                            self.showAlert = true
                                            self.loginError = true
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
            }.frame(alignment: .center)
            NavigationLink("Register new account", destination: RegisterView().environmentObject(appState))
        }.navigationBarHidden(true)
            .alert(isPresented: $showAlert) {
                if (self.communicationError) {
                    return Alert(
                        title: Text("No communication with server"),
                        message: Text("We have internal server problems, we work on them"),
                        dismissButton: .default(Text("Ok")) {
                            self.communicationError = false
                        }
                    )
                } else if (self.emailError) {
                    return Alert(
                        title: Text("Incorrect email address"),
                        dismissButton: .default(Text("Ok")) {
                            self.emailError = false
                        }
                    )
                } else if (self.passwordError) {
                    return Alert(
                        title: Text("Incorrect password"),
                        dismissButton: .default(Text("Ok")) {
                            self.passwordError = false
                        }
                    )
                } else {
                    return Alert(
                        title: Text("Incorrect login data or user already exists"),
                        dismissButton: .default(Text("Ok")) {
                            self.loginError = false
                        }
                    )
                }
            }
    }
    
    private func ValidateEmail() {
        if (self.email.isEmpty) {
            self.showAlert = true
            self.emailError = true
        } else {
            let emailFormat = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}"
            let emailPredicate = NSPredicate(format: "SELF MATCHES %@", emailFormat)
            self.emailError = !emailPredicate.evaluate(with: self.email)
            if (self.emailError) {
                self.showAlert = true
            }
        }
    }
    
    private func ValidatePassword() {
        if (self.password.isEmpty) {
            self.passwordError = true
            self.showAlert = true
        } else {
            self.passwordError = false
        }
    }
}

struct LoginView_Previews: PreviewProvider {
    static var previews: some View {
        LoginView()
    }
}
