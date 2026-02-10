using System;
using System.Text;
using BethanyPieShop.InventoryManagement.Domain.General;

namespace BethanyPieShop.InventoryManagement.Domain.ProductManagement;

public partial class Product
{
    private int id;
    private string name = string.Empty;
    private string? description;

    private int maxItemInStock = 0;

    public Product(int id) : this(id, string.Empty) { }

    public Product(int Id, string name)
    {
        this.Id = Id;
        this.name = name;
    }

    public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        UnitType = unitType;
        maxItemInStock = maxAmountInStock;

        UpdateLowStock();
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set
        {
            name = value.Length > 50 ? value[..50] : value;
        }
    }

    public string? Description
    {
        get { return description; }
        set
        {
            if (value == null)
            {
                description = string.Empty;
            }
            else
            {
                description = value.Length > 250 ? value[..250] : value;
            }
        }
    }

    public UnitType UnitType { get; set; }
    public int AmountInStock { get; private set; }
    public bool IsBelowStockThreshold { get; private set; }

    public Price Price { get; set; }

    public void UseProduct(int items)
    {
        if (items <= AmountInStock)
        {
            // Use the item
            AmountInStock -= items;

            UpdateLowStock();

            Log($"Amount in stack updated. Now {AmountInStock} items in stock");
        }
        else
        {
            Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested");
        }
    }

    public void IncreaseStock()
    {
        AmountInStock++;
    }

    public void IncreaseStock(int amount)
    {
        int newStock = AmountInStock + amount;

        if (newStock <= maxItemInStock)
        {
            AmountInStock += amount;
        }
        else
        {
            AmountInStock = maxItemInStock; // We only store the possible items, overstock isn't stored.
            Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
        }

        if (AmountInStock > StockThreshold)
        {
            IsBelowStockThreshold = false;
        }
    }

    private void DecreaseStock(int items, string reason)
    {
        if (items <= AmountInStock)
        {
            // decrease the stock with the specified number of items
            AmountInStock -= items;
        }
        else
        {
            AmountInStock = 0;
        }

        UpdateLowStock();

        Log(reason);
    }

    public string DisplayDetailsShort()
    {
        return $"{id}. {name} \n{AmountInStock} items in stock";
    }

    public string DisplayDetailsFull()
    {
        return DisplayDetailsFull("");
    }

    public string DisplayDetailsFull(string extraDetails)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

        sb.Append(extraDetails);

        if (IsBelowStockThreshold)
        {
            sb.Append("\n!!STOCK LOW!!");
        }

        return sb.ToString();
    }


}
