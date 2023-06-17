//
//  FindFriendView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import CoreData

struct FindFriendView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var appState: AppState
    @State private var nickname: String = "";
    
    @State private var persons: [User] = []
    @State private var refresh: Bool = false
    
    var body: some View {
        VStack {
            TextField("Find a friend...", text: $nickname)
                .textFieldStyle(RoundedBorderTextFieldStyle()).padding(.horizontal)
                .textInputAutocapitalization(.never)
                .onChange(of: nickname) { newValue in
                    FindUsers()
                }


            List(persons, id: \.id){
                person in
                PersonRowView()
                    .environmentObject(person)
                    .swipeActions {
                        Button {
                        } label: {
                            Image(systemName: "person.fill.xmark.rtl")
                        }
                        .tint(.blue)
                    }
            }
        }
    }
    
    private func FindUsers() {
        if let safeUser = appState.user {
            let request: NSFetchRequest<User> = User.fetchRequest()
            let predictate = NSPredicate(format: "nickname LIKE %@ && id != %@", "*\(nickname)*", safeUser.id! as CVarArg)
            request.predicate = predictate
            do {
                self.persons = try viewContext.fetch(request)
            } catch {
                print("Error fetching data from context \(error)")
            }
        }
    }
}

struct FindFriendView_Previews: PreviewProvider {
    static var previews: some View {
        FindFriendView()
    }
}
