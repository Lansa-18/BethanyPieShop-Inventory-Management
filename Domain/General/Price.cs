using System;

namespace BethanyPieShop.InventoryManagement.Domain.General;

public class Price
{
    public double ItemPrice { set; get; }
    public Currency Currency { get; set; }

    public override string ToString()
    {
        return $"{ItemPrice} {Currency}";
    }
}
