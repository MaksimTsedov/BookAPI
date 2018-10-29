namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Library manager
    /// </summary>
    /// <seealso cref="BusinessLogic_BookAPI.Services.ILibraryService" />
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// List of books
        /// </summary>
        private List<Book> _books;

        /// <summary>
        /// List of authors
        /// </summary>
        private List<Author> _authors;

        /// <summary>
        /// List of genres
        /// </summary>
        private List<Genre> _genres;

        // HACK: Is it appropriate to use such "SortedSet<KeyValuePair<>>" link collection?

        /// <summary>
        /// List of book-genre link
        /// </summary>
        private SortedSet<KeyValuePair<long, long>> _bookGenrePair;

        /// <summary>
        /// List of book-author pair
        /// </summary>
        private SortedSet<KeyValuePair<long, long>> _bookAuthorPair;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService"/> class.
        /// </summary>
        public LibraryService()
        {
            _authors = new List<Author>
            {
                new Author("Ray Bradbury", "USA"),
                new Author("George Orwell", "Great Britain"),
                new Author("Homer", "Ancient Greece")
            };

            _books = new List<Book>
            {
                new Book("451 fahrenheit", 158, 1953),
                new Book("1984", 328, 1949),
                new Book("Odyssey", 800, -800),
                new Book("Dandelion wine", 164, 1957),
                new Book("Folk tails", 160, 1890)
            };

            _genres = new List<Genre>
            {
                new Genre("Dystopia"),
                new Genre("Fiction"),
                new Genre("Epos"),
                new Genre("Fairy tail")
            };

            _bookGenrePair = new SortedSet<KeyValuePair<long, long>>
            {
                new KeyValuePair<long, long>(1, 1),
                new KeyValuePair<long, long>(1, 2),
                new KeyValuePair<long, long>(2, 1),
                new KeyValuePair<long, long>(3, 3),
                new KeyValuePair<long, long>(5, 4)
            };

            _bookAuthorPair = new SortedSet<KeyValuePair<long, long>>
            {
                new KeyValuePair<long, long>(1, 1),
                new KeyValuePair<long, long>(2, 2),
                new KeyValuePair<long, long>(3, 3),
                new KeyValuePair<long, long>(4, 1)
            };
        }

        #region Creating


        /// <summary>
        /// Creates and adds to list an author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns>
        /// Created author.
        /// </returns>
        public Author CreateAuthor(Author author)
        {
            _authors.Add(author);
            return author;
        }

        /// <summary>
        /// Creates and adds to list a book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Created book.
        /// </returns>
        public Book CreateBook(Book book)
        {
            _books.Add(book);
            return book;
        }

        /// <summary>
        /// Creates and adds to list a new genre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns>
        /// Created genre.
        /// </returns>
        public Genre CreateGenre(Genre genre)
        {
            _genres.Add(genre);
            return genre;
        }

        /// <summary>
        /// Adds the genre to book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>Is added new link.</returns>
        public bool AddGenreToBook(long book_id, long genre_id)
        {
            bool result = false;
            Genre genreToFind = _genres.Find((genre) => genre.Id == genre_id);
            if (GetBook(book_id) != null && genreToFind != null)
            {
                _bookGenrePair.Add(new KeyValuePair<long, long>(book_id, genre_id));
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is added new link.</returns>
        public bool AddAuthorOfBook(long book_id, long author_id)
        {
            bool result = false;
            if (GetBook(book_id) != null && GetAuthor(author_id) != null)
            {
                _bookAuthorPair.Add(new KeyValuePair<long, long>(book_id, author_id));
                result = true;
            }

            return result;
        }

        #endregion

        #region Deleting

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
            _bookAuthorPair.RemoveWhere((bookAuthorPair) => bookAuthorPair.Value == id);
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
            _bookAuthorPair.RemoveWhere((bookAuthorPair) => bookAuthorPair.Key == id);
            _bookGenrePair.RemoveWhere((bookAuthorPair) => bookAuthorPair.Key == id);
        }

        /// <summary>
        /// Deletes a genre by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no genre with such id.</exception>
        /// <exception cref="FormatException">It is thrown if there are exist books of this genre.</exception>
        public void DeleteGenre(long id)
        {
            Genre genreToDelete = _genres.Find((genre) => genre.Id == id);
            if (genreToDelete == null)
            {
                throw new ArgumentNullException();
            }

            KeyValuePair<long, long> existingBookOfGenre = _bookGenrePair.
                Where(bookGenrePair => bookGenrePair.Value == id).FirstOrDefault();
            if (existingBookOfGenre.Equals(default(KeyValuePair<long, long>)))
            {
                throw new FormatException();
            }
            else
            {
                _genres.Remove(genreToDelete);
            }
        }

        #endregion

        #region Getting all entities

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
        /// Gets all genres.
        /// </summary>
        /// <returns>
        /// Enumeration of all genres
        /// </returns>
        public IEnumerable<Genre> GetAllGenres()
        {
            foreach (var genre in _genres)
            {
                yield return genre;
            }
        }

        #endregion

        #region Getting by id

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

        #endregion

        #region Updating

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
                bookToUpdate.Clone(book);
            }

            return bookToUpdate;
        }

        #endregion

        #region Get genre or author books

        /// <summary>
        /// Gets all books of author.
        /// </summary>
        /// <param name="author_Id">The identifier of author.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Book> GetAuthorBooks(long author_Id)
        {
            List<KeyValuePair<long, long>> booksOfChozenAuthor = _bookAuthorPair.
                Where(_bookAuthorPair => _bookAuthorPair.Value == author_Id).ToList();
            foreach (var bookAuthorPair in booksOfChozenAuthor)
            {
                yield return _books.Find((book) => book.Id == bookAuthorPair.Key);
            }
        }

        /// <summary>
        /// Gets all books of supposed genre.
        /// </summary>
        /// <param name="genre_Id">The genre identifier.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Book> GetAllGenreBooks(long genre_Id)
        {
            List<KeyValuePair<long, long>> booksOfChozenGenre = _bookGenrePair.
                Where(bookGenrePair => bookGenrePair.Value == genre_Id).ToList();
            foreach (var bookGenrePair in booksOfChozenGenre)
            {
                yield return _books.Find((book) => book.Id == bookGenrePair.Key);
            }
        }

        #endregion
    }
}
