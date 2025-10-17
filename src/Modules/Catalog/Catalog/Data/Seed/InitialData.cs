namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
    [
        Product.Create(new Guid("a3b4c5d6-e7f8-4a9b-bc1d-2e3f4a5b6c7d"),
                "Samsung S25 Ultra",
                ["Smartphone"],
                "Flagship smartphone with 200MP camera, 8K video recording, and foldable OLED display",
                "samsung-s25ultra.png",
                1199.99M),
           
        Product.Create(new Guid("b1a2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
               "iPhone 15 Pro Max",
                ["Smartphone"],
                "Apple premium smartphone with A17 Pro chip, titanium frame, and 5x optical zoom",
                "iphone-15promax.png",
                1299.00M),

        Product.Create(new Guid("e4f5a6b7-c8d9-4e0f-1a2b-3c4d5e6f7a8b"),
                "Apple MacBook Pro 16",
                ["Laptop"],
                "Professional laptop with M3 Pro chip, 32GB RAM, and 1TB SSD storage",
                "macbook-pro-16.png",
                2499.00M),
        
        Product.Create( new Guid("c8d9e0f1-a2b3-4c4d-5e6f-7a8b9c0d1e2f"),
                "Sony WH-1000XM5",
                ["Headphones"],
                "Premium noise-cancelling wireless headphones with 30-hour battery life",
                "sony-headphones.png",
                349.99M),
            Product.Create( new Guid("e0f1a2b3-c4d5-4e6f-7a8b-9c0d1e2f3a4b"),
                "Nintendo Switch OLED",
                ["Gaming Console"],
                "Gaming console with 7-inch OLED screen and 64GB internal storage",
                "switch-oled.png",
                349.99M),
            Product.Create(
                new Guid("b3c4d5e6-f7a8-4b9c-0d1e-2f3a4b5c6d7e"),
                "Canon EOS R5",
                ["Camera"],
                "Mirrorless camera with 45MP sensor and 8K video capability",
                "canon-eos-r5.png",
                3899.00M) 
    ];
}
