//
//  LoginView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

// TODO: Add validators to fields

import SwiftUI
import CoreData

struct LoginView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject var appState: AppState
    
    @State var email: String = ""
    @State var password: String = ""
    
    var body: some View {
        VStack {
            Spacer()
            Text("inMotion").font(.custom("Roboto", fixedSize: 50))
            
            Spacer()
            
            VStack(spacing: 20.0) {
                TextField("Email", text: $email)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .textInputAutocapitalization(.never)
    
                SecureField("Password", text: $password)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                
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
                return result[0]
            }
            return nil
        } catch {
            print("Error fetching data from context \(error)")
        }
        return nil
    }
}

struct LoginView_Previews: PreviewProvider {
    static var previews: some View {
        LoginView()
    }
}
