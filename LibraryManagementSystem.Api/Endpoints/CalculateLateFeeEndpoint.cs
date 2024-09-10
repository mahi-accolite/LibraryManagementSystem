using FastEndpoints;
using LibraryManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class CalculateLateFeeEndpoint : Endpoint<RequestId, ResponseResult>
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
            _bookService.CalculateLateFees(req.Id);
            await SendAsync(new ResponseResult { Message = "Check Late fee Amount" });
        }
    }
}
