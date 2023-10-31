//
//  ImsHttpMessage.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct ImsHttpMessage<T: Codable>: Codable {
    public var status: Int
    public var serverResponseTime: Date
    public var serverRequestTime: Date
    public var data: T
}
