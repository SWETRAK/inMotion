//
//  VideoMetaDataDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct VideoMetaDataDto: Codable {
    public var bucketName: String
    public var bucketLocalization: String
    public var filename: String
    public var contentType: String
    public var type: String // TODO: Change type of this to enum
}
