//
//  GetPostResponseDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct GetPostResponseDto: Codable {
    public var id: UUID
    
    public var description: String
    public var title: String
    
    public var author: PostAuthorDto
    public var tags: [PostTagDto]
    
    public var videos: [PostVideoDto]
    public var postCommentsCount: UInt
    public var postReactionsCount: UInt
    
    public var isLikedByUser: Bool
    public var postReaction: PostReactionWithoutAuthorDto?
    
    public var createdAt: Date
}
