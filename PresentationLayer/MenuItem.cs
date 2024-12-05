using Spectre.Console;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public class MenuItem
{
    public string Category { get; set; }
    public string Dish { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }

    public MenuItem(string category, string dish, string description, string price)
    {
        Category = category;
        Dish = dish;
        Description = description;
        Price = price;
    }
}