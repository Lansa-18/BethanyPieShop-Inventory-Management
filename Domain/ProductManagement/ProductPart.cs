using System;
using System.Net;

namespace BethanyPieShop.InventoryManagement.Domain.ProductManagement;

public partial class Product
{
    public static int StockThreshold = 5;

    public static void ChangeStockThreshold(int newStockThreshold)
    {
        // We will only allow this to go through if the value is >= 0
        if (newStockThreshold > 0)
        {
            StockThreshold = newStockThreshold;
        }
    }
    public void UpdateLowStock()
    {
        if (AmountInStock < StockThreshold)
        {
            IsBelowStockThreshold = true;
        }
    }

    private void Log(string message)
    {
        // This could be written into a file.
        Console.WriteLine(message);
    }

    private string CreateSimpleProductRepresentation()
    {
        return $"Product {id} ({name})";
    }
}
