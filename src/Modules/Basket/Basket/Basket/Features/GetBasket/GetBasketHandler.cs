
namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCartDto ShoppingCart);
public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // Get basket with UserName
        var basket = await repository.GetBasket(query.UserName, true, cancellationToken);

        //mapping Basket entity to shoppingCartDto
        var basketDto = basket.Adapt<ShoppingCartDto>();
        return new GetBasketResult(basketDto);

    }
}
