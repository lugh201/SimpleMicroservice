using BookGallery.Data;
using BookGallery.Models;
using MongoDB.Driver;

namespace BookGallery.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepository(MongoDbContext context)
        {
            _books = context.Books;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync() => await _books.Find(book => true).ToListAsync();
        public async Task<Book> GetBookByIdAsync(string id) => await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
        public async Task CreateBookAsync(Book book) => await _books.InsertOneAsync(book);
        public async Task UpdateBookAsync(string id, Book book) => await _books.ReplaceOneAsync(book => book.Id == id, book);
        public async Task DeleteBookAsync(string id) => await _books.DeleteOneAsync(book => book.Id == id);
    }
}
