//
//  LoginView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI

struct LoginView: View {
    @Environment(\.managedObjectContext) private var viewContext
    
    @Binding var logged: Bool
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
    
                TextField("Password", text: $password)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                
                Button("Login with email") {
                    self.logged = true
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
            
            NavigationLink("Register new account", destination: RegisterView(logged: $logged))
            
        }
        .padding()
        .navigationBarHidden(true)
    }
    
}

struct LoginView_Previews: PreviewProvider {
    static var previews: some View {
        LoginView(logged: .constant(false))
    }
}
