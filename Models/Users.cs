using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Driver;


namespace TigerByte_Web_Copy.Models;

public class Users
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    //public string username { get; set; } = null!;

    /*

    [BsonElement("usersList")]
    [JsonPropertyName("usersList")]
    public List<string> usersList { get; set; } = null!;

    */
}