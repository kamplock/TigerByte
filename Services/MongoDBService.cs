using TigerByte_Web_Copy.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using static MongoDB.Driver.WriteConcern;

namespace TigerByte_Web_Copy.Services;
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
    public async Task<List<Users>> GetUsersAsync()
    {
        return await _usersCollection.Find(new BsonDocument()).ToListAsync();

    }

    //create user
    public async Task CreateUserAsync(Users user)
    {
        await _usersCollection.InsertOneAsync(user);
        return;

    }

    public async Task UpdateUserAsync(string id, string newEmail, string newName)
    {
        var filter = Builders<Users>.Filter.Eq(user => user.Id, id);

        var updateEmail = Builders<Users>.Update.Set(user => user.Email, newEmail);
        await _usersCollection.UpdateOneAsync(filter, updateEmail);

        var updateName = Builders<Users>.Update.Set(user => user.Name, newName);
        await _usersCollection.UpdateOneAsync(filter, updateName);
        return;
    }



    public async Task DeleteUserAsync(string id)
    {
        FilterDefinition<Users> filter = Builders<Users>.Filter.Eq("Id", id);
        await _usersCollection.DeleteOneAsync(filter);
        return;

    }






    //problems crud
    public async Task<List<Problems>> GetProblemsAsync()
    {
        return await _problemsCollection.Find(new BsonDocument()).ToListAsync();
    }

    //create problem
    public async Task CreateProblemAsync(Problems problem)
    {
        await _problemsCollection.InsertOneAsync(problem);
        return;

    }

    public async Task UpdateProblemsAsync(string id, string newproblemName, string newproblem, string newsolution, string newtype)
    {
        var filter = Builders<Problems>.Filter.Eq(problem => problem.Id, id);

        var updateProbName = Builders<Problems>.Update.Set(problem => problem.ProblemName, newproblemName);
        await _problemsCollection.UpdateOneAsync(filter, updateProbName);

        var updateProblem = Builders<Problems>.Update.Set(problem => problem.Problem, newproblem);
        await _problemsCollection.UpdateOneAsync(filter, updateProblem);

        var updateSolution = Builders<Problems>.Update.Set(problem => problem.Solution, newsolution);
        await _problemsCollection.UpdateOneAsync(filter, updateProblem);

        var updateType = Builders<Problems>.Update.Set(problem => problem.Type, newtype);
        await _problemsCollection.UpdateOneAsync(filter, updateType);



        return;

    }
    public async Task DeleteProblemAsync(string id)
    {
        FilterDefinition<Problems> filter = Builders<Problems>.Filter.Eq("Id", id);
        await _problemsCollection.DeleteOneAsync(filter);
        return;

    }

}