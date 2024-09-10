using global::LibraryManagementSystem.Models;
using global::LibraryManagementSystem.Repositories;
using global::LibraryManagementSystem.Services;
using Moq;
using Xunit;

namespace LibraryManagementSystem.Tests
{
    public class BookServiceTests
    {

        [Fact]
        public void AddBook_ShouldAddNewBook_WhenBookDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(r => r.FindBook(It.IsAny<string>())).Returns((Book)null);
            var service = new BookService(mockRepository.Object);

            // Act
            service.AddBook("1", "2 States", "Chetan Bhagat");

            // Assert
            mockRepository.Verify(r => r.AddBook(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public void RemoveBook_ShouldCallRemoveBookOnRepository()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var service = new BookService(mockRepository.Object);

            // Act
            service.RemoveBook("1");

            // Assert
            mockRepository.Verify(r => r.RemoveBook("1"), Times.Once);
        }

        [Fact]
        public void CheckOutBook_ShouldCallCheckOutOnBook()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            var service = new BookService(mockRepository.Object);

            // Act
            service.CheckOutBook("1");

            // Assert
            Assert.True(book.IsCheckedOut);
        }

        [Fact]
        public void FindBook_ShouldCallFindBookOnRepository()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            var service = new BookService(mockRepository.Object);

            // Act
            var result = service.FindBook("1");

            // Assert
            Assert.Same(book, result);
        }

        [Fact]
        public void ReturnBook_ShouldCallReturnOnBook()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            book.CheckOut();
            mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            var service = new BookService(mockRepository.Object);

            // Act
            service.ReturnBook("1");

            // Assert
            Assert.False(book.IsCheckedOut);
        }

        [Fact]
        public void GetCheckedOutBooks_ShouldCallGetCheckedOutBooksOnRepository()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var books = new List<Book>
            {
                new Book("1", "2 States", "Chetan Bhagat"),
                new Book("2", "The Phonix", "SF")
            };
            mockRepository.Setup(r => r.GetCheckedOutBooks()).Returns(books);
            var service = new BookService(mockRepository.Object);

            // Act
            var result = service.GetCheckedOutBooks();

            // Assert
            Assert.Equal(books, result);
        }

        [Fact]
        public void CalculateLateFees_ShouldCallCalculateLateFeeOnBook()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            book.CheckOut();
            book.CheckedOutDate = DateTime.Now.AddDays(-14);
            mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            var service = new BookService(mockRepository.Object);

            // Act
            var result = service.CalculateLateFees("1");

            // Assert
            Assert.Equal(7 * 2, result);
        }
        [Fact]
        public void CalculateLateFees_ShouldBookNotOverdue()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            book.CheckOut();
            book.CheckedOutDate = DateTime.Now;
            mockRepository.Setup(r => r.FindBook("1")).Returns(book);
            var service = new BookService(mockRepository.Object);

            // Act
            var result = service.CalculateLateFees("1");

            // Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var books = new List<Book>
            {
                new Book("1", "2 States", "Chetan Bhagat"),
                new Book("2", "The Phonix", "SF")
            };
            mockRepository.Setup(repo => repo.GetAllBooks()).Returns(books);
            var bookService = new BookService(mockRepository.Object);

            // Act
            var result = bookService.GetAllBooks();

            // Assert
            Assert.Equal(books.Count(), result.Count());
            Assert.Contains(books[0], result);
            Assert.Contains(books[1], result);
        }
    }
}