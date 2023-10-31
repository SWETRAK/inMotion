//
//  CreatePostCommentDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct CreatePostCommentDto: Codable {
    public var content: String
    public var postId: UUID
}
