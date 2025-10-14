using Catalog.Products.Events;

namespace Catalog.Products.Models;
public class Product : Aggregate<Guid>
{
    public string Name { get; set; } = default!;
    public List<string> Categories { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
    public static Product Create(string name, List<string> categories, string description, string imageFile, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product
        {
            Name = name,
            Categories = categories,
            Description = description,
            ImageFile = imageFile,
            Price = price
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public void Update(string name, List<string> categories, string description, string imageFile, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        Name= name;
        Categories= categories;
        Description= description;
        ImageFile= imageFile;
        Price= price;

        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }
    }
}

