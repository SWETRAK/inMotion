//
//  PostCommentDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct PostCommentDto: Codable {
    public var id: UUID
    public var author: PostAuthorDto
    public var postId: UUID
    public var content: String
    public var postCommentReactionCount: Int
    public var createdAt: Date
}
