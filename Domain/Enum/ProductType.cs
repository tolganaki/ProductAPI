using System.ComponentModel;

namespace ProductAPI.Domain.Enum
{
    public enum ProductType : byte
    {
        [Description("Product Type 1")]
        ProductType1 = 1,

        [Description("Product Type 2")]
        ProductType2 = 2,

        [Description("Product Type 3")]
        ProductType3 = 3
    }
}