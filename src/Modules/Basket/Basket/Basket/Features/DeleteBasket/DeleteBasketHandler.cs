namespace Basket.Basket.Features.DeleteBasket;
public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class DeleteBasketHandler(BasketDbContext dbContext) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        // Delete Basket entity from command object
        // Save to database
        // return result
        var basket =await dbContext.ShoppingCarts.SingleOrDefaultAsync(x=>x.UserName == command.UserName);
        
        if (basket is null)
            throw new BasketNotFoundException(command.UserName);

        dbContext.ShoppingCarts.Remove(basket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteBasketResult(true);
    }
}
