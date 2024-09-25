using API.Error;
using API.Mutations;
using API.Queries;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add database contexts
builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("Data")
    ));

builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Register services
builder.Services.AddTransient<ITodoService, TodoService>();

// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<TodoQuery>()
    .AddErrorFilter<ErrorFilter>() // Enable detailed error messages
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.MapGraphQL();
app.UseHttpsRedirection();
app.Run();