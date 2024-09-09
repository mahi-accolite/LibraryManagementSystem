using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Endpoints
{
    public class CheckOutBookEndpoint : Endpoint<CheckOutBookRequest, CheckOutBookResponse>
    {
        private readonly IBookService _bookService;

        public CheckOutBookEndpoint(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Post("/books/checkout");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CheckOutBookRequest req, CancellationToken ct)
        {
            _bookService.CheckOutBook(req.Id);
            await SendAsync(new CheckOutBookResponse { Message = "Book checked out successfully!" });
        }
    }
}