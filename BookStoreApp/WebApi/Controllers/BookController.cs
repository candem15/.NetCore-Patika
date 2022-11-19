using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.Operations.BookOperations.Commands;
using WebApi.Operations.BookOperations.Queries;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    readonly BookStoreDbContext _context;

    public BookController(BookStoreDbContext context)
    {
        _context = context;
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        try
        {
            UpdateBookCommand command = new UpdateBookCommand(_context, id, updatedBook);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        try
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, id);
            var item = query.Handle();
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
