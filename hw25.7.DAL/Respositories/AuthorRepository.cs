using hw25.DAL.Model;

namespace hw25.DAL.Repositories
{
    public static class AuthorRepository
    {
        //выбор объекта из БД по его идентификатору
        public static Author? GetAuthor(int id)
        {
            using (var db = new AppContextDb())
            return db.Authors.FirstOrDefault(u => u.AuthorID == id);
        }
        //выбор всех объектов
        public static IEnumerable<Author> GetAllAuthors()
        {
            using (var db = new AppContextDb())
            return db.Authors.ToList();        
        }
        //добавление объекта в БД 
        public static void AddAuthor(string fullName)
        {
            using (var db = new AppContextDb())
           
                try
                {
                    Author Author = new Author { FullName = fullName };

                    db.Authors.Add(Author);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            
        }
        //удаление из БД
        public static void DeleteAuthor(int id)
        {
            using (var db = new AppContextDb())
            
                try
                {
                    if (db.Authors.FirstOrDefault(u => u.AuthorID == id) is Author Author)
                    {
                        db.Authors.Remove(Author);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                } 
        }
        //Получать количество книг определенного автора в библиотеке.
        public static List<Book>? GetBooks(string fullName)
        {
            using (var db = new AppContextDb())
            {
                var gg = db.Authors.FirstOrDefault(x => x.FullName.Equals(fullName));
                return db.Authors.FirstOrDefault(x => x.FullName.Equals(fullName))?.Books;
            }
        }
       
    }
}
