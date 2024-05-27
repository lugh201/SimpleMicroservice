// Repositories/IBookRepository.cs
using BookGallery.Models;

namespace BookGallery.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(string id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(string id, Book book);
        Task DeleteBookAsync(string id);
    }
}
