//
//  UpdateUserProfileVideoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct UpdateUserProfileVideoDto: Codable {
    public var filename: String
    public var bucketName: String
    public var bucketLocation: String
    public var contentType: String
}
