namespace BookAPI.Services
{
    using BookAPI.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Library manager abstraction
    /// </summary>
    /// <seealso cref="BookAPI.Services.IAuthorService" />
    /// <seealso cref="BookAPI.Services.IBookShelf" />
    // HACK: Is that normal to use such extension architecture?
    public interface ILibraryService : IAuthorService, IBookShelf
    {
        /// <summary>
        /// Updates book`s author.
        /// </summary>
        /// <param name="book_Id">The book identifier.</param>
        /// <param name="author_Id">The author identifier.</param>
        /// <returns>Updated book.</returns>
        Book UpdateBookAuthor(long book_Id, long author_Id);

        /// <summary>
        /// Gets the books written by chozen author.
        /// </summary>
        /// <param name="author_Id">The author identifier.</param>
        /// <returns>Enumeration of books.</returns>
        IEnumerable<Book> GetAuthorBooks(long author_Id);
    }
}
