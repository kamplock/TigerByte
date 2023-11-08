using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Driver;


namespace TigerByte_API.Models;

public class Users
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    //public string username { get; set; } = null!;

    
    [BsonElement("items")]
    [JsonPropertyName("items")]
    public List<string> usersList { get; set; } = null!;
    
}