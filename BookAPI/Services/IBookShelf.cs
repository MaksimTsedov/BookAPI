namespace BookAPI.Services
{
    using System.Collections.Generic;
    using BookAPI.Models;

    /// <summary>
    /// Abstraction of book service to solve the problem of DI
    /// </summary>
    public interface IBookShelf
    {
        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>Enumeration of all books</returns>
        IEnumerable<Book> GetAllBooks();

        /// <summary>
        /// Gets the book by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Chozen book.</returns>
        Book GetBook(long id);

        /// <summary>
        /// Creates the specified book.
        /// </summary>
        /// <param name="book">The book.</param>
        Book CreateBook(Book book);

        /// <summary>
        /// Updates a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="book">The book.</param>
        /// <returns>Updated book.</returns>
        Book UpdateBook(long id, Book book);

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteBook(long id);
    }
}
