using Xunit;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Tests
{
    public class InMemoryBookRepositoryTests
    {
        [Fact]
        public void AddBook_ShouldAddBookToList()
        {
            // Arrange
            var repository = new InMemoryBookRepository();
            var book = new Book("1", "2 States", "Chetan Bhagat");

            // Act
            repository.AddBook(book);

            // Assert
            var allBooks = repository.GetAllBooks();
            Assert.Contains(book, allBooks);
        }

        [Fact]
        public void RemoveBook_ShouldRemoveBookFromList()
        {
            // Arrange
            var repository = new InMemoryBookRepository();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            repository.AddBook(book);

            // Act
            var removedBook = repository.RemoveBook(book.Id);

            // Assert
            Assert.NotNull(removedBook);
            Assert.Equal(book, removedBook);
            Assert.DoesNotContain(book, repository.GetAllBooks());
        }

        [Fact]
        public void FindBook_ShouldReturnBookWhenExists()
        {
            // Arrange
            var repository = new InMemoryBookRepository();
            var book = new Book("1", "2 States", "Chetan Bhagat");
            repository.AddBook(book);

            // Act
            var result = repository.FindBook(book.Id);

            // Assert
            Assert.Same(book, result);
        }

        [Fact]
        public void FindBook_ShouldReturnNullWhenNotExists()
        {
            // Arrange
            var repository = new InMemoryBookRepository();

            // Act
            var result = repository.FindBook("1");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooksInList()
        {
            // Arrange
            var repository = new InMemoryBookRepository();
            var book1 = new Book("1", "2 States", "Chetan Bhagat");
            var book2 = new Book("2", "The Phonix", "SF");
            repository.AddBook(book1);
            repository.AddBook(book2);

            // Act
            var result = repository.GetAllBooks();

            // Assert
            Assert.Equal(new[] { book1, book2 }, result.ToArray());
        }

        [Fact]
        public void GetCheckedOutBooks_ShouldReturnOnlyCheckedOutBooks()
        {
            // Arrange
            var repository = new InMemoryBookRepository();
            var book1 = new Book("1", "2 States", "Chetan Bhagat");
            var book2 = new Book("2", "The Phonix", "SF");
            book1.CheckOut(); // Assuming CheckOut sets IsCheckedOut to true
            repository.AddBook(book1);
            repository.AddBook(book2);

            // Act
            var result = repository.GetCheckedOutBooks();

            // Assert
            Assert.Contains(book1, result);
            Assert.DoesNotContain(book2, result);
        }
    }
}