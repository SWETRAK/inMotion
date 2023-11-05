//
//  AddPasswordDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct AddPasswordDto: Codable {
    public var newPassword: String
    public var repeatPassword: String
}
