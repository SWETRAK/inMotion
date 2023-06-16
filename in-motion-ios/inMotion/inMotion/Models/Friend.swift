//
//  Friend.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import Foundation

class Friend {
    var username: String = "";
    var lastseen: String = "";
    var avatar: String = "";
    
    init(nickname: String, lastseen: String, avatar: String) {
        self.username = nickname
        self.lastseen = lastseen
        self.avatar = avatar
    }
}
