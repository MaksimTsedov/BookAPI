namespace BookAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Book
    {
        private static int _globalCount;

        public Book(string title, string author, int numberOfPages, int year)
        {
            this.Title = title;
            this.Author = author;
            this.NumberOfPages = numberOfPages;
            this.Year = year;
            this.Id = ++_globalCount;
        }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number")]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        [Range(1, 2500, ErrorMessage = "Book number of pages should be natural and be less 2500")]
        public int NumberOfPages { get; set; }

        [Range(-2000, 2018, ErrorMessage = "Wrong year parameter (never heard about books from that time)!")]
        public int Year { get; set; }
    }
}