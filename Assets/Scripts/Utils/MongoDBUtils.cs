﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using UnityEngine;

using static Constants.MongoDBAtlas;

public class UserDocument
{
    public ObjectId _id { set; get; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("image")]
    public string Image { get; set; }

    [BsonElement("bio")]
    public string Bio { get; set; }

    [BsonElement("firstName")]
    public string FirstName { get; set; }

    [BsonElement("lastName")]
    public string LastName { get; set; }
}
public class MongoDBUtils
{

    private static IMongoDatabase getDatabase()
    {
        var client = new MongoClient(URI);
        
        return client.GetDatabase(DATABASE);
    }

    public static User GetUser(string username)
    {
        try
        {
            var usersCollection = getDatabase().GetCollection<UserDocument>("users");

            var filter = Builders<UserDocument>.Filter.Eq(user => user.Username, username);

            var user = usersCollection.Find(filter).FirstOrDefault();

            if (user == null) return null;
            
            return new User()
            {
                Username = user.Username,
                Password = user.Password,
                Image = user.Image,
                Bio = user.Bio,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DeckCollection = null
            };

        } catch (MongoException ex)
        {
            Debug.LogException(ex);
            return null;
        }
        
    }
}