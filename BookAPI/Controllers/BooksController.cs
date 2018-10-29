namespace BookAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Controller for realization of CRUD operations about books
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        /// <summary>
        /// The interface reference for DI inverse
        /// </summary>
        private readonly ILibraryService _books;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="books">The instance of books.</param>
        public BooksController(ILibraryService books)
        {
            _books = books;
        }

        /// <summary>
        /// Creates and adds the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid book input.");
            }

            _books.CreateBook(book);
            return Created("books", book);
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The book.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateBook(long id, Book item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            Book updatedBook = _books.UpdateBook(id, item);
            if (updatedBook == null)
            {
                return NotFound("No book with such id!");
            }

            return Ok(updatedBook);
        }

        /// <summary>
        /// Updates the book author
        /// </summary>
        /// <param name="book_id">The identifier of a book.</param>
        /// <param name="author_id">The identifier of a writer.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPut("{book_id}/{author_id}")]
        public IActionResult UpdateBook(long book_id, long author_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            Book updatedBook = _books.UpdateBookAuthor(book_id, author_id);
            if (updatedBook == null)
            {
                return NotFound("No book or author with such id!");
            }

            return Ok(updatedBook);
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid student id");
            }

            try
            {
                _books.DeleteBook(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("No book with such Id!");
            }
           
            return Ok();
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> booklist = _books.GetAllBooks().ToList();
            if (booklist.Count == 0)
            {
                return NotFound("Any book are not recorded!");
            }

            return Ok(booklist);
        }

        /// <summary>
        /// Gets a book the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            Book bookToFind = _books.GetBook(id);
            if (bookToFind == null)
            {
                return NotFound("No book with such id!");
            }

            return Ok(bookToFind);
        }
    }
}
