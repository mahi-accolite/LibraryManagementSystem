﻿using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using System.Net.Http.Headers;

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
            if(_bookRepository.FindBook(id)?.Id is null)            
            _bookRepository.AddBook(new Book(id, title, author));
        }

        public void RemoveBook(string id)
        {
            _bookRepository.RemoveBook(id);
        }

        public bool CheckOutBook(string id)
        {
            var book = _bookRepository.FindBook(id);
            book?.CheckOut();
            return book.IsCheckedOut;
        }
        public Book? FindBook(string id)
        {
            return _bookRepository.FindBook(id);
        }
        public bool ReturnBook(string id)
        {
            var book = _bookRepository.FindBook(id);
            book?.Return();
            return book is not null? true: false;
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