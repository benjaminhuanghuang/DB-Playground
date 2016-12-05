using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

using DBPlayground.Models;

namespace DBPlayground
{
   
    public class MongoDBService
    {
        private IMongoCollection<TodoModel> TodoCollection { get; }
        public MongoDBService(string dbUrl, string dbName, string collectionName)
        {
            var mongoClient = new MongoClient(dbUrl);
            var mongoDB = mongoClient.GetDatabase(dbName);

            TodoCollection = mongoDB.GetCollection<TodoModel>(collectionName);
        }
        
        public async Task InsertTodoAsync(TodoModel todo) => await TodoCollection.InsertOneAsync(todo);
        public void InsertTodo(TodoModel todo){
            TodoCollection.InsertOne(todo);
        }
        public async Task<List<TodoModel>> GetAllTodosAsync() {
            var todos = new List<TodoModel>();
            var allDocument = await TodoCollection.FindAsync(new BsonDocument());
            await allDocument.ForEachAsync(doc => todos.Add(doc));
            return todos;
        }

        public List<TodoModel> GetAllTodos() {
            var todos = new List<TodoModel>();
            var allDocument = TodoCollection.Find(new BsonDocument()).ToListAsync().Result;
            allDocument.ForEach(doc => todos.Add(doc));
            return todos;
        }
    }
    
    public class MongoDemo
    {
        // If add auth on one database, the dbUrl has to include databaes name at the end. 
        private static MongoDBService mogoDbService =  new MongoDBService("mongodb://user:user123@ds119618.mlab.com:19618/db_todo", 
                                                                          "db_todo", 
                                                                          "todos");

        public static void FindData()
        {
            var client = new MongoClient("mongodb://user:user123@ds119618.mlab.com:19618/db_todo");
            var db = client.GetDatabase("db_todo");
            var coll = db.GetCollection<TodoModel>("todos");

            var todos = coll.Find(t => t.Title == "a").ToListAsync().Result;
            foreach(var todo in todos)
            {
                Console.WriteLine(todo.Title);
            }
        }    
        public static void InsertData()
        {
            TodoModel todo = new TodoModel()
            {
                Title="Todo title",
                Completed=false
            };
            mogoDbService.InsertTodo(todo);
        }
        public static void  GetData() 
        {
             List<TodoModel> allTodos = mogoDbService.GetAllTodos();
             foreach(var todo in allTodos)
             {
                 Console.WriteLine(todo.Title);
             }
        }   
    }
}