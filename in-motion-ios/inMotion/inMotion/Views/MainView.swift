//
//  MainView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

struct MainView: View {
    @Environment(\.managedObjectContext) private var viewContext
    
    
    var body: some View {
        VStack{
            ScrollView() {
                MainWallPost()
                MainWallPost()
                MainWallPost()
            }
        }.toolbar {
            ToolbarItem(placement: .primaryAction) {
                NavigationLink {
                    FriendsView()
                } label: {
                    Image(systemName: "person.2.fill")
                }.buttonStyle(.plain)
            }
        }
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
