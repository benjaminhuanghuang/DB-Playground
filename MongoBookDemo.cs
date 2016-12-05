using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

using DBPlayground.Models;

// https://www.youtube.com/watch?v=rMbIx4Yk6U8
namespace DBPlayground
{
    public class MongoBookDemo
    {
        public static void Get()
        {
            var client = new MongoClient("mongodb://ben:ben123@ds119768.mlab.com:19768/db_bentest");
            var db = client.GetDatabase("db_bentest");
            var coll = db.GetCollection<Book>("books");

            var publisherId = new ObjectId("");
            Task<List<Book>> booksAsync = coll.Find(b => b.Publisher == publisherId)
                                        .Limit(5)
                                        .ToListAsync();

            // Find with waiting
            var books = coll.Find(b => b.Publisher == publisherId)
                                        .Limit(5)
                                        .SortBy(b => b.Title)
                                        .ToListAsync()
                                        .Result;

            foreach(var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void Update()
        {
            var client = new MongoClient("mongodb://ben:ben123@ds119768.mlab.com:19768/db_bentest");
            var db = client.GetDatabase("db_bentest");
            var coll = db.GetCollection<Book>("books");

            var publisherId = new ObjectId("");
            // Find with waiting
            var books = coll.Find(b => b.Publisher == publisherId)
                                        .Limit(5)
                                        .SortBy(b => b.Title)
                                        .ToListAsync()
                                        .Result;
            var book = books.First();
            book.Title = book.Title.ToUpper();
            coll.SaveAsync(book).Wait(); 
        }
    }
}