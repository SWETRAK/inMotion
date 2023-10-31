//
//  FriendInfoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct FriendInfoDto : Codable {
    public var id: UUID
    public var nickname: String
    public var bio: String
    public var frontVideo: FriendProfileVideoDto 
}
