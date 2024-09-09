using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.Extensions.Hosting;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
// Add services to the container.
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();


// Use FastEndpoints
app.UseFastEndpoints();


app.Run();