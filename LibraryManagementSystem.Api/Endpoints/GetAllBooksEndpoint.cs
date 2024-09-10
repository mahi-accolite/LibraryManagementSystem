using FastEndpoints;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class GetAllBooksEndpoint : Endpoint<RequestId, ResponseResult<IEnumerable<Book>>>
    {
        private readonly IBookService _bookService;

        public GetAllBooksEndpoint(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            var data = _bookService.GetAllBooks();
            await SendAsync(new ResponseResult<IEnumerable<Book>> { Data = data });
        }
    }
}