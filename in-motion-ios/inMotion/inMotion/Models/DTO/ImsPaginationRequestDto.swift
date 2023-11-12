//
//  ImsPaginationRequestDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct ImsPaginationRequestDto: Codable {
    public var pageNumber: Int
    public var pageSize: Int
}
