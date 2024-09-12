using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using System.Net.Http.Headers;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void AddBook(string id, string title, string author)
        {
            //so?  if the book exists you don't add the new one?
            //that is good.   but would you not want to inform the 
            if (_bookRepository.FindBook(id)?.Id is null)
                _bookRepository.AddBook(new Book(id, title, author));
        }

        public Book RemoveBook(string id)
        {
            return _bookRepository.RemoveBook(id);
        }

        public Book CheckOutBook(string id)
        {
            var book = _bookRepository.FindBook(id);
            book?.CheckOut();
            return book;
        }
        public Book? FindBook(string id)
        {
            return _bookRepository.FindBook(id);
        }
        public Book ReturnBook(string id)
        {
            var book = _bookRepository.FindBook(id);
            book?.Return();
            return book;
        }

        public IEnumerable<Book> GetCheckedOutBooks()
        {
            return _bookRepository.GetCheckedOutBooks();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }
        public double CalculateLateFees(string id)
        {
            var book = _bookRepository.FindBook(id);
            if (book is null || !book.CheckedOutDate.HasValue) return 0;
            var overdueDays = (int)(DateTime.Now - book.CheckedOutDate.Value).TotalDays - 7; // Assuming 7 days for checkout
            return overdueDays > 0 ? overdueDays * 2 : 0;
        }        
    }
}