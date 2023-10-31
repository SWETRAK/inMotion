//
//  EditPostRequestDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct EditPostRequestDto: Codable {
    public var title: String // Length: 256
    public var description: String // Length: 2048
}
