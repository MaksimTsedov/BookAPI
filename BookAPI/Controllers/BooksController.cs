namespace BookAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BookAPI.Services;
    using BookAPI.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookShelf _books;

        public BooksController(IBookShelf books)
        {
            _books = books;
        }

        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid book input.");
            }

            _books.Create(book);
            return Created("books", book);
        }

        [HttpPut]
        public ActionResult<Book> PutBook(long id, Book item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            Book updatedBook = _books.Update(id, item);
            if (updatedBook == null)
            {
                return NotFound();
            }
            return Ok(updatedBook);
        }

        [HttpDelete]
        public IActionResult DeleteBook(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid student id");
            }

            _books.Delete(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> booklist = _books.GetAll().ToList();
            if (booklist.Count == 0)
            {
                return NotFound();
            }

            return Ok(booklist);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            Book bookToFind = _books.GetAll().ToList().Find((book) => book.Id == id);
            if (bookToFind == null)
            {
                return NotFound();
            }

            return Ok(bookToFind);
        }
    }
}
