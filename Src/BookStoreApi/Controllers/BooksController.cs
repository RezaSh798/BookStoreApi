using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> FindOne(int id)
    {
        Book? book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        return book;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> FindMany()
    {
        return await _context.Books.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(FindOne), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Book updatedBook)
    {
        Book? book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        updatedBook.Id = id;
        _context.Entry(book).CurrentValues.SetValues(updatedBook);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        Book? book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}