import SwiftUI
import GoogleSignIn

struct LoginView: View {
    
    @EnvironmentObject public var appState: AppState
    
    @State private var email: String = ""
    @State private var password: String = ""
    @State private var emailError: Bool = false
    @State private var passwordError: Bool = false
    @State private var loginError: Bool = false
    @State private var communicationError: Bool = false
    
    var body: some View {
        VStack {
            Spacer()
            Text("inMotion")
                .font(.custom("Roboto", fixedSize: 50))
            Spacer()
            
            Form {
                Section(header: Text("Login with email")) {
                    TextField("Email", text: self.$email)
                        .keyboardType(.emailAddress)
                        .textInputAutocapitalization(.never)
                        .disableAutocorrection(true)
                        .alert("Incorrect email address", isPresented: self.$emailError) {
                            Button("Ok", role: .cancel) {}
                        }
                    
                    SecureField("Password", text: self.$password)
                        .alert("Incorrect password", isPresented: self.$passwordError) {
                            Button("Ok", role: .cancel) {}
                        }
                    
                    Button("Login with email") {
                        self.LoginWithEmailAndPassword()
                    }
                    .alert("Incorrect credentials or no exists", isPresented: self.$loginError) {
                        Button("Ok", role: .cancel) {}
                    }
                }
                Section(header: Text("Social login")) {
                    Button {
                        self.LoginWithGoogleProvider()
                    } label: {
                        HStack{
                            Image("google-logo")
                                .resizable()
                                .frame(width: 50, height: 50)
                            Text("Login with google")
                        }
                        .frame(alignment: .center)
                    }
                }
            }
            .frame(alignment: .center)
            
            NavigationLink("Register new account", destination: RegisterView().environmentObject(appState))
        }
        .navigationBarHidden(true)
        .alert("No communication with server", isPresented: self.$communicationError) {
            Button("Ok", role: .cancel) {}
        }
    }
    
    private func LoginWithEmailAndPassword() {
        self.ValidatePassword()
        self.ValidateEmail()
        if (!self.emailError && !self.passwordError) {
            self.appState.loginWithEmailAndPasswordHttpRequest(
                requestData: LoginUserWithEmailAndPasswordDto(email: email, password: password),
                successLoginAction: { (userInfoDto) in
                    self.loginError = false
                },
                failureLoginAction: { (httpError) in
                    if (httpError.status == 404) {
                        self.loginError = true
                    } else if (httpError.status == 500) {
                        self.communicationError = true
                    }
                })
        }
    }
    
    private func LoginWithGoogleProvider() {
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
                    self.appState.loginWithGoogleHttpRequest(
                        requestData: AuthenticateWithGoogleProviderDto(userId: userID, token: idToken.tokenString),
                        successRegisterWithGoogle: {(data: UserInfoDto) in
                            self.loginError = false
                        },
                        failureRegisterWithGoogle: {(error: ImsHttpError) in
                            if(error.status == 401) {
                                self.loginError = true
                            } else if (error.status == 500) {
                                self.communicationError = true
                            }
                        })
                }
            }
        }
    }
    
    private func ValidateEmail() {
        if (self.email.isEmpty) {
            self.emailError = true
        } else {
            let emailFormat = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}"
            let emailPredicate = NSPredicate(format: "SELF MATCHES %@", emailFormat)
            self.emailError = !emailPredicate.evaluate(with: self.email)
        }
    }
    
    private func ValidatePassword() {
        if (self.password.isEmpty) {
            self.passwordError = true
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
