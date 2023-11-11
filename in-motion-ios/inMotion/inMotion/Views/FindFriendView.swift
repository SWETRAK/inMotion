//
//  FindFriendView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import CoreData

struct FindFriendView: View {
    @EnvironmentObject private var appState: AppState
    @State private var nickname: String = "";

    @State private var persons: [FullUserInfoDto] = []
    @State private var refresh: Bool = false

    var body: some View {
        VStack {
            TextField("Find a friend...", text: $nickname)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(.horizontal)
                .textInputAutocapitalization(.never)
                .onChange(of: nickname) { newValue in
                    FindUsers()
                }

            List(persons, id: \.id){
                person in
                PersonRowView(person: person)
                    .swipeActions {
                        if(GetFriendshipStatus(person: person) == FriendshipStatusEnum.Unknown){
                            HStack {
                                Button {
                                    SendInvitation(person: person)
                                } label: {
                                    Image(systemName: "plus")
                                }
                                .tint(.blue)
                            }
                        }
                    }
            }
        }
    }

    private func FindUsers() {
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
    }


    private func SendInvitation(person: FullUserInfoDto) {
        appState.createFriendshipHttpRequest(
            otherUserId: person.id,
            successCreateFriendshipAction: {(data: InvitationFriendshipDto) in },
            failureCreateFriendshipAction: {(error: ImsHttpError) in })
    }

    private func GetFriendshipStatus(person: FullUserInfoDto) -> FriendshipStatusEnum {
        if (self.appState.invitedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            return FriendshipStatusEnum.Invited
        }
        
        if (self.appState.acceptedFriendships.first { x in return x.externalUserId == person.id } != nil)
        {
            return FriendshipStatusEnum.Accepted
        }

        if (self.appState.requestedFriendships.first { x in return x.externalUserId == person.id } != nil) {
            return FriendshipStatusEnum.Requested
        }
        
        return FriendshipStatusEnum.Unknown
    }
}

struct FindFriendView_Previews: PreviewProvider {
    static var previews: some View {
        FindFriendView()
    }
}
