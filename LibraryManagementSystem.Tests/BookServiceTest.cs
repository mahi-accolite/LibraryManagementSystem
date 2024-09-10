using global::LibraryManagementSystem.Models;
using global::LibraryManagementSystem.Repositories;
using global::LibraryManagementSystem.Services;
using Moq;
using System.Net;
using Xunit;

namespace LibraryManagementSystem.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockRepository.Object);
        }

        [Fact]
        public void RemoveBook_ShouldCallRemoveBookOnRepository()
        {
            // Act
            _bookService.RemoveBook("1");

            // Assert
            _mockRepository.Verify(r => r.RemoveBook("1"), Times.Once);
        }

        [Fact]
        public void CheckOutBook_ShouldCallCheckOutOnBook()
        {
            // Arrange
            var book = BookList().First();
            _mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            
            // Act
            _bookService.CheckOutBook("1");

            // Assert
            Assert.True(book.IsCheckedOut);
        }

        [Fact]
        public void FindBook_ShouldCallFindBookOnRepository()
        {
            // Arrange
            var book = BookList().First();
            _mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            
            // Act
            var result = _bookService.FindBook("1");

            // Assert
            Assert.Same(book, result);
        }

        [Fact]
        public void ReturnBook_ShouldCallReturnOnBook()
        {
            // Arrange
            var book = BookList().First();
            book.CheckOut();
            _mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            
            // Act
            _bookService.ReturnBook("1");

            // Assert
            Assert.False(book.IsCheckedOut);
        }

        [Fact]
        public void AddBook_ShouldNotAddBook_WhenBookAlreadyExists()
        {
            // Arrange            
            var existingBook = BookList().First();
            _mockRepository.Setup(repo => repo.FindBook(existingBook.Id)).Returns(existingBook); // Simulate that the book already exists
            
            // Act
            _bookService.AddBook(existingBook.Id, existingBook.Title, existingBook.Author);

            // Assert
            _mockRepository.Verify(repo => repo.AddBook(It.IsAny<Book>()), Times.Never); // Ensure AddBook is not called
        }
    

        [Fact]
        public void CheckOutBook_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.FindBook("1")).Returns((Book)null);
            
            // Act
            var result = _bookService.CheckOutBook("1");

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.FindBook("1"), Times.Once);
        }

        [Fact]
        public void AddBook_ShouldAddBook_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = "1";
            var bookTitle = "Book 1";
            var bookAuthor = "Author 1";
            _mockRepository.Setup(repo => repo.FindBook(bookId)).Returns((Book)null); // Simulate that the book does not exist
            
            // Act
            _bookService.AddBook(bookId, bookTitle, bookAuthor);

            // Assert
            _mockRepository.Verify(repo => repo.AddBook(It.Is<Book>(b =>
                b.Id == bookId &&
                b.Title == bookTitle &&
                b.Author == bookAuthor)), Times.Once);
        }

        [Fact]
        public void ReturnBook_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.FindBook("1")).Returns((Book)null);
            
            // Act
            var result = _bookService.ReturnBook("1");

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.FindBook("1"), Times.Once);
        }
        [Fact]
        public void GetCheckedOutBooks_ShouldCallGetCheckedOutBooksOnRepository()
        {
            // Arrange
            var books = BookList();
            _mockRepository.Setup(r => r.GetCheckedOutBooks()).Returns(books);
            
            // Act
            var result = _bookService.GetCheckedOutBooks();

            // Assert
            Assert.Equal(books, result);
        }

        [Fact]
        public void CalculateLateFees_ShouldReturnZero_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = "1";
            _mockRepository.Setup(repo => repo.FindBook(bookId)).Returns((Book)null); // Simulate that the book does not exist
            
            // Act
            var result = _bookService.CalculateLateFees(bookId);

            // Assert
            Assert.Equal(0, result);
            _mockRepository.Verify(repo => repo.FindBook(bookId), Times.Once);
        }
        [Fact]
        public void CalculateLateFees_ShouldCallCalculateLateFeeOnBook()
        {
            // Arrange
            var book = BookList().First();
            book.CheckOut();
            book.CheckedOutDate = DateTime.Now.AddDays(-14);
            _mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            
            // Act
            var result = _bookService.CalculateLateFees("1");

            // Assert
            Assert.Equal(7 * 2, result);
        }
        [Fact]
        public void CalculateLateFees_ShouldBookNotOverdue()
        {
            // Arrange
            var book = BookList().First();
            book.CheckOut();
            book.CheckedOutDate = DateTime.Now;
            _mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            
            // Act
            var result = _bookService.CalculateLateFees("1");

            // Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            var books = BookList();
            _mockRepository.Setup(repo => repo.GetAllBooks()).Returns(books);
            
            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.Equal(books.Count(), result.Count());
            Assert.Contains(books[0], result);
            Assert.Contains(books[1], result);
        }
        private List<Book> BookList()
        {
           return new List<Book>
            {
                new Book("1", "2 States", "Chetan Bhagat"),
                new Book("2", "The Phonix", "SF")
            };
        }
    }
}