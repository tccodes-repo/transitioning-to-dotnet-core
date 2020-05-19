using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Repository.Mongo
{
    public class MongoRepository : IRepository
    {

        private readonly IMongoDatabase _database;

        public MongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IList<Todo>> GetAllAsync()
        {
            return await _database.GetCollection<Todo>("todos")
                .Find(Builders<Todo>.Filter.Empty)
                .ToListAsync();
        }

        public async Task AddTodoAsync(Todo todo)
        {
            await _database.GetCollection<Todo>("todos").InsertOneAsync(todo);
        }
    }
}