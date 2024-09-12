using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class AddBookEndpoint : Endpoint<Book, ResponseResult<Book>>
    {
        private readonly IBookService _bookService;

        public AddBookEndpoint(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override void Configure()
        {
            Post("/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Book req, CancellationToken ct)
        {
            // why are we not passing the entire object to the service.  what if I add 5 more properites to 
            // the book object?  we now have to add 5 more arguments to an object that already has these
            // nicely contained.
            _bookService.AddBook(req.Id, req.Title, req.Author);
            await SendAsync(new ResponseResult<Book> { Message = "Book added successfully!" });
        }
    }
}