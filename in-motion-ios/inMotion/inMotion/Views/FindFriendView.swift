////
////  FindFriendView.swift
////  inMotion
////
////  Created by Kamil Pietrak on 14/06/2023.
////
//
//import SwiftUI
//import CoreData
//
//struct FindFriendView: View {
//    @Environment(\.managedObjectContext) private var viewContext
//    @EnvironmentObject private var appState: AppState
//    @State private var nickname: String = "";
//
//    @State private var persons: [User] = []
//    @State private var refresh: Bool = false
//
//    var body: some View {
//        VStack {
//            TextField("Find a friend...", text: $nickname)
//                .textFieldStyle(RoundedBorderTextFieldStyle()).padding(.horizontal)
//                .textInputAutocapitalization(.never)
//                .onChange(of: nickname) { newValue in
//                    FindUsers()
//                }
//
//            List(persons, id: \.id){
//                person in
//                PersonRowView()
//                    .environmentObject(person)
//                    .swipeActions {
//                        if(GetFriendshipStatus(person: person)){
//                            HStack {
//                                Button {
//                                    SendInvitation(person: person)
//                                } label: {
//                                    Image(systemName: "plus")
//                                }
//                                .tint(.blue)
//                            }
//                        }
//                    }
//            }
//        }
//    }
//
//    private func FindUsers() {
//        if let safeUser = appState.user {
//            let request: NSFetchRequest<User> = User.fetchRequest()
//            let predictate = NSPredicate(format: "nickname LIKE %@ && id != %@", "*\(nickname)*", safeUser.id! as CVarArg)
//            request.predicate = predictate
//            do {
//                self.persons = try viewContext.fetch(request)
//            } catch {
//                print("Error fetching data from context \(error)")
//            }
//        }
//    }
//
//
//    private func SendInvitation(person: User) {
//        if let safeUser = appState.user {
//            let friendship = Friendship(context: viewContext)
//            friendship.id = UUID()
//            friendship.userOne = safeUser
//            friendship.userTwo = person
//            friendship.status = FriendshipStatusEnum.Requested.rawValue
//            if(viewContext.hasChanges) {
//                do {
//                    try viewContext.save()
//                } catch {
//                    print("Error fetching data from context \(error)")
//                }
//            }
//            FindUsers()
//        }
//    }
//
//    private func GetFriendshipStatus(person: User) -> Bool {
//        if let safeUser = appState.user {
//            let request: NSFetchRequest<Friendship> = Friendship.fetchRequest()
//            let predictate = NSPredicate(format: "(userOne.id == %@ && userTwo.id == %@) || ( userOne.id == %@ && userTwo.id == %@ )", person.id! as CVarArg, safeUser.id! as CVarArg, safeUser.id! as CVarArg, person.id! as CVarArg)
//            request.predicate = predictate
//            do {
//                let friendship = try viewContext.fetch(request)
//
//                if(friendship.count == 0)
//                {
//                    return true
//                }
//            } catch {
//                print("Error fetching data from context \(error)")
//            }
//        }
//        return false
//    }
//}
//
//struct FindFriendView_Previews: PreviewProvider {
//    static var previews: some View {
//        FindFriendView()
//    }
//}
