using FastEndpoints;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class DeleteBookEndPoint : Endpoint<Book, ResponseResult<Book>>
    {
        private readonly IBookService _bookService;
        public DeleteBookEndPoint(IBookService bookService)
        {
            _bookService = bookService;
        }
        public override void Configure()
        {
            Delete("/books/{id}");
            AllowAnonymous();
        }
        public override async Task HandleAsync(Book req, CancellationToken ct)
        {
            var book = _bookService.RemoveBook(req.Id);
            await SendAsync(new ResponseResult<Book> {Data = book, Message = "Book Deleted successfully!" });
        }
    }
}
