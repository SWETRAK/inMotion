import SwiftUI
import CoreData

struct RegisterView: View {
    @EnvironmentObject public var appState: AppState
    
    @State private var nickname: String = ""
    @State private var email: String = ""
    @State private var password: String = ""
    @State private var repeatPassword: String = ""
    
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
                    TextField("Nickname", text: self.$nickname)
                        .disableAutocorrection(true)
                        .alert("Nickname can`t be empty", isPresented: self.$nicknameError) {
                            Button("Ok", role: .cancel) {}
                        }
                    
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
                    
                    SecureField("Repeat password", text: self.$repeatPassword)
                        .alert("Incorrect repeat password", isPresented: self.$repeatPasswordError) {
                            Button("Ok", role: .cancel) {}
                        }
                    
                    Button("Register with email and password"){
                        self.RegisterWithEmailAndPassword()
                    }
                }
            }
            Button("Login to existing account"){
                self.presentationMode.wrappedValue.dismiss()
            }
        }
        .navigationBarHidden(true)
    }
    
    private func RegisterWithEmailAndPassword() {
        self.ValidatePassword()
        self.ValidateRepeatPassword()
        self.ValidateEmail()
        self.ValidateNickname()
        
        if(!self.repeatPasswordError && !self.nicknameError && !self.passwordError && !self.emailError) {
            appState.registerUserWithEmailAndPasswordHttpRequest(
                registerData: RegisterUserWithEmailAndPasswordDto(email: self.email, password: self.password, repeatPassword: self.repeatPassword, nickname: self.nickname),
                successRegisterAction: {(successData: SuccessfulRegistrationResponseDto) in
                    DispatchQueue.main.async {
                        self.presentationMode.wrappedValue.dismiss()
                    }
                },
                validationRegisterAction: {(validationErrors: Dictionary<String, [String]>) in
                    if (validationErrors.keys.contains("Email")) {
                        DispatchQueue.main.async {
                            self.emailError = true
                        }
                    }
                },
                failureRegisterAction: {(error: ImsHttpError) in
                    print(error.status, error.errorMessage, error.errorType)
                })
        }
    }
    
    private func ValidateNickname() {
        if(self.nickname.isEmpty) {
            self.nicknameError = true
        } else {
            self.nicknameError = false
        }
    }
    
    private func ValidateEmail(){
        if(self.email.isEmpty) {
            self.emailError = true
        } else {
            let emailFormat = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}" // short format
            let emailPredicate = NSPredicate(format:"SELF MATCHES %@", emailFormat)
            self.emailError = !emailPredicate.evaluate(with: self.email)
        }
    }
    
    private func ValidatePassword() {
        if(self.password.isEmpty) {
            self.passwordError = true
        } else {
            self.passwordError = false
        }
    }
    
    private func ValidateRepeatPassword() {
        if(self.repeatPassword.isEmpty) {
            self.repeatPasswordError = true
        } else {
            if(self.password != self.repeatPassword){
                self.repeatPasswordError = true
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
