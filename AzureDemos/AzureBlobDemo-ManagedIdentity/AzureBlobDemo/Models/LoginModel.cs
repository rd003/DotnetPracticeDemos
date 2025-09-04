using System.ComponentModel.DataAnnotations;

namespace AzureBlobDemo.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; } = null!;
    
    [Required] 
    public string Password { get; set; } = null!;
    
    public List<string> Errors { get; set; } = [];
}
