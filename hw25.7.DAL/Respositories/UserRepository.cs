using hw25.DAL.Model;
using System;


namespace hw25.DAL.Repositories
{
    public static class UserRepository
    {
        //выбор объекта из БД по его идентификатору
        public static User? GetUser(int id)
        {
            using (var db = new AppContextDb())
            return db.Users.FirstOrDefault(u => u.UserID == id);
        }
        //выбор всех объектов
        public static IEnumerable<User> GetAllUsers()
        {
            using (var db = new AppContextDb())
            return db.Users.ToList();        
        }
        //добавление объекта в БД 
        public static void AddUser(string name,  string email)
        {
            using (var db = new AppContextDb())
           
                try
                {
                    User User = new User { Name = name, Email = email };

                    db.Users.Add(User);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            
        }
        //удаление из БД
        public static void DeleteUser(int id)
        {
            using (var db = new AppContextDb())
            
                try
                {
                    if (db.Users.FirstOrDefault(u => u.UserID == id) is User user)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                } 
        }
        //обновление имени пользователя (по Id)
        public static bool UpdateUserName(int id, string name)
        {
            using (var db = new AppContextDb())
                try
                {
                    if (db.Users.FirstOrDefault(u => u.UserID == id) is User user)
                    {
                        user.Name = name;
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
         //Выдать книгу.
        public static void GiveBookToUser(Book book, User user)
        {
            using (var db = new AppContextDb())
            {
                db.IssuedBooks.Add(new IssuedBook() { BookId = book.BookID, UserId = user.UserID});
                db.SaveChanges();
            }
        }
        //Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        public static bool BookExist(Book book, User user)
        {
            using (var db = new AppContextDb())
               return db.IssuedBooks.Where(ib=>ib.UserId == user.UserID && ib.BookId == book.BookID).Any();
            
        }

        //Получать количество книг на руках у пользователя
        public static int GetBooksCount(User user)
        {
            using (var db = new AppContextDb())
                return db.IssuedBooks.Where(ib => ib.UserId == user.UserID).Count();
            
        }

    }
}
