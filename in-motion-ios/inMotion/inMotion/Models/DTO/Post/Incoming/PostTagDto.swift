//
//  PostTagDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct PostTagDto: Codable {
    public var id: UUID
    public var name: String
    public var createAt: Date
}
