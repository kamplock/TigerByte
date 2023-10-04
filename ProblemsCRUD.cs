using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TigerByte;

public class ProblemsCRUD
{

    private IMongoCollection<Problems> collection;

    public ProblemsCRUD(IMongoDatabase database)
    {
        // Initialize the collection with the appropriate collection from MongoDB
        collection = database.GetCollection<Problems>("problems");
    }



    //insert one - create
    public Problems InsertProblem(Problems theproblem)
    {
        try
        {
            collection.InsertOne(theproblem);
            return theproblem;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong trying to insert the problem. Message: {e.Message}");
            Console.WriteLine(e);
            return null;
        }
    }

    //read all users
    public void getAllProblems()
    {
        var allDocs = collection.Find(Builders<Problems>.Filter.Empty).ToList();

        foreach (Problems problems in allDocs)
        {
            Console.WriteLine($"Name: {problems.ProblemName} \nProblem: {problems.Problem} \nSolution: {problems.Solution} \nType: {problems.Type}");
            Console.WriteLine();
        }
    }

    //read one user
    public string getOneProblemByName(string name)
    {
        var findFilter = Builders<Problems>.Filter.Eq("ProblemName", name);

        var findResult = collection.Find(findFilter).FirstOrDefault();

        if (findResult == null)
        {
            return "I didn't find a problem with the name that matched.";
        }
        return findResult.ToString();
    }

    public string getOneProblemByType(string type)
    {
        var findFilter = Builders<Problems>.Filter.Eq("Type", type);

        var findResult = collection.Find(findFilter).FirstOrDefault();

        if (findResult == null)
        {
            return "I didn't find a problem with the type that matched.";
        }
        return findResult.ToString();
    }

    //update 
    public string updateDoc(Problems prob, string solOrType, string updated)
    {
        var findFilter = Builders<Problems>.Filter.Eq(solOrType, updated);

        if (solOrType.Equals("Soltution"))
        {
            var updateFilter = Builders<Problems>.Update.Set(t => t.Solution, updated);

            // The following FindOneAndUpdateOptions specify that we want the *updated* document
            // to be returned to us. By default, we get the document as it was *before*
            // the update.

            var options = new FindOneAndUpdateOptions<Problems, Problems>()
            {
                ReturnDocument = ReturnDocument.After
            };

            // The updatedDocument object is a Users object that reflects the
            // changes we just made.
            var updatedDocument = collection.FindOneAndUpdate(findFilter,
                updateFilter, options);

            return updatedDocument.ToString();
        }
        else if (solOrType.Equals("Type"))
        {
            var updateFilter = Builders<Problems>.Update.Set(t => t.Type, updated);

            // The following FindOneAndUpdateOptions specify that we want the *updated* document
            // to be returned to us. By default, we get the document as it was *before*
            // the update.

            var options = new FindOneAndUpdateOptions<Problems, Problems>()
            {
                ReturnDocument = ReturnDocument.After
            };

            // The updatedDocument object is a Recipe object that reflects the
            // changes we just made.
            var updatedDocument = collection.FindOneAndUpdate(findFilter,
                updateFilter, options);

            return updatedDocument.ToString();
        }
        else
        {
            return "We couldn't complete your request.";
        }
    }
    //delete 1
    public void deleteOneProblem(string probName)
    {
        var deleteResult = collection.DeleteOne(Builders<Problems>.Filter.Eq("ProblemName", probName));
        Console.WriteLine($"I deleted the problem called {deleteResult.DeletedCount}.");
    }

    //delete all
    public void deleteAll()
    {
        var filter = Builders<Problems>.Filter.Empty;
        var result = collection.DeleteMany(filter);
        Console.WriteLine($"I deleted {result.DeletedCount} problems.");
    }



}
