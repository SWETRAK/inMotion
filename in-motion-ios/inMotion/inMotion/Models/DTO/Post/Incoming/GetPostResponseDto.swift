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
    public var localization: PostLocalizationDto
    
    public var videos: [PostVideoDto]
    public var postCommentsCount: UInt
    public var PostReactionsCount: UInt
    
    public var CreateAt: Date
}
