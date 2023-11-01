//
//  ContentView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI
import CoreData

struct ContentView: View {

    @StateObject var appState: AppState = AppState(logged: false, user: nil, token: nil)

    // TODO: rewrite this for new approach
    // This should check online if user is logged in
    // If not login page should be displayed
    // If there is no connection to internet there should be displayed no internet connection view
    // If loged in main page should be displayed
    var body: some View {
        NavigationView {
            LoginView().environmentObject(appState);
        }.onAppear{

        }

//        NavigationView{
//            if(appState.logged) {
//                MainView().navigationTitle("inMotion")
//                    .environmentObject(appState)
//            } else {
//                LoginView()
//                    .environmentObject(appState)
//            }
//        }.onAppear{
//            // Initiate data for testing application
//            if(InitData.CheckIfEmpty(context: viewContext)) {
//                InitData.InitData(context: viewContext)
//            }
//        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
