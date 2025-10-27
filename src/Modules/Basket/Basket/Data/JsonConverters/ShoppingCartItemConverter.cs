using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Data.JsonConverters;

public class ShoppingCartItemConverter : JsonConverter<ShoppingCartItem>
{
    public override ShoppingCartItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var jsonElement = jsonDocument.RootElement;

        var id = jsonElement.GetProperty("id").GetGuid();
        var shoppingCartId = jsonElement.GetProperty("shoppingCartId").GetGuid();
        var productId = jsonElement.GetProperty("productId").GetGuid();
        var quantity = jsonElement.GetProperty("quantity").GetInt32();
        var color = jsonElement.GetProperty("color").GetString();
        var price = jsonElement.GetProperty("price").GetDecimal();
        var productName = jsonElement.GetProperty("productName").GetString();

        return new ShoppingCartItem(id, shoppingCartId, productId,quantity,color,price,productName);

    }

    public override void Write(Utf8JsonWriter writer, ShoppingCartItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id",value.Id.ToString());
        writer.WriteString("shoppingCartId", value.ShoppingCartId.ToString());
        writer.WriteString("productId", value.ProductId.ToString());
        writer.WriteNumber("quantity", value.Quantity);
        writer.WriteString("color", value.Color);
        writer.WriteNumber("price", value.Price);
        writer.WriteString("productName", value.ProductName);
        
        writer.WriteEndObject();
    }
}
