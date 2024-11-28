using Spectre.Console;
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

public class FoodMenu
{
    private static string menuFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataSources/foodmenu.json"));
    public Dictionary<string, List<MenuItem>> _menuItems = new Dictionary<string, List<MenuItem>>();

    public FoodMenu()
    {
        LoadMenu();
    }

    // Load the menu from the JSON file
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
                _menuItems = new Dictionary<string, List<MenuItem>>(); // Initialize empty menu on error
            }
        }
        else
        {
            _menuItems = new Dictionary<string, List<MenuItem>>(); // Initialize empty menu if file doesn't exist
        }
    }

    // Save the menu to the JSON file
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
    
    // Method to add a menu item to the correct category
    public void AddItem(string category, string dish, string description, string price)
    {
        // If the category doesn't exist yet, create it
        if (!_menuItems.ContainsKey(category))
        {
            _menuItems[category] = new List<MenuItem>();
        }

        // Add the new menu item to the category
        _menuItems[category].Add(new MenuItem(category, dish, description, price));
        SaveMenu();
    }

    public bool DeleteItem(string dishName)
    {
        foreach (var category in _menuItems.Values)
        {
            var item = category.FirstOrDefault(i => i.Dish.Equals(dishName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                category.Remove(item);
                SaveMenu();
                return true; // Exit after removing the item
            }
        }
        return false;
    }

    // Updates a menu item based on its dish name
    public void UpdateItem(string dishName, string? newCategory, string? newDish, string? newDescription, string? newPrice)
    {
        MenuItem? itemToUpdate = null;
        string? oldCategory = null;

        // Locate the item in the current menu
        foreach (var category in _menuItems)
        {
            itemToUpdate = category.Value.FirstOrDefault(i => i.Dish.Equals(dishName, StringComparison.OrdinalIgnoreCase));
            if (itemToUpdate != null)
            {
                oldCategory = category.Key;
                break;
            }
        }

        // If the item is not found, exit
        if (itemToUpdate == null)
        {
            Console.WriteLine($"Dish '{dishName}' not found in the menu.");
            return;
        }

        // Remove the item from the old category if the category is changing
        if (!string.IsNullOrEmpty(newCategory) && !newCategory.Equals(oldCategory, StringComparison.OrdinalIgnoreCase))
        {
            _menuItems[oldCategory].Remove(itemToUpdate);

            // Clean up the old category if it's now empty
            if (_menuItems[oldCategory].Count == 0)
            {
                _menuItems.Remove(oldCategory);
            }

            // Add the item to the new category
            if (!_menuItems.ContainsKey(newCategory))
            {
                _menuItems[newCategory] = new List<MenuItem>();
            }
            _menuItems[newCategory].Add(itemToUpdate);

            // Update the item's category field
            itemToUpdate.Category = newCategory;
        }

        // Update other fields
        if (!string.IsNullOrEmpty(newDish)) itemToUpdate.Dish = newDish;
        if (!string.IsNullOrEmpty(newDescription)) itemToUpdate.Description = newDescription;
        if (!string.IsNullOrEmpty(newPrice)) itemToUpdate.Price = newPrice;

        // Save changes
        SaveMenu();

        Console.WriteLine($"Dish '{dishName}' has been updated.");
    }

    // Method to display the food menu using Spectre.Console table
    public void DisplayFoodMenu()
    {
        LoadMenu();
        // Create a new table with headers
        var table = new Table();
        table.AddColumn("Category");
        table.AddColumn("Dish");
        table.AddColumn("Description");
        table.AddColumn("Price");

        // Loop through each category and add the menu items to the table
        foreach (var category in _menuItems)
        {
            // Add the category name as the first row
            table.AddRow(category.Key, "", "", "");
            table.AddRow("[grey]────────────────────────────────────────────[/]", "", "", "");

            // Add the menu items for this category
            foreach (var item in category.Value)
            {
                table.AddRow("", item.Dish, item.Description, item.Price);
            }

            // Add a line between sections
            table.AddRow("[grey]────────────────────────────────────────────[/]", "", "", "");
        }

        // Render the table in the console
        AnsiConsole.Clear();
        AnsiConsole.Render(table);
    }
}
