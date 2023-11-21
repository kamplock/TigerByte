using System.Collections.Generic;

namespace TigerByte_Web_Copy.Models;

public class MongoDBSettings
{

    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public List<string> CollectionName { get; set; } = null!;

}