using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        void AddBook(string id, string title, string author);
        void RemoveBook(string id);
        void CheckOutBook(string id);
        void ReturnBook(string id);
        IEnumerable<Book> GetCheckedOutBooks();
        double CalculateLateFees(string id);
        Book? FindBook(string id);
    }
}