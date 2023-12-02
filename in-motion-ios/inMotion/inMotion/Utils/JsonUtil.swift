//
// Created by Kamil Pietrak on 01/11/2023.
//

import Foundation

class JsonUtil {

    public static func decodeJsonData<T: Codable>(data: Data) -> T? {
        do {
            let jsonDecode = JSONDecoder()
            jsonDecode.dateDecodingStrategy = .iso8601WithFractionalSeconds
            return try jsonDecode.decode(T.self, from: data)
        } catch {
            print("Unexpected error: \(error).")
            return nil
        }
    }

    public static func encodeJsonStringFromObject(_ object: Codable) -> Data? {
        do {
            let jsonEncoder = JSONEncoder()
            let jsonData = try jsonEncoder.encode(object)
            return String(data: jsonData, encoding: .utf8)?.data(using: .utf8)
        } catch {
            print("Unexpected error: \(error).")
            return nil
        }
    }
}
