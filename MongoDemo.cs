using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace DBPlayground
{
    public class TodoModel
    {
        [BsonId]
        public ObjectId TodoId { get; set; }

        [BsonRequired]
        public string Title { get; set; }

        public bool Completed { get; set; } = false;
        public DateTime? OpTime { get; set; } = null;
    }
    public class MongoDBService
    {
        private IMongoCollection<TodoModel> TodoCollection { get; }
        public MongoDBService(string dbUrl, string dbName, string collectionName)
        {
            var mongoClient = new MongoClient(dbUrl);
            var mongoDB = mongoClient.GetDatabase(dbName);

            TodoCollection = mongoDB.GetCollection<TodoModel>(collectionName);
        }
        
        public async Task InsertTodo(TodoModel todo) => await TodoCollection.InsertOneAsync(todo);

        public async Task<List<TodoModel>> GetAllTodos() {
            var todos = new List<TodoModel>();
            var allDocument = await TodoCollection.FindAsync(new BsonDocument());
            await allDocument.ForEachAsync(doc => todos.Add(doc));
            return todos;
        }
    }
    
    public class MongoDemo
    {
        private static MongoDBService mogoDbService = new MongoDBService("mongodb://user:user123@ds119618.mlab.com:19618", "db_todo", "todos");
            
        public static async void InsertData()
        {
            TodoModel todo = new TodoModel()
            {
                Title="Todo title",
                Completed=false
            };
            await mogoDbService.InsertTodo(todo);
        }
        public static async void  GetData() 
        {
             List<TodoModel> allTodos = await mogoDbService.GetAllTodos();
             foreach(var todo in allTodos)
             {
                 Console.WriteLine(todo.Title);
             }
        }   
    }
}