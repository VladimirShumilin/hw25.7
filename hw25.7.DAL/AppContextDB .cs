
using hw25.DAL.Converters;
using hw25.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw25.DAL
{
    public class AppContextDb : DbContext
    {
        // Объекты таблицы Users
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<IssuedBook> IssuedBooks  { get; set; }
        public DbSet<Author> Authors { get; set; }

        public AppContextDb()
        {
           
        }
        public void DbFirstInit()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            #region Наполнить бд значениями для тестирования 
            var user1 = new User { Name = "Arthur", Email = "asd@gmail.com" };
            var user2 = new User { Name = "Bob", Email = "dddd@gmail.com" };
            Users.AddRange(user1, user2);


            var author1 = new Author { FullName = "Оскар Уайльд" };
            var author2 = new Author { FullName = "Оруэлл Джорж" };
            var author3 = new Author { FullName = "Алигьери Данте" };
            Authors.AddRange(author1, author2, author3);


            var book1 = new Book { Name = "Портрет Дориана Грея", Genre = "Классическая проза", Author = author1, YearOfIssue = 2020 };
            var book2 = new Book { Name = "1984. Скотный двор", Genre = "Классическая проза", Author = author2, YearOfIssue = 2019 };
            var book3 = new Book { Name = "Саломея", Genre = "Классическая проза", Author = author1, YearOfIssue = 2021 };
            var book4 = new Book { Name = "Божественная Комедия ", Genre = "Классическая поэзия", Author = author3, YearOfIssue = 2022 };
            Books.AddRange(book1, book2, book3, book4);

            SaveChanges();
            #endregion
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID);


            modelBuilder.Entity<IssuedBook>()
                .HasOne(b => b.User)
                .WithMany(u => u.IssuedBooks)
                .HasForeignKey(b => b.UserId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=172.16.10.33;Database=LinqTesting;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
