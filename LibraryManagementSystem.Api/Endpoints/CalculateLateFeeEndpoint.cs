using FastEndpoints;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class CalculateLateFeeEndpoint : Endpoint<RequestId, ResponseResult<double>>
    {
        private readonly IBookService _bookService;

        public CalculateLateFeeEndpoint(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/latefee/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            // I know this is just an exercise, but should the book service not also use async/await?
            var fine = _bookService.CalculateLateFees(req.Id);
            await SendAsync(new ResponseResult<double> {Data = fine, Message = "Check Late fee Amount is"+ fine});
        }
    }
}
