//
//  PostUploadInfoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 18/11/2023.
//

import Foundation

struct PostUploadInfoDto: Codable {
    public var postId: UUID
    public var getVideosPath: String
}
