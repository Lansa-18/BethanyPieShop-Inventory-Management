using System;

namespace BethanyPieShop.InventoryManagement.Domain.General;

public class Price
{
    public double ItemPrice { set; get; }
    public Currency Currency { get; set; }

    public Price(double price, Currency currency) {
        ItemPrice = price;
        Currency = currency;
    }

    public override string ToString()
    {
        return $"{ItemPrice} {Currency}";
    }
}
