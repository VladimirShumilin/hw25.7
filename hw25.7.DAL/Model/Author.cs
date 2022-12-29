using System.ComponentModel.DataAnnotations;

namespace hw25.DAL.Model
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string FullName { get; set; } = "";
        public List<Book>? Books { get; set; }
    }
}
