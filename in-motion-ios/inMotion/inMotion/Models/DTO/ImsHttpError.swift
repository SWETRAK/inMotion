//
//  ImsHttpError.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct ImsHttpError: Codable {
    
    public var status: Int
    
    public var errorMessage: String
    
    public var errorType: String
}
