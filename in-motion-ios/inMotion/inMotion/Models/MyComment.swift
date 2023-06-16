//
//  Comment.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import Foundation

class MyComment {
    var username: String = "";
    var comment: String = "";
    var location: String = "";
    var time: String = "";
    var avatar: String = "";
    
    init(username: String, comment: String, location: String, time: String, avatar: String) {
        self.username = username
        self.comment = comment
        self.location = location
        self.time = time
        self.avatar = avatar
    }
}
