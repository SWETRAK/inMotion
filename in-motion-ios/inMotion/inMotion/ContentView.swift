//
//  ContentView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI
import Network

struct ContentView: View {
    
    @StateObject private var appState: AppState = AppState()
    
    private let monitor = NWPathMonitor()
    
    var body: some View {
        NavigationView {
            if(self.appState.initAppReady) {
                if(self.appState.internetConnection) {
                    if (self.appState.logged) {
                        MainView().navigationTitle("inMotion")
                            .environmentObject(self.appState)
                    } else {
                        LoginView().environmentObject(self.appState);
                    }
                    
                } else {
                    Text("No internet connection")
                }
            } else {
                Text("INIT PAGE")
            }
        }.onAppear{
            self.monitor.start(queue: DispatchQueue.main)
            self.monitor.pathUpdateHandler = { path in
                if path.status == .satisfied {
                    self.appState.internetConnection = true
                    self.appState.internetConnectionViaCellular = path.isExpensive
                    if (self.appState.token != nil) {
                        self.appState.getLoggedInUserHttpRequest(
                            successGetUserAction: {(data: UserInfoDto) in
                                DispatchQueue.main.async {
                                    self.appState.logged = true
                                    self.appState.initAppReady = true
                                    
                                    self.appState.getUserByIdHttpRequest(
                                        userId: self.appState.user!.id,
                                        successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                            DispatchQueue.main.async {
                                                self.appState.fullUserInfo = fullUserInfo
                                            }
                                        },
                                        failureGetUserAction: { (error: ImsHttpError) in})
                                }
                            },
                            failureGetUserAction: {(error: ImsHttpError) in
                                DispatchQueue.main.async {
                                    self.appState.logged = false
                                    self.appState.initAppReady = true
                                }
                            })
                    } else {
                        DispatchQueue.main.async {
                            self.appState.initAppReady = true
                        }
                    }
                } else {
                    DispatchQueue.main.async {
                        self.appState.internetConnection = false
                        self.appState.internetConnectionViaCellular = false
                        self.appState.initAppReady = true
                    }
                }
            }
        }.onDisappear {
            self.monitor.cancel()
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
