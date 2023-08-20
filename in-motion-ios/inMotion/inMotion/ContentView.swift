//
//  ContentView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI
import CoreData

struct ContentView: View {
    @Environment(\.managedObjectContext) private var viewContext 
    @StateObject var appState: AppState = AppState(logged: false, user: nil)
    
    var body: some View {
        NavigationView{
            if(appState.logged) {
                MainView().navigationTitle("inMotion")
                    .environmentObject(appState)
            } else {
                LoginView()
                    .environmentObject(appState)
            }
        }.onAppear{
            // Initiate data for testing application
            if(InitData.CheckIfEmpty(context: viewContext)) {
                InitData.InitData(context: viewContext)
            }
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView().environment(\.managedObjectContext, PersistenceController.preview.container.viewContext)
    }
}
