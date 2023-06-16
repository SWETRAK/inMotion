//
//  RegisterView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct RegisterView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @Binding var logged: Bool
    
    @State var nickname: String = "";
    @State var email: String = "";
    @State var password: String = "";
    @State var repeatPassword: String = "";
    
    @Environment(\.presentationMode) var presentationMode: Binding<PresentationMode>
    var body: some View {
        VStack(spacing: 20.0) {
                
            Spacer()
            
            Text("inMotion").font(.custom("Roboto", fixedSize: 50))
            
            Spacer()
            
            TextField("Nickname", text: $nickname)
                .textFieldStyle(RoundedBorderTextFieldStyle())
            
            TextField("Email", text: $email)
                .textFieldStyle(RoundedBorderTextFieldStyle())
            
            TextField("Password", text: $password)
                .textFieldStyle(RoundedBorderTextFieldStyle())
            
            TextField("Repeat password", text: $repeatPassword)
                .textFieldStyle(RoundedBorderTextFieldStyle())

            Button("REGISTER"){
                self.logged = true
            }
            Spacer()
            Button("Login to existing account"){
                self.presentationMode.wrappedValue.dismiss()
            }
        }
        .padding()
        .navigationBarHidden(true)
    }
}

struct RegisterView_Previews: PreviewProvider {
    static var previews: some View {
        RegisterView(logged: .constant(false))
    }
}
