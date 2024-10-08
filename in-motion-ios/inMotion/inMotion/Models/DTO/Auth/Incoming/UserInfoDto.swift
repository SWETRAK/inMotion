//
//  UserInfoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct UserInfoDto: Codable{
    public var id: UUID
    public var email: String
    public var nickname: String
    public var token: String
    public var providers: [String]
}
