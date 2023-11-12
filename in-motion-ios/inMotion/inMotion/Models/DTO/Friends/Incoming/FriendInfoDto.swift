//
//  FriendInfoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct FriendInfoDto : Codable {
    public var id: UUID
    public var nickname: String
    public var bio: String?
    public var frontVideo: FriendProfileVideoDto?
    
    
    func ParseFrontVideoToUserProfilevideoDto() -> UserProfileVideoDto? {
        if let safeVideo = self.frontVideo {
            return UserProfileVideoDto(
                id: safeVideo.id,
                userId: self.id,
                filename: safeVideo.filename,
                bucketName: safeVideo.bucketName,
                bucketLocation: safeVideo.bucketLocation,
                contentType: safeVideo.contentType)
        }
        return nil
    }
}
