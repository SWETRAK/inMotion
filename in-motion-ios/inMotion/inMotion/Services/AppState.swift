//
//  AppState.swift
//  inMotion
//
//  Created by Kamil Pietrak on 16/06/2023.
//

import Foundation

class AppState: ObservableObject {
    @Published var logged: Bool = false
    @Published var token: String? = nil
    @Published var user: UserInfoDto? = nil
    
    @Published var initAppReady: Bool = false

    @Published var internetConnection: Bool = false
    @Published var internetConnectionViaCellular: Bool = false

    let userDefaults: UserDefaults
    
    init() {
        self.userDefaults = .standard
        
        if let safeToken = self.userDefaults.string(forKey: "token") {
            self.token = safeToken
        }
    }

    let httpBaseUrl = "http://localhost"

    enum HTTPMethods: String {
        case POST = "POST"
        case GET = "GET"
        case PUT = "PUT"
        case PATCH = "PATCH"
        case DELETE = "DELETE"
    }
    
    public func logOut() {
        self.logged = false
        self.user = nil
        self.token = nil
        self.userDefaults.removeObject(forKey: "token")
    }
}

