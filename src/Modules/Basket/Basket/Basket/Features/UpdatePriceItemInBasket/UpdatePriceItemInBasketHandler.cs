
namespace Basket.Basket.Features.UpdatePriceItemInBasket;

public record UpdatePriceItemInBasketCommand(Guid ProductId, decimal Price):ICommand<UpdatePriceItemInBasketResult>;
public record UpdatePriceItemInBasketResult(bool IsSuccess);

public class UpdatePriceItemInBasketCommandValidator : AbstractValidator<UpdatePriceItemInBasketCommand>
{
    public UpdatePriceItemInBasketCommandValidator()
    {
        RuleFor(x=>x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}

public class UpdatePriceItemInBasketHandler(BasketDbContext dbContext) : ICommandHandler<UpdatePriceItemInBasketCommand, UpdatePriceItemInBasketResult>
{
    public async Task<UpdatePriceItemInBasketResult> Handle(UpdatePriceItemInBasketCommand command, CancellationToken cancellationToken)
    {

        var itemsToUpdate = await dbContext.ShoppingCartItems
            .Where(x => x.ProductId == command.ProductId)
            .ToListAsync(cancellationToken);

        if(!itemsToUpdate.Any())
            return new UpdatePriceItemInBasketResult(false);

        foreach(var item in itemsToUpdate)
        {
            item.UpdatePrice(command.Price);
        }

        return new UpdatePriceItemInBasketResult(true);
    }
}
