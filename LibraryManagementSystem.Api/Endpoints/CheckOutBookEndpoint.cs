using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System.Reflection.Metadata;

namespace LibraryManagementSystem.Endpoints
{
    public class CheckOutBookEndpoint : Endpoint<RequestId, ResponseResult<Book>>
    {
        private readonly IBookService _bookService;

        public CheckOutBookEndpoint(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override void Configure()
        {
            Get("/books/checkout/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            var checkedOutBook = _bookService.CheckOutBook(req.Id);
            await SendAsync(new ResponseResult<Book> {Data = checkedOutBook });
        }
    }
}