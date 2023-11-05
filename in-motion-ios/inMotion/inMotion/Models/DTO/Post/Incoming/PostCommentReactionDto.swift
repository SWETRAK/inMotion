//
//  PostCommentReactionDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct PostCommentReactionDto: Codable {
    public var id: UUID
    public var author: PostAuthorDto
    public var emoji: String
    public var postCommmentId: UUID
    public var createdAt: Date
}
