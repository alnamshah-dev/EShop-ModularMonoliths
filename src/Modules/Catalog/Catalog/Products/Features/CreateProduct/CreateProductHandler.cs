
namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto productDto):ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.productDto);
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            Guid.NewGuid(), 
            productDto.Name, 
            productDto.Category, 
            productDto.Description, 
            productDto.ImageFile, 
            productDto.Price
            );
        return product;
    }
}
