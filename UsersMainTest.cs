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
            string password = Environment.GetEnvironmentVariable("<password>");

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

            //one database 3 collections (users, problems, submissions)
            var dbName = "TigerByteDB";
            var collectionName = "UsersColl";

            var collection = client.GetDatabase(dbName)
                   .GetCollection<Users>(collectionName);

            
        }
        




    }

    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }

        public Users(string name, string email, int userid)
        {
            this.Name = name;
            this.Email = email;
            this.UserId = userid;

        }

        /// <summary>
        /// This static method is just here so we have a convenient way
        /// to generate sample user data.
        /// </summary>
        /// <returns>A list of Users</returns>       
        public static List<Users> GetUsers()
        {
            return new List<Users>()
            {

                new Users("Olivia Navarro", "olivia.navarro@doane.edu", 1),
                new Users("Kamryn Plock", "kamryn.plock@doane.edu", 2),
                new Users("Mark Meysenburg", "mark.meysenburg@doane.edu", 3),
                new Users("Alec Engebretson", "alec.engebretson@doane.edu", 4),


            };
        }

        internal void InsertOne(Users theuser)
        {
            throw new NotImplementedException();
        }
    }
}
