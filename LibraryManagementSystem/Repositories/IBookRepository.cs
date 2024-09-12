using LibraryManagementSystem.Models;

//namespace should reflect its physical location
namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        Book RemoveBook(string id);
        Book? FindBook(string id);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetCheckedOutBooks();
    }
}