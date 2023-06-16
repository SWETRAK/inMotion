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
                
                Button ("Load Data") {
                    self.LoadData()
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
    
    private func LoadData() {
        
        let user1 = User(context: viewContext)
        user1.id = UUID()
        user1.nickname = "swetrak"
        user1.profile_photo = "google-logo"
        user1.email = "kamilpietrak123@gmail.com"
        user1.password = "Ssmr1234"
        
        let user2 = User(context: viewContext)
        user2.id = UUID()
        user2.nickname = "userDwa"
        user2.profile_photo = "facebook-logo"
        user2.email = "kamilpietrak123@icloud.com"
        user2.password = "Password2"
        
        let post1 = Post(context: viewContext)
        post1.id = UUID()
        post1.author = user1
        post1.localization_longitude = 21.00558
        post1.localization_latitude = 52.23105
        post1.localization_name = "Warsaw"
        post1.video_link = "facebook-logo"
        
        let post2 = Post(context: viewContext)
        post2.id = UUID()
        post2.author = user1
        post2.localization_longitude = 21.00558
        post2.localization_latitude = 52.23105
        post2.localization_name = "Warsaw"
        post2.video_link = "facebook-logo"
        
        let comment1 = Comment(context: viewContext)
        comment1.id = UUID()
        comment1.post = post1
        comment1.time = Date.now
        comment1.author = user1
        comment1.comment = "Test comment 1"
        
        let comment2 = Comment(context: viewContext)
        comment2.id = UUID()
        comment2.post = post1
        comment2.time = Date.now
        comment2.author = user2
        comment2.comment = "Test comment 2"
        
        if (viewContext.hasChanges) {
            do {
                try viewContext.save()
            } catch {
                let nserror = error as NSError
                fatalError("Unresolved error \(nserror), \(nserror.userInfo)")
            }
        }
    }
}

struct LoginView_Previews: PreviewProvider {
    static var previews: some View {
        LoginView()
    }
}
