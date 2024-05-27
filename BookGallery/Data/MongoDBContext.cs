using BookGallery.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookGallery.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
    }
}
