namespace TestLibraryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Test class for CRUD operations for authors
    /// </summary>
    [TestClass]
    public class TestLibraryService_Authors
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
        /// Tests the getting of all authors.
        /// </summary>
        [TestMethod]
        public void TestGetAuthors()
        {
            List<Author> expected = new List<Author> {
                    new Author("Ray Bradbury", "USA"),
                    new Author("George Orwell", "Great Britain")
            };

            List<Author> actual = _libraryToTest.GetAllAuthors().ToList();

            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests correct author getting.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(1, "Ray Bradbury", "USA")]
        [DataRow(2, "George Orwell", "Great Britain")]
        public void TestGetAuthor_Correct(long id, string fullname, string country)
        {
            Author expected = new Author(fullname, country);

            Author actual = _libraryToTest.GetAuthor(id);

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the getting of chozen author incorrect.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetAuthor_InCorrect(long id)
        {
            Author actual = _libraryToTest.GetAuthor(id);

            Assert.AreEqual(actual, null);
        }

        /// <summary>
        /// Tests the list of author books (first author tests).
        /// </summary>
        [TestMethod]
        public void TestGetAuthorBooks_FirstAuthor()
        {
            Book expected = _libraryToTest.GetBook(1);
            Book actual = _libraryToTest.GetAuthorBooks(1).ToList().First();
            

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests correct the adding author.
        /// </summary>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow("Testauthor", "Lemuria")]
        public void TestAddAuthor_Correct(string fullname, string country)
        {
            Author expected = new Author(fullname, country);

            Author actual = _libraryToTest.CreateAuthor(expected);

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the update of a author is correct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(1, "Testing", "Ukraine")]
        public void TestUpdateAuthor_Correct(long id, string fullname, string country)
        {
            Author expected = new Author(fullname, country);

            Author actual = _libraryToTest.UpdateAuthor(id, new Author(fullname, country));

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Tests the update of an author is incorrect.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(3, "TestingWrong", "Ukraine")]
        [DataRow(-1, "TestingWronginRome", "Rome")]
        public void TestUpdateAuthor_InCorrect(long id, string fullname, string country)
        {
            Author expected = new Author(fullname, country);

            Author actual = _libraryToTest.UpdateAuthor(id, new Author(fullname, country));

            Assert.AreEqual(actual, null);
        }

        /// <summary>
        /// Tests correct deleting of a author.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteAuthor_Correct(long id)
        {
            _libraryToTest.DeleteAuthor(id);

            List<Book> books = _libraryToTest.GetAuthorBooks(id).ToList();

            Author actual = _libraryToTest.GetAuthor(id);

            Assert.AreEqual(books.Count, 0);
            Assert.AreEqual(actual, null);
        }

        /// <summary>
        /// Tests incorrect deleting of a author.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-11)]
        [DataRow(0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteAuthor_InCorrect(long id)
        {
            _libraryToTest.DeleteAuthor(id);
        }
    }
}
