﻿
namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCart):ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x=>x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class CreateBasketHandler(BasketDbContext dbContext) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        // Create Basket entity from command object
        // Save to database
        // return result

        var shoppingCart = CreateNewBasket(command.ShoppingCart);

        dbContext.ShoppingCarts.Add(shoppingCart);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBasketResult(shoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCart)
    {
        // Create new basket
        var newBasket =  ShoppingCart.Create(
            Guid.NewGuid(),
            shoppingCart.UserName
            );

        shoppingCart.Items.ForEach(item =>
        {
            newBasket.AddItem(
                    item.ProductId,
                    item.Quantity,
                    item.Color,
                    item.Price,
                    item.ProductName
                );
        });
        return newBasket;
    }
}
