//
//  RegisterView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import CoreData

struct RegisterView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var appState: AppState
    
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

            Spacer()
            VStack {
                if self.nicknameError {
                    Text("Nickname cant be empty")
                }

                TextField(
                    "Nickname",
                    text: $nickname,
                    onEditingChanged: { (isChanged) in
                        if(!isChanged) {
                            self.ValidateNickname()
                        }
                })
                .textFieldStyle(RoundedBorderTextFieldStyle())
            }

            VStack {
                if self.emailError {
                    Text("Incorect email address")
                }
                TextField("Email", text: $email,  onEditingChanged: { (isChanged) in
                    if(!isChanged) {
                        self.ValidateEmail()
                    }
                })
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .textInputAutocapitalization(.never)
            }

            VStack {
                if self.passwordError {
                    Text("Incorrect password")
                }
                SecureField("Password", text: $password)
                .textFieldStyle(RoundedBorderTextFieldStyle())
            }

            VStack {
                if self.repeatPasswordError {
                    Text("Incorrect repeat password")
                }
                SecureField("Repeat password", text: $repeatPassword)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
            }

            Button("REGISTER"){
                self.ValidatePassword()
                self.ValidateRepeatPassword()

                if(!self.repeatPasswordError && !self.nicknameError && !self.passwordError && !self.emailError) {
                    appState.registerUserWithEmailAndPasswordHttpRequest(registerData: RegisterUserWithEmailAndPasswordDto(email: self.email, password: self.password, repeatPassword: self.repeatPassword, nickname: self.nickname),
                            successRegisterAction: {(successData: SuccessfulRegistrationResponseDto) in
                            },
                            failureRegisterAction: {(error: ImsHttpError) in
                            })
                }
            }
            Spacer()
            Button("Login to existing account"){
                self.presentationMode.wrappedValue.dismiss()
            }
        }
        .padding()
        .navigationBarHidden(true)
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
