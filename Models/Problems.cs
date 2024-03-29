using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TigerByte_Web_Copy.Models;

public class Problems
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string ProblemName { get; set; } = null!;
    public string Problem { get; set; } = null!;
    public string Solution { get; set; } = null!;
    public string Type { get; set; } = null!;





}