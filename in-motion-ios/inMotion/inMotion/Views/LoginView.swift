//
//  LoginView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI

struct LoginView: View {
    
    @State var email: String = ""
    @State var password: String = ""
    
    var body: some View {
        VStack {
            Image("logo")
            
            Spacer()
            
            VStack {
                TextField("Email", text: $email)
                
                TextField("Password", text: $password)
                
                Button("Login with email") {
                    
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
            
            Button("Register new account") {
                
            }
            
        }.padding()
    }
    
}

struct LoginView_Previews: PreviewProvider {
    static var previews: some View {
        LoginView()
    }
}
