//
//  ContentView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI
import Network

struct ContentView: View {

    @StateObject var appState: AppState = AppState(logged: false, user: nil, token: nil)

    let monitor = NWPathMonitor()
    // TODO: rewrite this for new approach
    // This should check online if user is logged in
    // If not login page should be displayed
    // If there is no connection to internet there should be displayed no internet connection view
    // If logged in main page should be displayed
    var body: some View {
        NavigationView {
            if(appState.internetConnection) {
                if(appState.logged) {
                    MainView().navigationTitle("inMotion")
                            .environmentObject(appState)
                } else {
                    LoginView().environmentObject(appState);
                }
            } else {
                //TODO: Create view if no internet
                Text("No internet connection")
            }
        }.onAppear{
            monitor.start(queue: DispatchQueue.main)
            monitor.pathUpdateHandler = { path in
                print(path.status)
                if path.status == .satisfied {
                    appState.internetConnection = true
                    appState.internetConnectionViaCellular = path.isExpensive
                } else {
                    appState.internetConnection = false
                    appState.internetConnectionViaCellular = false
                }
            }
        }.onDisappear {
            monitor.cancel()
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
