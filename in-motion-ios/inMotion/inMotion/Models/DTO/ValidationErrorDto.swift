import Foundation

struct ValidationErrorDto: Codable {

    public var title: String
    public var status: Int
    public var errors: Dictionary<String, [String]>
}
