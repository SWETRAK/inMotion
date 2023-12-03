//
//  AuthenticateWithFacebookProviderDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct AuthenticateWithFacebookProviderDto: Codable {
    public var token: String
    public var userId: String
}
