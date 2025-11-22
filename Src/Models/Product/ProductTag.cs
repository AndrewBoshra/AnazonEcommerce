namespace Anazon.Models;


public class ProductTag
{

    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    
    public string Tag { get; set; } = default!;
}