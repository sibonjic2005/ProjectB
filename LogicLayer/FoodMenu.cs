using Spectre.Console;
using Spectre.Console.Rendering;
using System.Text.Json;
using System.Globalization;
public class FoodMenu
{
    private static string menuFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataSources/foodmenu.json"));
    public Dictionary<string, List<MenuItem>> _menuItems = new Dictionary<string, List<MenuItem>>();
    public string Category { get; set; }
    public string Dish { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }

    public FoodMenu()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        if (File.Exists(menuFilePath))
        {
            try
            {
                string json = File.ReadAllText(menuFilePath);
                _menuItems = JsonSerializer.Deserialize<Dictionary<string, List<MenuItem>>>(json) ?? new Dictionary<string, List<MenuItem>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading menu: {ex.Message}");
                _menuItems = new Dictionary<string, List<MenuItem>>();
            }
        }
        else
        {
            _menuItems = new Dictionary<string, List<MenuItem>>();
        }
    }

    public void SaveMenu()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_menuItems, options);

            string directory = Path.GetDirectoryName(menuFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(menuFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving menu: {ex.Message}");
        }
    }

    public bool AddItem(string category, string dish, string description, string price)
{
    if (_menuItems.ContainsKey(category) && _menuItems[category].Any(i => i.Dish.Equals(dish, StringComparison.OrdinalIgnoreCase)))
    {
        AnsiConsole.MarkupLine("[red]Dish already exists in this category![/]");
        return false;
    }

    string formattedPrice;

    if (price.Contains(','))
        {
            price = price.Replace(',', '.');
        }

    if (double.TryParse(price, out double price1))
    {
        formattedPrice = HandleDecimals(price1);
    }
    else
    {
        Console.WriteLine("Nice try Diddy! Pass a valid number next time!");
        Console.ReadLine();
        return false;
    }

    if (!_menuItems.ContainsKey(category))
    {
        _menuItems[category] = new List<MenuItem>();
    }

    _menuItems[category].Add(new MenuItem(category, dish, description, formattedPrice));

    SaveMenu();

    return true;
}


    public bool DeleteItem(string dishName)
    {
        foreach (var category in _menuItems.Values)
        {
            var item = category.FirstOrDefault(i => i.Dish.Equals(dishName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                category.Remove(item);
                if (category.Count == 0)
                {
                    _menuItems = _menuItems.Where(c => c.Value.Count > 0).ToDictionary(c => c.Key, c => c.Value);
                }
                SaveMenu();
                return true;
            }
        }
        return false;
    }

    public bool UpdateItem(string dishName, string? newCategory, string? newDish, string? newDescription, string? newPrice)
    {
        MenuItem? itemToUpdate = null;
        string? oldCategory = null;

        foreach (var category in _menuItems)
        {
            itemToUpdate = category.Value.FirstOrDefault(i => i.Dish.Equals(dishName, StringComparison.OrdinalIgnoreCase));
            if (itemToUpdate != null)
            {
                oldCategory = category.Key;
                break;
            }
        }

        if (!string.IsNullOrEmpty(newCategory) && !newCategory.Equals(oldCategory, StringComparison.OrdinalIgnoreCase))
        {
            _menuItems[oldCategory].Remove(itemToUpdate);

            if (_menuItems[oldCategory].Count == 0)
            {
                _menuItems.Remove(oldCategory);
            }

            if (!_menuItems.ContainsKey(newCategory))
            {
                _menuItems[newCategory] = new List<MenuItem>();
            }
            _menuItems[newCategory].Add(itemToUpdate);

            itemToUpdate.Category = newCategory;
        }

        if (newDish != null) itemToUpdate.Dish = newDish;
        if (newDescription != null) itemToUpdate.Description = newDescription;
        if (newPrice != null)
        {
            if (newPrice.Contains(','))
            {
                newPrice = newPrice.Replace(',', '.');
            }

            if (double.TryParse(newPrice, out double price))
            {
                string formattedPrice = HandleDecimals(price);
                itemToUpdate.Price = formattedPrice;
            }
            else
            {
                Console.WriteLine("Nice try Diddy! Pass a valid number next time!\n");
                return false;
            }
        }

        SaveMenu();

        return true;
    }

    public static string HandleDecimals(double price)
    {
        double roundedPrice = Math.Round(price, 2);

        string formattedPrice = string.Format(CultureInfo.InvariantCulture, "€{0:F2}", roundedPrice);

        return formattedPrice;
    }

    public void DisplayFoodMenu()
    {
        LoadMenu();
        var table = new Table().Expand();
        table.AddColumn("[bold yellow]Category[/]");
        table.AddColumn("[bold cyan]Dish[/]");
        table.AddColumn("[bold green]Description[/]");
        table.AddColumn("[bold magenta]Price[/]");

        foreach (var category in _menuItems)
        {
            table.AddRow($"[bold yellow]{category.Key}[/]", "", "", "");
            foreach (var item in category.Value)
            {
                table.AddRow("", $"[cyan]{item.Dish}[/]", item.Description, $"[magenta]{item.Price}[/]");
            }
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public static List<string> GetAllergyOptions()
    {
        return new List<string> {
                    "Tree Nuts", "Soy", "Fish",
                    "Peanuts", "Shellfish", "Eggs",
                    "Wheats", "Dairy"
        };
    }

        // Method to display the food menu using Spectre.Console table
    // public void DisplayFoodMenu()
    // {
    //     LoadMenu();
    //     // Create a new table with headers
    //     var table = new Table();
    //     table.AddColumn("Category");
    //     table.AddColumn("Dish");
    //     table.AddColumn("Description");
    //     table.AddColumn("Price");

    //     // Loop through each category and add the menu items to the table
    //     foreach (var category in _menuItems)
    //     {
    //         // Add the category name as the first row
    //         table.AddRow(category.Key, "", "", "");
    //         table.AddRow("[grey]────────────────────────────────────────────[/]", "", "", "");

    //         // Add the menu items for this category
    //         foreach (var item in category.Value)
    //         {
    //             table.AddRow("", item.Dish, item.Description, item.Price);
    //         }

    //         // Add a line between sections
    //         table.AddRow("[grey]────────────────────────────────────────────[/]", "", "", "");
    //     }

    //     // Render the table in the console
    //     AnsiConsole.Clear();
    //     AnsiConsole.Render(table);
    // }
}
public static class FoodMenuLoader
{
    public static Dictionary<string, List<FoodMenu>> LoadMenuFromJson(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<Dictionary<string, List<FoodMenu>>>(jsonString, options);
    }
}