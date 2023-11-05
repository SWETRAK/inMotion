//
// Created by Kamil Pietrak on 01/11/2023.
//

import Foundation

extension Formatter {
    static var customISO8601DateFormatter: ISO8601DateFormatter = {
        let formatter = ISO8601DateFormatter()
        formatter.formatOptions = [.withInternetDateTime, .withFractionalSeconds]
        return formatter
    }()
}