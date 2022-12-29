using hw25.DAL;
using hw25.DAL.Model;
using hw25.DAL.Repositories;

using (var db = new AppContextDb())
{
    db.DbFirstInit();
    //получить пользователя по ID
    if (UserRepository.GetUser(1) is not User u)
    {
        Console.WriteLine("В БД нет данных для работы");
        return;
    }

    //получить книгу по ID
    if (BookRepository.GetBook(1) is not Book b)
    {
        Console.WriteLine("В БД нет данных для работы");
        return;
    }

    //выдать книгу
    UserRepository.GiveBookToUser(b, u);
   
    //Получать список книг определенного жанра и вышедших между определенными годами.
    var books = BookRepository.GetBooks("Классическая проза", 2019, 2022);

    //Получать количество книг определенного автора в библиотеке.
    books = BookRepository.GetBooksByAuthor("Оскар Уайльд");
    //books = AuthorRepository.GetBooks("Оскар Уайльд");

    //Получать количество книг определенного жанра в библиотеке.
    books = BookRepository.GetBooksByGenre("Классическая проза");

    //Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
    bool result = BookRepository.Exist("Оскар Уайльд", "Портрет Дориана Грея");

    //Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
    result = UserRepository.BookExist(b,u);

    //Получать количество книг на руках у пользователя.
    int bookCount = UserRepository.GetBooksCount(u);

    //Получение последней вышедшей книги.
    var book = BookRepository.GetLastIssueBooks();

    //Получение списка всех книг, отсортированного в алфавитном порядке по названию.
    var listOfAllBooks = BookRepository.GetListOfAllBooks();

    //Получение списка всех книг, отсортированного в порядке убывания года их выхода.
    listOfAllBooks = BookRepository.GetListOfAllBooksOrderByIssueDate();
    ;
}
