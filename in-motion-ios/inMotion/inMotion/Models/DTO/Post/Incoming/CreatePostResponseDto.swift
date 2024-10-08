//
//  CreatePostResponseDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct CreatePostResponseDto : Codable {
    public var id: UUID
    public var title: String
    public var description: String
    
    public var tags: [PostTagDto]
}
