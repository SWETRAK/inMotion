//
//  FullUserInfoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct FullUserInfoDto: Codable {
    public var id: UUID
    public var nickname: String
    public var bio: String
    
    public var userProfileVideo: UserProfileVideoDto
}
