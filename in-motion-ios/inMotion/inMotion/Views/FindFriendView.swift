//
//  FindFriendView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct FindFriendView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @State var nickname: String = "";
    var body: some View {
        VStack {
            VStack {
                TextField(text: $nickname){
                    Text("Find a friend...")
                }
                Divider() .overlay(Color.blue)
            }.padding(.horizontal)
            ScrollView {
                FindFriendRowView()
                FindFriendRowView()
                FindFriendRowView()
            }
        }
    }
}

struct FindFriendView_Previews: PreviewProvider {
    static var previews: some View {
        FindFriendView()
    }
}
