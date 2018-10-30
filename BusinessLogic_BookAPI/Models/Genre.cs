namespace BusinessLogic_BookAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Genre entity
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// The global count for id autoincrement
        /// </summary>
        private static int _globalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        /// <param name="naming">The naming.</param>
        public Genre(string naming)
        {
            this.Naming = naming;
            this.Id = ++_globalCount;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required(ErrorMessage = "Id is always required!")]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number!")]
        public long Id { get; private set; }

        /// <summary>
        /// Gets or sets the naming.
        /// </summary>
        /// <value>
        /// The naming.
        /// </value>
        [Required(ErrorMessage = "Genre should have his name!")]
        [StringLength(50)]
        public string Naming { get; set; }
    }
}
