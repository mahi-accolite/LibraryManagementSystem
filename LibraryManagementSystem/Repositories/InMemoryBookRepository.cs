using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBook(string id)
        {
            var book = FindBook(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }

        public Book? FindBook(string id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public IEnumerable<Book> GetCheckedOutBooks()
        {
            return _books.Where(b => b.IsCheckedOut);
        }
    }
}