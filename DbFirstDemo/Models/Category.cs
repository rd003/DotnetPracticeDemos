namespace DbFirstDemo.Models;

public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = [];
}