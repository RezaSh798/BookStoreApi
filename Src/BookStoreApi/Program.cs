using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("BookStoreDB"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BookStoreApi", Version = "v1" });
});

var app = builder.Build();

// Configure middleware
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreApi v1");
});

app.MapControllers();
app.Run();

public partial class Program {}