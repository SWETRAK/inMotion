import SwiftUI
import CoreData

struct RegisterView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var appState: AppState
    
    @State private var nickname: String = ""
    @State private var email: String = ""
    @State private var password: String = ""
    @State private var repeatPassword: String = ""

    @State private var showAlert: Bool = false
    
    @State private var emailError: Bool = false
    @State private var nicknameError: Bool = false
    @State private var passwordError: Bool = false
    @State private var repeatPasswordError: Bool = false
    
    @Environment(\.presentationMode) var presentationMode: Binding<PresentationMode>
    var body: some View {
        VStack(spacing: 20.0) {
                
            Spacer()

            Text("inMotion").font(.custom("Roboto", fixedSize: 50))

            Form {
                Section {
                    TextField("Nickname", text: $nickname)

                    TextField("Email", text: $email)
                        .keyboardType(.emailAddress)
                        .textInputAutocapitalization(.never)

                    SecureField("Password", text: $password)

                    SecureField("Repeat password", text: $repeatPassword)

                    Button("Register with email and password"){
                        self.ValidatePassword()
                        self.ValidateRepeatPassword()
                        self.ValidateEmail()
                        self.ValidateNickname()

                        if(!self.repeatPasswordError && !self.nicknameError && !self.passwordError && !self.emailError) {
                            appState.registerUserWithEmailAndPasswordHttpRequest(registerData: RegisterUserWithEmailAndPasswordDto(email: self.email, password: self.password, repeatPassword: self.repeatPassword, nickname: self.nickname),
                                    successRegisterAction: {(successData: SuccessfulRegistrationResponseDto) in
                                        // TODO: Go to login page
                                    },
                                    validationRegisterAction: {(validationErrors: Dictionary<String, [String]>) in
                                        if (validationErrors.keys.contains("Email")) {
                                            self.emailError = true
                                            self.showAlert = true
                                        }
                                        // TODO: Add password validation
                                    },
                                    failureRegisterAction: {(error: ImsHttpError) in
                                        print(error.status, error.errorMessage, error.errorType)
                                    })
                        }
                    }.alert(isPresented: $showAlert) {
                        if (self.emailError) {
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
                        } else if (self.nicknameError) {
                            return Alert(
                                title: Text("Nickname can`t be empty"),
                                dismissButton: .default(Text("Ok")) {
                                    self.nicknameError = false
                                }
                            )
                        } else {
                            return Alert(
                                title: Text("Incorrect repeat password"),
                                dismissButton: .default(Text("Ok")) {
                                    self.repeatPasswordError = false
                                }
                            )
                        }
                    }
                }
            }
            Button("Login to existing account"){
                self.presentationMode.wrappedValue.dismiss()
            }
        }
        .navigationBarHidden(true)
    }
    
    private func ValidateNickname() {
        if(self.nickname.isEmpty) {
            self.nicknameError = true
            self.showAlert = true
        } else {
            self.nicknameError = false
        }
    }

    private func ValidateEmail(){
        if(self.email.isEmpty) {
            self.emailError = true
            self.showAlert = true
        } else {
            let emailFormat = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}" // short format
            let emailPredicate = NSPredicate(format:"SELF MATCHES %@", emailFormat)
            self.emailError = !emailPredicate.evaluate(with: self.email)
            self.showAlert = self.emailError
        }
    }

    private func ValidatePassword() {
        if(self.password.isEmpty) {
            self.passwordError = true
            self.showAlert = true
        } else {
            self.passwordError = false
        }
    }

    private func ValidateRepeatPassword() {
        if(self.repeatPassword.isEmpty) {
            self.repeatPasswordError = true
            self.showAlert = true
        } else {
            if(self.password != self.repeatPassword){
                self.repeatPasswordError = true
                self.showAlert = true
            } else {
                self.repeatPasswordError = false
            }
        }
    }
}

struct RegisterView_Previews: PreviewProvider {
    static var previews: some View {
        RegisterView()
    }
}
