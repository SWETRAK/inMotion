//
//  PostAuthorDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct PostAuthorDto: Codable {
    public var id: UUID
    public var nickname: String
    public var frontVideo: String?
}
