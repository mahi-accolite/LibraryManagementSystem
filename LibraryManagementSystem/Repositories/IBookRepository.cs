using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        void RemoveBook(string id);
        Book? FindBook(string id);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetCheckedOutBooks();
    }
}