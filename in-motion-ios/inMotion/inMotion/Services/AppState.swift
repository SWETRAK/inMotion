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

    @Published var internetConnection: Bool = false
    @Published var internetConnectionViaCellular: Bool = false

    init(logged: Bool, user: UserInfoDto?, token: String?) {
        self.logged = logged
        self.user = user
    }

    let httpBaseUrl = "http://localhost"

    enum HTTPMethods: String {
        case POST = "POST"
        case GET = "GET"
        case PUT = "PUT"
        case PATCH = "PATCH"
        case DELETE = "DELETE"
    }
}

