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
    public string? ProblemName { get; set; }
    public string? Problem { get; set; }
    public string? Solution { get; set; }
    public string? Type { get; set; }

    //public string username { get; set; } = null!;

    
    [BsonElement("items")]
    [JsonPropertyName("items")]
    public List<string> problemsList { get; set; } = null!;
    
}