using FastEndpoints;
using LibraryManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Endpoints
{
    internal class FindBookEndPoint : Endpoint<RequestId, ResponseResult>
    {
        private readonly IBookService _bookService;

        public FindBookEndPoint(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            _bookService.FindBook(req.Id);
            await SendAsync(new ResponseResult { Message = "Book found successfully!" });
        }
    }
}
