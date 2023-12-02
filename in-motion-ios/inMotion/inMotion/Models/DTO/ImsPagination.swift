//
//  ImsPagination.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct ImsPagination<T: Codable> : Codable {
    
    public var pageNumber: Int
    public var pageSize: Int
    public var totalCount: Int
    public var data: T
}
