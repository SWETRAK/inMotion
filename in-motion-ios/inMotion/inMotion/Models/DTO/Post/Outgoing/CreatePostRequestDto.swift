//
//  CreatePostRequestDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct CreatePostRequestDto: Codable {
    public var title: String
    public var description: String
    public var localization: NewPostLocalizationDto
}
