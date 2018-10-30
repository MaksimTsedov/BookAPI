namespace TestLibraryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Moq;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Test class for CRUD operations for authors
    /// </summary>
    [TestClass]
    public class TestLibraryService_Authors
    {
        /// <summary>
        /// The library class to test
        /// </summary>
        private static Mock<IDataProvider> mockLibrary;

        /// <summary>
        /// Initializes the library instance with some books and authors.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            mockLibrary = new Mock<IDataProvider>();
        }

        /// <summary>
        /// Tests the getting of all authors.
        /// </summary>
        [TestMethod]
        public void TestGetAuthors()
        {
            mockLibrary.Setup(lib => lib.GetAllAuthors()).Returns(new List<Author> { default(Author) });
            ILibraryService library = mockLibrary.Object;

            var result = library.GetAllAuthors().Count();

            Assert.IsTrue(result == 1);
        }

        /// <summary>
        /// Tests correct author getting.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void TestGetAuthor_Correct(long id)
        {
            mockLibrary.Setup(lib => lib.GetAuthor(It.Is<long>((mockId)=>mockId==id))).Returns<Author>(author => author);
            ILibraryService library = mockLibrary.Object;

            var result = library.GetAuthor(id);

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests the getting of chozen author incorrect.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(7)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetAuthor_InCorrect(long id)
        {
            mockLibrary.Setup(lib => lib.GetAuthor(id)).Returns<Author>(author => author);
            ILibraryService library = mockLibrary.Object;

            var result = library.GetAuthor(id);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the list of author books (first author tests).
        /// </summary>
        [TestMethod]
        public void TestGetAuthorBooks_FirstAuthor()
        {
            mockLibrary.Setup(lib => lib.GetAuthorBooks(It.IsAny<long>())).Returns(new List<Book> { default(Book) });
            ILibraryService library = mockLibrary.Object;

            var result = library.GetAuthorBooks(1).Count();

            Assert.IsTrue(result == 1);
        }

        /// <summary>
        /// Tests correct the adding author.
        /// </summary>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow("Testauthor", "Lemuria")]
        [DataRow("Hello", "Ukraine")]
        public void TestAddAuthor_Correct(string fullname, string country)
        {
            mockLibrary.Setup(lib => lib.CreateAuthor(new Author(fullname, country))).Returns<Author>(author => author);
            ILibraryService library = mockLibrary.Object;

            var result = library.GetAllAuthors().Count();

            Assert.IsTrue(result == 1);
        }

        /// <summary>
        /// Tests correct the adding author.
        /// </summary>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        public void TestAddAuthor_Incorrect()
        {
            mockLibrary.Setup(lib => lib.CreateAuthor(new Author(string.Empty, string.Empty))).Returns<Author>(author => author);
            ILibraryService library = mockLibrary.Object;

            var result = library.GetAllAuthors().Count();

            Assert.IsTrue(result == 0);
        }

        /*/// <summary>
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

            Author actual = mockLibrary.UpdateAuthor(id, new Author(fullname, country));

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

            Author actual = mockLibrary.UpdateAuthor(id, new Author(fullname, country));

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
            mockLibrary.DeleteAuthor(id);

            List<Book> books = mockLibrary.GetAuthorBooks(id).ToList();

            Author actual = mockLibrary.GetAuthor(id);

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
            mockLibrary.DeleteAuthor(id);
        }*/
    }
}
