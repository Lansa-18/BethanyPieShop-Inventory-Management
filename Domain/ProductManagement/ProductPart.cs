using System;

namespace BethanyPieShop.InventoryManagement.Domain.ProductManagement;

public partial class Product
{
    private void UpdateLowStock()
    {
        if (AmountInStock < 10) // Using a fixed value for now
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
