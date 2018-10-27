namespace BookAPI.Services
{
    using System.Collections.Generic;
    using BookAPI.Models;

    public class BookShelf : IBookShelf
    {
        private List<Book> _books;

        public BookShelf()
        {
            _books = new List<Book>
            {
                new Book("451 fahrenheit", "Ray Bradbury", 158, 1953),
                new Book("1984", "George Orwell", 328, 1949)
            };
        }

        public void Create(Book book)
        {
            _books.Add(book);
        }

        public void Delete(long id)
        {
            Book bookToDelete = _books.Find((book) => book.Id == id);
            _books.Remove(bookToDelete);
        }

        public IEnumerable<Book> GetAll()
        {
            foreach (var book in _books)
            {
                yield return book;
            }
        }

        public Book GetBook(long id)
        {
            return _books.Find((book) => book.Id == id);
        }

        public Book Update(long id, Book book)
        {
            Book result = null;
            Book bookToUpdate = _books.Find((oldbook) => oldbook.Id == id);
            if (book != null)
            {

                book.Id = id;
                int index = _books.IndexOf(bookToUpdate);
                _books[index] = book;
                result = _books[index];
            }

            return result;
        }
    }
}
