using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        void AddBook(string id, string title, string author);
        Book RemoveBook(string id);
        bool CheckOutBook(string id);
        Book ReturnBook(string id);
        IEnumerable<Book> GetCheckedOutBooks();
        IEnumerable<Book> GetAllBooks();
        double CalculateLateFees(string id);
        Book? FindBook(string id);
    }
}