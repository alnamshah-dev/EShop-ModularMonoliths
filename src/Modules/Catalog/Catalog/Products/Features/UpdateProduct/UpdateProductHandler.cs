
namespace Catalog.Products.Features.UpdateProduct;
public record UpdateProductCommand(ProductDto productDto) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.productDto.Id],cancellationToken:cancellationToken);
        if (product is null)
            throw new Exception($"Product not found :{command.productDto.Id}");

        UpdateProductWithNewValue(product,command.productDto);

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);


    }

    private void UpdateProductWithNewValue(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price
            );
    }
}
