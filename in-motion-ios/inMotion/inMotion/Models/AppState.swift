//
//  AppState.swift
//  inMotion
//
//  Created by Kamil Pietrak on 16/06/2023.
//

import Foundation

class AppState: ObservableObject {
    var logged: Bool = false
    var user: User? = nil

    init(logged: Bool, user: User?) {
        self.logged = logged
        self.user = user
    }
}
