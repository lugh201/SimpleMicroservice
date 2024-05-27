using BookGallery.Models;
using BookGallery.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookGallery.DTOs;

namespace BookGallery.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get() => await _repository.GetAllBooksAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BookDTO dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description= dto.Description,
                PublishedDate = dto.PublishedDate,
            };

            await _repository.CreateBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] BookDTO dto)
        {
          
            var existingBook = await _repository.GetBookByIdAsync(id);
            if (existingBook == null) return NotFound();

            existingBook.Title = dto.Title == null ? existingBook.Title : dto.Title;
            existingBook.Author = dto.Author == null ? existingBook.Author : dto.Author;
            existingBook.Description = dto.Description == null ? existingBook.Description : dto.Description;
           
            await _repository.UpdateBookAsync(id, existingBook);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingBook = await _repository.GetBookByIdAsync(id);
            if (existingBook == null) return NotFound();
            await _repository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
