using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoFullStack.Api.Models;

namespace TodoFullStack.Api.Controllers;

[Route("api/todos")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return CreatedAtRoute("GetTodo", new { id = todo.Id }, todo);
    }

    [HttpGet("{id:guid}",Name = "GetTodo")]
    public async Task<IActionResult> GetTodo(Guid id)
    {
        var todo = await _context.Todos.AsNoTracking().SingleOrDefaultAsync(a=>a.Id==id);
        return Ok(todo);
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        return Ok(await _context.Todos.AsNoTracking().ToListAsync());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        var todo = await _context.Todos.AsNoTracking().SingleAsync(a => a.Id == id);
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodo(Todo todoToUpdate)
    {
        _context.Todos.Update(todoToUpdate);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
