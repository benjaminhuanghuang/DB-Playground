using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace DBPlayground
{
    public class TodoModel1
    {
        [BsonId]
        public ObjectId TodoId { get; set; }

        [BsonRequired]
        public string Title { get; set; }

        public bool Completed { get; set; } = false;
        public DateTime? OpTime { get; set; } = null;
    }
    
    
    public class MongoDemo1
    {
        public static void GetData()
        {
            var client = new MongoClient("mongodb://user:user123@ds119618.mlab.com:19618/db_todo");
            var db = client.GetDatabase("db_todo");
            var coll = db.GetCollection<TodoModel1>("todos");

            var todos = coll.Find(t => t.Title == "a").ToListAsync().Result;
            foreach(var todo in todos)
            {
                Console.WriteLine(todo.Title);
            }
        }
    }
}