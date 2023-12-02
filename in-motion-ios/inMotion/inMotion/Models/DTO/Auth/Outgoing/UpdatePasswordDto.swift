//
//  UpdatePasswordDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct UpdatePasswordDto: Codable {
    
    public var oldPassword: String
    public var newPassword: String
    public var repeatPassword: String 
}
