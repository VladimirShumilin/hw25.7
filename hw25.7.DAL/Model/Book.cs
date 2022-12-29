using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw25.DAL.Model
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Name { get; set; } = "";
        public int YearOfIssue { get; set; }
        public string Genre { get; set; } = "";

        [Required]
        public int AuthorID { get; set; }
        [Required]
        public Author Author { get; set; } = new();
    }
}
