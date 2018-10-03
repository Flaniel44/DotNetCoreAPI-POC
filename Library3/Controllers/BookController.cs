using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library3.Models;
using Library3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library3.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //CRUD operations

        [HttpGet, Authorize]
        public async Task<IEnumerable<BookItem>> Get()
        {
            return await _bookRepository.GetAllBooks();
        }

        [HttpGet("{id}")]
        public async Task<BookItem> Get(string id)
        {
            return await _bookRepository.GetBook(id);
        }

        // POST api/book - creates a new book
        [HttpPost]
        public void Post([FromBody] BookParam newBook)
        {
            _bookRepository.AddBook(new BookItem
            {
                Name = newBook.Name,
                ISBN = newBook.ISBN,
                TotalQty = newBook.TotalQty,
                AvailableQty = newBook.AvailableQty,
                Genre = newBook.Genre
            });
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            _bookRepository.RemoveBook(name);
        }
    }
    public class BookParam
    {
        public string Name { get; set; } = string.Empty;
        public int ISBN { get; set; } = 0;
        public int TotalQty { get; set; } = 0;
        public int AvailableQty { get; set; } = 0;
        public string Genre { get; set; } = string.Empty;
    }
}