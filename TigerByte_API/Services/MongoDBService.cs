using TigerByte_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TigerByte_API.Services;
public class MongoDBService
{

    private readonly IMongoCollection<Users> _usersCollection;
    private readonly IMongoCollection<Problems> _problemsCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<Users>(mongoDBSettings.Value.CollectionName[0]);
        _problemsCollection = database.GetCollection<Problems>(mongoDBSettings.Value.CollectionName[1]);
    }

    //users crud
    public async Task<List<Users>> GetUsersAsync() {
        return await _usersCollection.Find(new BsonDocument()).ToListAsync();
    
    }

    //create user
    public async Task CreateUserAsync(Users user) {
        await _usersCollection.InsertOneAsync(user);
        return;
    
    }
    public async Task AddToUsersAsync(string email, string usersList) {
        FilterDefinition<Users> filter = Builders<Users>.Filter.Eq("Email", email);
        UpdateDefinition<Users> update = Builders<Users>.Update.AddToSet<string>("usersList", usersList);
        await _usersCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteUserAsync(string email) {
        FilterDefinition<Users> filter = Builders<Users>.Filter.Eq("Email", email);
        await _usersCollection.DeleteOneAsync(filter);
        return;

    }
    





    //problems crud
    public async Task<List<Problems>> GetProblemsAsync() {
        return await _problemsCollection.Find(new BsonDocument()).ToListAsync();
    }

    //create problem
    public async Task CreateProblemAsync(Problems problem) {
        await _problemsCollection.InsertOneAsync(problem);
        return;
    
    }

    public async Task AddToProblemsAsync(string problemName, string problem, string solution, string type, string problemsList) {
        FilterDefinition<Problems> filter = Builders<Problems>.Filter.Eq("Type", type);
        UpdateDefinition<Problems> update = Builders<Problems>.Update.AddToSet<string>("problemsList", problemsList);
        await _problemsCollection.UpdateOneAsync(filter, update);
        return;

    }
    public async Task DeleteProblemAsync(string problemName) {
        FilterDefinition<Problems> filter = Builders<Problems>.Filter.Eq("Name", problemName);
        await _problemsCollection.DeleteOneAsync(filter);
        return;

    }

}