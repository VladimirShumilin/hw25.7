using hw25.DAL.Model;
using System.Security.Cryptography.X509Certificates;

namespace hw25.DAL.Repositories
{
    public static class BookRepository
    {
        //выбор объекта из БД по его идентификатору
        public static Book? GetBook(int id)
        {
            using (var db = new AppContextDb())
            return db.Books.FirstOrDefault(u => u.BookID == id);
        }
        //выбор всех объектов
        public static IEnumerable<Book> GetAllBooks()
        {
            using (var db = new AppContextDb())
            return db.Books.ToList();        
        }
        //добавление объекта в БД 
        public static void AddBook(string name,  int yearOfIssue)
        {
            using (var db = new AppContextDb())
           
                try
                {
                    Book book = new Book { Name = name, YearOfIssue = yearOfIssue };

                    db.Books.Add(book);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            
        }
        //удаление из БД
        public static void DeleteBook(int id)
        {
            using (var db = new AppContextDb())
            
                try
                {
                    if (db.Books.FirstOrDefault(u => u.BookID == id) is Book book)
                    {
                        db.Books.Remove(book);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                } 
        }
        //обновление года выпуска книги (по Id)
        public static bool UpdateYearOfIssue(int id, int newYear)
        {
            using (var db = new AppContextDb())
                try
                {
                    if (db.Books.FirstOrDefault(u => u.BookID == id) is Book book)
                    {
                        book.YearOfIssue = newYear;
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            return false;
        }
        //Получать список книг определенного жанра и вышедших между определенными годами.
        public static List<Book>? GetBooks(string genre ,int yearStart,int yearEnd)
        {
            using (var db = new AppContextDb())
                return db.Books.Where(
                    x=>x.Genre == genre &&
                    x.YearOfIssue >= yearStart && x.YearOfIssue <= yearEnd).ToList();
        }
        //Получать количество книг определенного автора в библиотеке.
        public static List<Book>? GetBooksByAuthor(string fullName)
        {
            using (var db = new AppContextDb())
            {
                if (db.Authors.FirstOrDefault(x => x.FullName.Equals(fullName)) is Author author)
                    return db.Books.Where(x => x.AuthorID == author.AuthorID).ToList();
                else
                    return null;
            }
        }
        //Получать количество книг определенного жанра в библиотеке.
        public static List<Book>? GetBooksByGenre(string genre)
        {
            using (var db = new AppContextDb())
                return db.Books.Where(
                    x => x.Genre == genre).ToList();
        }
        //Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        public static bool Exist(string authorFullName, string bookName)
        {
            using (var db = new AppContextDb())
            {
                var result = from b in db.Books
                             join a in db.Authors on b.AuthorID equals a.AuthorID
                             where a.FullName == authorFullName &&
                                   b.Name == bookName
                             select  b;

                return result.Any();
            }
        }

        //Получение последней вышедшей книги.
        public static Book? GetLastIssueBooks()
        {
            using (var db = new AppContextDb())
                return db.Books.OrderByDescending(x => x.YearOfIssue).ToList()?.First();
        }

        //Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        public static List<string>? GetListOfAllBooks()
        {
            using (var db = new AppContextDb())
                return (from b in db.Books
                       orderby b.Name
                       select new string(b.Name))?.ToList();
                       
        }

        //Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        public static List<string>? GetListOfAllBooksOrderByIssueDate()
        {
            using (var db = new AppContextDb())
                return (from b in db.Books
                        orderby b.YearOfIssue descending
                        select new string(b.Name))?.ToList();

        }

        
    }
}
