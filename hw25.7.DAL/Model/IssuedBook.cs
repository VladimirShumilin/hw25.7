namespace hw25.DAL.Model
{
    public class IssuedBook
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
