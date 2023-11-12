//
//  CreatePostReactionDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct CreatePostReactionDto: Codable {
    public var postId: UUID
    public var emoji: String
}
