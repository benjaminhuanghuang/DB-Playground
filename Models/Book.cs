using System;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
namespace DBPlayground.Models
{
    public class Book:IIdentified
    {
        public ObjectId Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }

        public ObjectId Publisher { get; set; }

        public ImageUrl[] ImageUrls { get; set; }
        public Rating[] Ratings { get; set; }
    }

    public class ImageUrl
    {
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Rating
    {
        public ObjectId UserId { get; set; }
        public int Value { get; set; }
    }
}