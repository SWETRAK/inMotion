//
// Created by Kamil Pietrak on 01/11/2023.
//

import Foundation

extension JSONDecoder.DateDecodingStrategy {
    static var iso8601WithFractionalSeconds = custom { decoder in
        let dateStr = try decoder.singleValueContainer().decode(String.self)
        let customIsoFormatter = Formatter.customISO8601DateFormatter
        if let date = customIsoFormatter.date(from: dateStr) {
            return date
        }
        throw DecodingError.dataCorrupted(
                DecodingError.Context(codingPath: decoder.codingPath,
                        debugDescription: "Invalid date"))
    }
}
