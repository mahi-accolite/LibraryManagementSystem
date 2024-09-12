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

        //
        //why are we checking for null?  would it not be better of the FindBook throw an exception if
        // it does not find it?
        // every consumer of FindBook then has to check for nulls. and this would be for every repository
        // you create if you continue with this pattern
        //
        public Book RemoveBook(string id)
        {
            var book = FindBook(id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return book;

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