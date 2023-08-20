//
//  MainView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI
import CoreData

struct MainView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject private var appState: AppState
    
    @State private var posts: [Post] = []
    
    var body: some View {
        VStack{
            ScrollView() {
                ForEach(posts, id:\.id) { post in
                    MainWallPost().environmentObject(appState).environmentObject(post)
                }
            }
            Button("Logout") {
                self.appState.logged = false
                self.appState.user = nil
            }
        }.toolbar {
            ToolbarItem(placement: .primaryAction) {
                NavigationLink {
                    FriendsView().environmentObject(appState)
                } label: {
                    Image(systemName: "person.2.fill")
                }.buttonStyle(.plain)
            }
        }.onAppear{
            LoadPosts()
        }
    }
    
    private func LoadPosts() {
        let request: NSFetchRequest<Post> = Post.fetchRequest()
        do {
            self.posts = try viewContext.fetch(request)
        } catch {
            print("Error fetching data from context \(error)")
        }
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
