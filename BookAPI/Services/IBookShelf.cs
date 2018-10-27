namespace BookAPI.Services
{
    using System.Collections.Generic;
    using BookAPI.Models;

    public interface IBookShelf
    {
        IEnumerable<Book> GetAll();

        Book GetBook(long id);

        void Create(Book book);

        Book Update(long id, Book book);

        void Delete(long id);
    }
}
