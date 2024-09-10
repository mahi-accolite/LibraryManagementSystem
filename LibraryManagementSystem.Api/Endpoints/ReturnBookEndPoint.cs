using FastEndpoints;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class ReturnBookEndPoint : Endpoint<RequestId, ResponseResult<bool>>
    {
        private readonly IBookService _bookService;

        public ReturnBookEndPoint(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/return/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            var returnBook = _bookService.ReturnBook(req.Id);
            await SendAsync(new ResponseResult<bool> {Data = returnBook });
        }
    }
}
