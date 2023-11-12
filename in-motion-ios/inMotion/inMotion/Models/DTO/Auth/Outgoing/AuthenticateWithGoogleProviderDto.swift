//
//  AuthenticateWithGoogleProviderDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct AuthenticateWithGoogleProviderDto : Codable {
    public var userId: String
    public var token: String
}
