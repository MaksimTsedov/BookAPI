namespace BookAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Book entity
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The global count for id autoincrement
        /// </summary>
        private static int _globalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="author">The author.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year.</param>
        public Book(string title, string author, int numberOfPages, int year)
        {
            this.Title = title;
            this.Author = author;
            this.NumberOfPages = numberOfPages;
            this.Year = year;
            this.Id = ++_globalCount;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title of a book.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of a book.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        /// <value>
        /// The number of pages.
        /// </value>
        [Range(1, 2500, ErrorMessage = "Book number of pages should be natural and be less 2500")]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the year of publishing.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [Range(-2000, 2018, ErrorMessage = "Wrong year parameter (never heard about books from that time)!")]
        public int Year { get; set; }
    }
}