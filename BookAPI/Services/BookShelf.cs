namespace BookAPI.Services
{
    using System.Collections.Generic;
    using BookAPI.Models;

    /// <summary>
    /// Class for operation with book list
    /// </summary>
    /// <seealso cref="BookAPI.Services.IBookShelf" />
    public class BookShelf : IBookShelf
    {
        /// <summary>
        /// The list of books
        /// </summary>
        private List<Book> _books;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookShelf"/> class.
        /// </summary>
        public BookShelf()
        {
            _books = new List<Book>
            {
                new Book("451 fahrenheit", "Ray Bradbury", 158, 1953),
                new Book("1984", "George Orwell", 328, 1949)
            };
        }

        /// <summary>
        /// Creates the specified book.
        /// </summary>
        /// <param name="book">The book.</param>
        public void Create(Book book)
        {
            _books.Add(book);
        }

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(long id)
        {
            Book bookToDelete = _books.Find((book) => book.Id == id);
            _books.Remove(bookToDelete);
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>Enumeration of all books.</returns>
        public IEnumerable<Book> GetAll()
        {
            foreach (var book in _books)
            {
                yield return book;
            }
        }

        /// <summary>
        /// Gets the book by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The chozen book.</returns>
        public Book GetBook(long id)
        {
            return _books.Find((book) => book.Id == id);
        }

        /// <summary>
        /// Updates a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Updated book.
        /// </returns>
        public Book Update(long id, Book book)
        {
            Book bookToUpdate = _books.Find((oldbook) => oldbook.Id == id);
            if (book != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.NumberOfPages = book.NumberOfPages;
                bookToUpdate.Year = book.Year;
            }

            return bookToUpdate;
        }
    }
}
