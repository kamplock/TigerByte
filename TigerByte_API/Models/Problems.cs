using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TigerByte_API.Models;

public class Problems
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string ProblemName { get; set; } = null!;
    public string Problem { get; set; } = null!;
    public string Solution { get; set; } = null!;
    public string Type { get; set; } = null!;

    //public string username { get; set; } = null!;

    
    [BsonElement("problemsList")]
    [JsonPropertyName("problemsList")]
    public List<string> problemsList { get; set; } = null!;
    
    
}