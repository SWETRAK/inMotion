//
//  AddPasswordToAccount.swift
//  inMotion
//
//  Created by Kamil Pietrak on 04/11/2023.
//

import SwiftUI

struct AddPasswordToAccount: View {
    
    @EnvironmentObject private var appState: AppState
    @Environment(\.dismiss) var dismiss
    
    @State private var password: String = ""
    @State private var repeatPassword: String = ""
    
    @State private var showAlert: Bool = false
    @State private var passwordError: Bool = false
    @State private var repeatPasswordError: Bool = false
    
    var body: some View {
        Form {
            Section{
                
                SecureField("Password", text: $password)

                SecureField("Repeat password", text: $repeatPassword)
                
                Button("Add password"){
                    self.ValidatePassword()
                    self.ValidateRepeatPassword()
                    
                    if(!self.repeatPasswordError && !self.passwordError) {
                        appState.addPasswordHttpRequest(
                            requestData: AddPasswordDto(newPassword: self.password, repeatPassword: self.repeatPassword),
                            successAddPasswordAction: {(data: Bool) in
                                dismiss()
                            },
                            failureAddPasswordAction: {(error: ImsHttpError) in})
                    }
                }.alert(isPresented: $showAlert) {
                    if (self.passwordError) {
                        return Alert(
                            title: Text("Incorrect password"),
                            dismissButton: .default(Text("Ok")) {
                                self.passwordError = false
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

struct AddPasswordToAccount_Previews: PreviewProvider {
    static var previews: some View {
        AddPasswordToAccount()
    }
}
