using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TigerByte;

public class UsersCRUD
{
    
    private IMongoCollection<Users> collection;

    public UsersCRUD(IMongoDatabase database)
    {
        // Initialize the collection with the appropriate collection from MongoDB
        collection = database.GetCollection<Users>("users");
    }



    //insert one - create
    public Users InsertUser(Users collection, Users theuser)
    {
        try
        {
            collection.InsertOne(theuser);
            return theuser;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong trying to insert the user. Message: {e.Message}");
            Console.WriteLine(e);
            return null;
        }
    }

    //read all users
    public void getAllUsers()
    {
        var allDocs = collection.Find(Builders<Users>.Filter.Empty).ToList();

        foreach (Users users in allDocs)
        {
            Console.WriteLine($"{users.Name}'s email address is {users.Email} and " +
                $"has an id of {users.UserId}");
            Console.WriteLine();
        }
    }

    //read one user
    public string getOneUserByName(string username)
    {
        var findFilter = Builders<Users>.Filter.Eq("Name", username);

        var findResult = collection.Find(findFilter).FirstOrDefault();

        if (findResult == null)
        {
            return "I didn't find a user with the name that matched.";
        }
        return findResult.ToString();
    }

    //update 
    public string updateDoc(Users theuser, string nameorEmail, string updated)
    {
        var findFilter = Builders<Users>.Filter.Eq(nameorEmail, updated);

        if (nameorEmail.Equals("Email"))
        {
            var updateFilter = Builders<Users>.Update.Set(t => t.Email, updated);

            // The following FindOneAndUpdateOptions specify that we want the *updated* document
            // to be returned to us. By default, we get the document as it was *before*
            // the update.

            var options = new FindOneAndUpdateOptions<Users, Users>()
            {
                ReturnDocument = ReturnDocument.After
            };

            // The updatedDocument object is a Users object that reflects the
            // changes we just made.
            var updatedDocument = collection.FindOneAndUpdate(findFilter,
                updateFilter, options);

            return updatedDocument.ToString();
        }
        else if (nameorEmail.Equals("Name"))
        {
            var updateFilter = Builders<Users>.Update.Set(t => t.Name, updated);

            // The following FindOneAndUpdateOptions specify that we want the *updated* document
            // to be returned to us. By default, we get the document as it was *before*
            // the update.

            var options = new FindOneAndUpdateOptions<Users, Users>()
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
    public void deleteOneUser(string email)
    {
        var deleteResult = collection.DeleteOne(Builders<Users>.Filter.Eq("Email", email));
        Console.WriteLine($"I deleted {deleteResult.DeletedCount} records.");
    }

        //delete all
    public void deleteAll()
    {
        var filter = Builders<Users>.Filter.Empty;
        var result = collection.DeleteMany(filter);
        Console.WriteLine($"I deleted {result.DeletedCount} records.");
    }


    
}
