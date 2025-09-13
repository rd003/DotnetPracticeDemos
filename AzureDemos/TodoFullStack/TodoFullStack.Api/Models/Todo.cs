using System.ComponentModel.DataAnnotations;

namespace TodoFullStack.Api.Models;
public class Todo
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Required]
    public string Title { get; set; } = null!;

    public bool Completed { get; set; } = false;
}