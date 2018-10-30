namespace BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using BookAPI.Models;

    /// <summary>
    /// Library service with typical CRUD operations
    /// </summary>
    /// <seealso cref="BookAPI.Services.IBookShelf" />
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// The list of books
        /// </summary>
        private List<Book> _books;

        /// <summary>
        /// The list of authors
        /// </summary>
        private List<Author> _authors;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService"/> class.
        /// </summary>
        public LibraryService()
        {
            _authors = new List<Author>
            {
                new Author("Ray Bradbury", "USA"),
                new Author("George Orwell", "Great Britain")
            };
            _books = new List<Book>
            {
                new Book("451 fahrenheit", 1, 158, 1953),
                new Book("1984", 2, 328, 1949)
            };
        }

        /// <summary>
        /// Creates and adds to list an author.
        /// </summary>
        /// <param name="author">The author.</param>
        public Author CreateAuthor(Author author)
        {
            _authors.Add(author);
            return author;
        }

        /// <summary>
        /// Creates and adds to list a book.
        /// </summary>
        /// <param name="book">The book.</param>
        public Book CreateBook(Book book)
        {
            if (_authors.Find((author) => author.Id == book.AuthorId) == null)
            {
                book.AuthorId = null;
            }

            _books.Add(book);
            return book;
        }

        /// <summary>
        /// Deletes writer and his books by his id.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no author with such id.</exception>
        public void DeleteAuthor(long id)
        {
            Author authorToDelete = _authors.Find((author) => author.Id == id);
            if (authorToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _authors.Remove(authorToDelete);
            _books.RemoveAll((book) => book.AuthorId == id);
        }

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no book with such id.</exception>
        public void DeleteBook(long id)
        {
            Book bookToDelete = _books.Find((book) => book.Id == id);
            if (bookToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _books.Remove(bookToDelete);
        }

        /// <summary>
        /// Gets list of authors.
        /// </summary>
        /// <returns>
        /// Enumeration of all authors.
        /// </returns>
        public IEnumerable<Author> GetAllAuthors()
        {
            foreach (var author in _authors)
            {
                yield return author;
            }
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>
        /// Enumeration of all books
        /// </returns>
        public IEnumerable<Book> GetAllBooks()
        {
            foreach (var book in _books)
            {
                yield return book;
            }
        }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <returns>
        /// Certain author.
        /// </returns>
        public Author GetAuthor(long id)
        {
            return _authors.Find((author) => author.Id == id);
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
        /// Updates information of chozen author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <param name="author">The author.</param>
        /// <returns>
        /// Updated writer
        /// </returns>
        public Author UpdateAuthor(long id, Author author)
        {
            Author authorToUpdate = _authors.Find((oldauthor) => oldauthor.Id == id);
            if (authorToUpdate != null)
            {
                authorToUpdate.Clone(author);
            }

            return authorToUpdate;
        }

        /// <summary>
        /// Updates a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Updated book.
        /// </returns>
        public Book UpdateBook(long id, Book book)
        {
            Book bookToUpdate = _books.Find((oldbook) => oldbook.Id == id);
            if (bookToUpdate != null)
            {
                if (_authors.Find((author) => author.Id == book.AuthorId) == null)
                {
                    book.AuthorId = null;
                }

                bookToUpdate.Clone(book);
            }

            return bookToUpdate;
        }

        /// <summary>
        /// Updates book`s author.
        /// </summary>
        /// <param name="book_Id">The book identifier.</param>
        /// <param name="author_Id">The author identifier.</param>
        /// <returns>
        /// Updated book.
        /// </returns>
        public Book UpdateBookAuthor(long book_Id, long author_Id)
        {
            Book result;
            Book bookToUpdate = _books.Find((oldbook) => oldbook.Id == book_Id);
            Author authorUpdate = _authors.Find((oldauthor) => oldauthor.Id == author_Id);
            if (bookToUpdate != null)
            {
                if (authorUpdate != null)
                {
                    bookToUpdate.AuthorId = authorUpdate.Id;
                }
                else
                {
                    bookToUpdate.AuthorId = null;
                }
                result = bookToUpdate;
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Gets all books of author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Book> GetAuthorBooks(long id)
        {
            return _books.FindAll((book) => book.AuthorId == id);
        }
    }
}
