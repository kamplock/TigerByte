using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TigerByte
{
    class UsersMainTest
	{
        public static void Main(string[] args)
        {
            //TODO talk to Chris or Mark about security
            string password = "w3schools";
                //Environment.GetEnvironmentVariable("<password>");

            var mongoUri = "mongodb+srv://root:" + password + "@cluster0.nzth94y.mongodb.net/?retryWrites=true&w=majority";

            // The IMongoClient is the object that defines the connection to our
            // datastore (Atlas, for example)
            IMongoClient client;

            // An IMongoCollection defines a connection to a specific MongoDB
            // collection. Your app may have one or many different IMongoCollection
            // objects.
            IMongoCollection<Users> UsersColl;

            //try to connect
            try
            {
                client = new MongoClient(mongoUri);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }

            //one database 2 collections (users, problems)
            var dbName = "TigerByteDB";
            var collectionName1 = "UsersColl";
            var collectionName2 = "ProblemsColl";


            var collection1 = client.GetDatabase(dbName)
                   .GetCollection<Users>(collectionName1);
            var collection2 = client.GetDatabase(dbName)
                   .GetCollection<Users>(collectionName2);




            //var usersCRUD = new UsersCRUD(client.GetDatabase(dbName));
            var problemsCRUD = new ProblemsCRUD(client.GetDatabase(dbName));

            // Insert a new user using the InsertUser method from UsersCRUD
            //var newUser = new Users("Susan Smith", "ss@doane.edu");
            //var newerUser = new Users("Bob Smith", "bs@doane.edu");

            var newProblem = new Problems("Hello World", "Write a program that prints out Hello World!", "print('Hello world!')", "strings");


            //usersCRUD.InsertUser(newUser);
            //usersCRUD.InsertUser(newerUser);
            //usersCRUD.getAllUsers();


            problemsCRUD.InsertProblem(newProblem);
            problemsCRUD.getAllProblems();
            
            
            // usersCRUD.deleteOneUser("ss@doane.edu");

           //usersCRUD.deleteAll();
            //dbName.collectionName.find().pretty();


        }
        




    }

    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public Users(string name, string email)
        {
            this.Name = name;
            this.Email = email;

        }


    }

    public class Problems
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProblemName { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
        public string Type { get; set; }


        public Problems(string pn, string p, string s, string type)
        {
            this.ProblemName = pn;
            this.Problem = p;
            this.Solution = s;
            this.Type = type;
        }


    }
}
