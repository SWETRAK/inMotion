//
//  UpdatePostCommentDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct UpdatePostCommentDto: Codable {
    public var content: String
    public var postId: UUID
}
