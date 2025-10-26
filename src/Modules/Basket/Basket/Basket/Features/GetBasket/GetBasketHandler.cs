

namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCartDto ShoppingCart);
public class GetBasketHandler(BasketDbContext dbContext) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // Get basket with UserName
        var basket = await dbContext.ShoppingCarts
            .AsNoTracking()
            .Include(x=>x.Items)
            .SingleOrDefaultAsync(x=>x.UserName == query.UserName);

        if (basket is null)
            throw new BasketNotFoundException(query.UserName);

        //mapping Basket entity to shoppingCartDto
        var basketDto = basket.Adapt<ShoppingCartDto>();
        return new GetBasketResult(basketDto);

    }
}
