namespace Ordering.Orders.Features.CreateOrder;

public record CreateOrderCommand(OrderDto Order): ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is required");
    }
}
public class CreateOrderHandler(OrderingDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id);
    }

    private Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(order.ShippingAddress.FirstName,order.ShippingAddress.LastName,order.ShippingAddress.EmailAddress,order.ShippingAddress.AddressLine,order.ShippingAddress.Country,order.ShippingAddress.State,order.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(order.BillingAddress.FirstName,order.BillingAddress.LastName,order.BillingAddress.EmailAddress,order.BillingAddress.AddressLine,order.BillingAddress.Country,order.BillingAddress.State,order.BillingAddress.ZipCode);

        var newOrder = Order.Create
            (
                id:Guid.NewGuid(),
                customerId: order.CustomerId,
                orderName: $"{order.OrderName}_{new Random().Next()}",
                shippingAddress:shippingAddress,
                billingAddress:billingAddress,
                payment: Payment.Of(order.Payment.CardName,order.Payment.CardNumber,order.Payment.Expiration,order.Payment.Cvv,order.Payment.PaymentMethod)
            );
        order.Items.ForEach(item =>
        {
            newOrder.Add(
                item.ProductId, 
                item.Quantity, 
                item.Price);
        });

        return newOrder;
    }
}
