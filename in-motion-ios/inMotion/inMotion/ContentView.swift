//
//  ContentView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI
import Network

struct ContentView: View {
    
    @StateObject var appState: AppState = AppState()
    
    let monitor = NWPathMonitor()
    var body: some View {
        NavigationView {
            if(appState.initAppReady) {
                if(appState.internetConnection) {
                    if (appState.logged) {
                        MainView().navigationTitle("inMotion")
                            .environmentObject(appState)
                    } else {
                        LoginView().environmentObject(appState);
                    }
                    
                } else {
                    Text("No internet connection")
                }
            } else {
                Text("INIT PAGE")
            }
        }.onAppear{
            monitor.start(queue: DispatchQueue.main)
            monitor.pathUpdateHandler = { path in
                print(path.status)
                if path.status == .satisfied {
                    appState.internetConnection = true
                    appState.internetConnectionViaCellular = path.isExpensive
                    if (appState.token != nil) {
                        appState.getLoggedInUserHttpRequest(
                            successGetUserAction: {(data: UserInfoDto) in
                                DispatchQueue.main.async {
                                    appState.logged = true
                                    appState.initAppReady = true
                                    
                                    appState.getUserByIdHttpRequest(
                                        userId: appState.user!.id,
                                        successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                            DispatchQueue.main.async {
                                                self.appState.fullUserInfo = fullUserInfo
                                            }
                                        },
                                        failureGetUserAction: { (error: ImsHttpError) in})
                                }
                            },
                            failureGetUserAction: {(error: ImsHttpError) in
                                appState.logged = false
                                appState.initAppReady = true
                            })
                    } else {
                        appState.initAppReady = true
                    }
                } else {
                    appState.internetConnection = false
                    appState.internetConnectionViaCellular = false
                    appState.initAppReady = true
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
