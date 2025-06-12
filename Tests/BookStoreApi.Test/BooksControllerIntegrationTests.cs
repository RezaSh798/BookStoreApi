using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using BookStoreApi; 
using BookStoreApi.Models;
using FluentAssertions;
using Xunit;

namespace BooksStoreApi.Test;

[Collection("IntegrationTests")]
public class BooksControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BooksControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_AllBooks_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/v1/books");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        books.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task POST_CreatesBook_ReturnsCreated()
    {
        var newBook = new Book { Title = "Test Book", Author = "Test Author" };
        
        var response = await _client.PostAsJsonAsync("/api/v1/books", newBook);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdBook = await response.Content.ReadFromJsonAsync<Book>();
        createdBook.Should().NotBeNull();
        createdBook!.Title.Should().Be(newBook.Title);
    }
}