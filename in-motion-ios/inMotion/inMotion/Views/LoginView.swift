//
//  LoginView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI
import CoreData

struct LoginView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var appState: AppState
    
    @State private var email: String = ""
    @State private var password: String = ""
    @State private var emailError: Bool = false
    @State private var passwordError: Bool = false
    @State private var loginError: Bool = false
    
    var body: some View {
        VStack {
            Spacer()
            Text("inMotion").font(.custom("Roboto", fixedSize: 50))
            
            Spacer()
            
            VStack(spacing: 20.0) {
                if self.loginError {
                    Text("Incorect login data")
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
                
                Button("Login with email") {
                    let user = LoginUser()
                    if let safeUser = user {
                        self.appState.logged = true
                        self.appState.user = safeUser
                    }
                }
                
                Divider()
                
                HStack{
                    Spacer()
                    Button {
                        
                    } label: {
                        Image("google-logo")
                            .resizable()
                            .frame(width: 50, height: 50)
                    }

                    Spacer()
                    
                    Button {
                        
                    } label: {
                        Image("facebook-logo")
                            .resizable()
                            .frame(width: 50, height: 50)
                    }
                    
                    Spacer()
                }
            }
            
            Spacer()
            
            NavigationLink("Register new account", destination: RegisterView().environmentObject(appState))
        }
        .padding()
        .navigationBarHidden(true)
    }
    
    private func LoginUser() -> User? {
        let request: NSFetchRequest<User> = User.fetchRequest()
        let predictate = NSPredicate(format: "email == %@ AND password == %@", self.email, self.password)
        request.predicate = predictate
        do {
            let result = try viewContext.fetch(request)
            if(result.count == 1) {
                self.loginError = false
                return result[0]
            }
        } catch {
            print("Error fetching data from context \(error)")
        }
        self.loginError = true
        return nil
    }
    
    private func ValidateEmail(){
        if(self.email.isEmpty) {
            self.emailError = true
        } else {
            let emailFormat = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}"
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
}

struct LoginView_Previews: PreviewProvider {
    static var previews: some View {
        LoginView()
    }
}
