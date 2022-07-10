using System.Text.Json.Serialization;

namespace ProductsTask.SharedKernal.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductStatus
    {
        Sold,
        InStock,
        Damaged
    }
}
