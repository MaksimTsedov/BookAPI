namespace TestLibraryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BookAPI.Services;
    using BookAPI.Models;

    /// <summary>
    /// Test class for CRUD operations for books
    /// </summary>
    [TestClass]
    public class TestLibraryService_Books
    {
        /// <summary>
        /// The library class to test
        /// </summary>
        private static LibraryService _libraryToTest;

        /// <summary>
        /// Initializes the library instance with some books and authors.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _libraryToTest = new LibraryService();
        }

        /// <summary>
        /// Tests the getting of all books.
        /// </summary>
        [TestMethod]
        public void TestGetBooks()
        {
            List<Book> expected = new List<Book> {
                new Book("451 fahrenheit", 1, 158, 1953),
                new Book("1984", 2, 328, 1949)
            };

            List<Book> actual = _libraryToTest.GetAllBooks().ToList();

            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the getting of chozen book correct.
        /// </summary>
        /// <param name="id">The identifier of book.</param>
        /// <param name="title">The title.</param>
        /// <param name="idAuthor">The identifier of author.</param>
        /// <param name="numberofPages">The number of pages.</param>
        /// <param name="year">The year.</param>
        [TestMethod]
        [DataRow(1, "451 fahrenheit", 1, 158, 1953)]
        [DataRow(2, "1984", 2, 328, 1949)]
        public void TestGetBook_Correct(long id, string title, long idAuthor, int numberofPages, int year)
        {
            Book expected = new Book(title, idAuthor, numberofPages, year);

            Book actual = _libraryToTest.GetBook(id);

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the getting of chozen book incorrect.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetBook_InCorrect(long id)
        {
            Book actual = _libraryToTest.GetBook(id);

            Assert.AreEqual(actual, null);
        }


        /// <summary>
        /// Tests the adding book correct.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="idAuthor">The identifier of author.</param>
        /// <param name="numberofPages">The number of pages.</param>
        /// <param name="year">The year.</param>
        [TestMethod]
        [DataRow("Test", 3, 1000, 1953)]
        [DataRow("TestN2", 1, 222, -200)]
        public void TestAddBook_Correct(string title, long idAuthor, int numberofPages, int year)
        {
            Book expected = new Book(title, idAuthor, numberofPages, year);

            Book actual = _libraryToTest.CreateBook(expected);

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the update of a book correct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="idAuthor">The identifier of author.</param>
        /// <param name="numberofPages">The number of pages.</param>
        /// <param name="year">The year.</param>
        [TestMethod]
        [DataRow(1, "New Rhino", 1, 100, 709)]
        [DataRow(2, "Meaw", 2, 100, 709)]
        public void TestUpdateBook_Correct(long id, string title, long idAuthor, int numberofPages, int year)
        {
            Book expected = new Book(title, idAuthor, numberofPages, year);

            Book actual = _libraryToTest.UpdateBook(id, new Book(title, idAuthor, numberofPages, year));

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the update of a book incorrect.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="idAuthor">The identifier of author.</param>
        /// <param name="numberofPages">The number of pages.</param>
        /// <param name="year">The year.</param>
        [TestMethod]
        [DataRow(3, "New Rhino", 1, 100, 709)]
        [DataRow(-1, "Meaw", 3, 100, 709)]
        public void TestUpdateBook_InCorrect(long id, string title, long idAuthor, int numberofPages, int year)
        {
            Book expected = new Book(title, idAuthor, numberofPages, year);

            Book actual = _libraryToTest.UpdateBook(id, new Book(title, idAuthor, numberofPages, year));

            Assert.AreEqual(actual, null);
        }

        /// <summary>
        /// Tests the update of a book author if author exists.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        [TestMethod]
        [DataRow(1, 2)]
        public void TestUpdateBookAuthor_AuthorExist(long book_id, long author_id)
        {
            Book expected = _libraryToTest.GetBook(book_id);
            expected.AuthorId = author_id;

            Book actual = _libraryToTest.UpdateBookAuthor(book_id, author_id);

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the update of a book author if there is no author.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        [TestMethod]
        [DataRow(2, 5)]
        public void TestUpdateBookAuthor_NoAuthor(long book_id, long author_id)
        {
            Book expected = _libraryToTest.GetBook(book_id);
            expected.AuthorId = null;

            Book actual = _libraryToTest.UpdateBookAuthor(book_id, author_id);

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the update of a book author if book doesn`t exist.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        [TestMethod]
        [DataRow(3, 1)]
        [DataRow(-1, 5)]
        public void TestUpdateBookAuthor_NoBook(long book_id, long author_id)
        {
            Book actual = _libraryToTest.UpdateBookAuthor(book_id, author_id);

            Assert.AreEqual(actual, null);
        }

        /// <summary>
        /// Tests correct deleting of a book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteBook_Correct(long id)
        {
            _libraryToTest.DeleteBook(id);

            Book actual = _libraryToTest.GetBook(id);

            Assert.AreEqual(actual, null);
        }

        /// <summary>
        /// Tests incorrect deleting of a book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(-11)]
        [DataRow(0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteBook_InCorrect(long id)
        {
            _libraryToTest.DeleteBook(id);
        }
    }
}
