//
//  CreatePostCommentReactionDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct CreatePostCommentReactionDto: Codable {
    public var postCommentId: UUID
    public var emoji: String
}
