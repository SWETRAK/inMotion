//
//  RegisterUserWithEmailAndPasswordDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct RegisterUserWithEmailAndPasswordDto: Codable {
    
    public var email: String
    public var password: String
    public var repeatPassword: String
    public var nickname: String
}
