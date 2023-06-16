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
    @EnvironmentObject var appState: AppState
    
    @State var nickname: String = ""
    @State var email: String = ""
    @State var password: String = ""
    @State var repeatPassword: String = ""
    
    @State var emailError: Bool = false
    @State var nicknameError: Bool = false
    @State var passwordError: Bool = false
    @State var repeatPasswordError: Bool = false
    
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
                    let user: User? = self.SaveUser()
                    if let safeuser = user {
                        self.appState.user = safeuser
                        self.appState.logged = true
                        self.presentationMode.wrappedValue.dismiss()
                    } else {
                        self.emailError = true
                    }
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

    private func SaveUser() -> User? {
        if(self.GetUser()) {
            let user = User(context: viewContext)
            user.nickname = nickname
            user.email = email
            user.password = password
            user.id = UUID()
            user.profile_photo = "google-logo"
            if (viewContext.hasChanges) {
                do {
                    try viewContext.save()
                    return user
                } catch {
                    let nserror = error as NSError
                    fatalError("Unresolved error \(nserror), \(nserror.userInfo)")
                }
            }
        }
        return nil
    }
    
    private func GetUser() -> Bool {
        let request: NSFetchRequest<User> = User.fetchRequest()
        let predictate = NSPredicate(format: "email == %@", self.email)
        request.predicate = predictate
        do {
            let result = try viewContext.fetch(request)
            if(result.count == 0) {
                return true
            }
            return false
        } catch {
            print("Error fetchig data from context \(error)")
        }
        return false
    }
}

struct RegisterView_Previews: PreviewProvider {
    static var previews: some View {
        RegisterView()
    }
}
