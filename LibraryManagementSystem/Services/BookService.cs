using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository libraryRepository)
        {
            _bookRepository = libraryRepository;
        }

        public void AddBook(string id, string title, string author)
        {
            var book = new Book(id, title, author);
            _bookRepository.AddBook(book);
        }

        public void RemoveBook(string id)
        {
            _bookRepository.RemoveBook(id);
        }

        public void CheckOutBook(string id)
        {
            var book = _bookRepository.FindBook(id);
            book?.CheckOut();
        }
        public Book? FindBook(string id)
        {
            return _bookRepository.FindBook(id);
        }
        public void ReturnBook(string id)
        {
            var book = _bookRepository.FindBook(id);
            book?.Return();
        }

        public IEnumerable<Book> GetCheckedOutBooks()
        {
            return _bookRepository.GetCheckedOutBooks();
        }

        public double CalculateLateFees(string id)
        {
            var book = _bookRepository.FindBook(id);
            return book?.CalculateLateFee() ?? 0;
        }
    }
}