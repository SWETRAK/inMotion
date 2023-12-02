//
//  POstReactionWithoutAuthorDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 02/12/2023.
//

import Foundation

struct PostReactionWithoutAuthorDto: Codable {
    public var id: UUID

    public var authorId: UUID
    public var emoji: String
    public var createdAt: Date
    
    
}
