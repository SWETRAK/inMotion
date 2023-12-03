//
//  ChangePassword.swift
//  inMotion
//
//  Created by Kamil Pietrak on 04/11/2023.
//

import SwiftUI

struct ChangePassword: View {
    
    @EnvironmentObject private var appState: AppState
    @Environment(\.dismiss) var dismiss
    
    @State private var password: String = ""
    @State private var newPassword: String = ""
    @State private var repeatPassword: String = ""
    
    @State private var showAlert: Bool = false
    @State private var passwordError: Bool = false
    @State private var newPasswordError: Bool = false
    @State private var repeatPasswordError: Bool = false
    
    var body: some View {
        Form {
            Section{
                
                SecureField("Old password", text: $password)
                
                SecureField("New password", text: $newPassword)

                SecureField("Repeat new password", text: $repeatPassword)
                
                Button("Add password"){
                    self.ValidatePassword()
                    self.ValidateNewPassword()
                    self.ValidateRepeatPassword()
                    
                    if(!self.repeatPasswordError && !self.passwordError && !self.newPasswordError) {
                        
                        self.appState.updateUserPasswordHttpRequest(
                            requestData: UpdatePasswordDto(oldPassword: password, newPassword: newPassword, repeatPassword: repeatPassword),
                            successPasswordChangeAction: {(data: Bool) in
                                dismiss()
                            },
                            failurePasswordChangeAction: {(error: ImsHttpError) in
                                if (error.status == 404) {
                                    self.passwordError = true
                                    self.showAlert = true
                                }
                            })
                    }
                }.alert(isPresented: $showAlert) {
                    if (self.passwordError) {
                        return Alert(
                            title: Text("Incorrect password"),
                            dismissButton: .default(Text("Ok")) {
                                self.passwordError = false
                            }
                        )
                    } else if (self.newPasswordError) {
                        return Alert(
                            title: Text("Incorrect new password"),
                            dismissButton: .default(Text("Ok")) {
                                self.newPasswordError = false
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
    }
    
    private func ValidatePassword() {
        if(self.newPassword.isEmpty) {
            self.passwordError = true
            self.showAlert = true
        } else {
            self.passwordError = false
        }
    }
    
    private func ValidateNewPassword() {
        if(self.newPassword.isEmpty) {
            self.newPasswordError = true
            self.showAlert = true
        } else {
            self.newPasswordError = false
        }
    }

    private func ValidateRepeatPassword() {
        if(self.repeatPassword.isEmpty) {
            self.repeatPasswordError = true
            self.showAlert = true
        } else {
            if(self.newPassword != self.repeatPassword){
                self.repeatPasswordError = true
                self.showAlert = true
            } else {
                self.repeatPasswordError = false
            }
        }
    }
}

struct ChangePassword_Previews: PreviewProvider {
    static var previews: some View {
        ChangePassword()
    }
}
