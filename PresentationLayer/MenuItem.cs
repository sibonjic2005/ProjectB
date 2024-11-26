using System;
using System.Collections.Generic;
using Spectre.Console;

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
    // A dictionary to hold menu items categorized by their category
    public Dictionary<string, List<MenuItem>> _menuItems = new Dictionary<string, List<MenuItem>>
    {
        // Surprise Menu
        ["Surprise Menu"] = new List<MenuItem>
        {
            new MenuItem("Surprise Menu", "Surprise 3-Course Platter", "- A delicious assortment of **freshly prepared dishes**.\n- A Surprise 3-Course Experience.\n- Each course is a unique surprise, made to order, for a truly one-of-a-kind dining experience!", "$54.99")
        },

        // Appetizers
        ["Appetizers"] = new List<MenuItem>
        {
            new MenuItem("Appetizers", "Bruschetta with Tomato and Basil", "- **Crispy bread** topped with fresh tomatoes, garlic, and basil.\n- A light and flavorful start to your meal.", "$6.99"),
            new MenuItem("Appetizers", "Garlic Parmesan Breadsticks", "- **Warm breadsticks** coated with garlic butter and parmesan.\n- Perfectly crispy and savory.", "$5.49"),
            new MenuItem("Appetizers", "Spinach and Artichoke Dip", "- **Creamy dip** made with spinach, artichokes, and cheese.\n- Served with crunchy crackers for dipping.", "$7.99")
        },

        // Soups & Salads
        ["Soups & Salads"] = new List<MenuItem>
        {
            new MenuItem("Soups & Salads", "Classic Caesar Salad", "- **Fresh romaine lettuce**, croutons, parmesan, and Caesar dressing.\n- A perfect balance of crunchy and creamy.", "$8.49"),
            new MenuItem("Soups & Salads", "Tomato Basil Soup", "- **Creamy tomato soup** with a hint of basil.\n- Perfect for dipping with your favorite bread.", "$5.99"),
            new MenuItem("Soups & Salads", "Greek Salad with Feta Cheese and Olives", "- **Cucumbers, tomatoes, feta, and olives** with a lemon-oregano dressing.\n- Light and refreshing with bold flavors.", "$9.49")
        },

        // Main Courses
        ["Main Courses"] = new List<MenuItem>
        {
            new MenuItem("Main Courses", "Grilled Chicken with Lemon Herb Sauce", "- **Grilled chicken** served with a fresh lemon herb sauce.\n- Tender and flavorful with a citrusy touch.", "$15.99"),
            new MenuItem("Main Courses", "Spaghetti Carbonara", "- **Pasta with eggs, bacon, parmesan, and black pepper**.\n- A creamy and savory Italian classic.", "$14.99"),
            new MenuItem("Main Courses", "Beef Tenderloin with Garlic Mashed Potatoes", "- **Juicy beef tenderloin** paired with creamy garlic mashed potatoes.\n- A hearty and satisfying combination.", "$22.99"),
            new MenuItem("Main Courses", "Vegetable Stir-Fry with Tofu", "- **Stir-fried vegetables** and tofu in a savory sauce.\n- A vibrant and healthy option for vegetarians.", "$12.99"),
            new MenuItem("Main Courses", "Shrimp Scampi with Linguine", "- **Juicy shrimp** cooked in a garlic butter sauce.\n- Served over linguine pasta for a satisfying meal.", "$18.99")
        },

        // Side Dishes
        ["Side Dishes"] = new List<MenuItem>
        {
            new MenuItem("Side Dishes", "Roasted Brussels Sprouts with Balsamic Glaze", "- **Crispy roasted Brussels sprouts** drizzled with balsamic glaze.\n- A sweet and savory complement to any dish.", "$5.99"),
            new MenuItem("Side Dishes", "Sweet Potato Fries", "- **Golden sweet potato fries** with a perfect balance of sweetness and salt.\n- A crispy and satisfying side.", "$4.99"),
            new MenuItem("Side Dishes", "Creamed Spinach", "- **Rich and creamy spinach** cooked to perfection.\n- A classic favorite with a velvety texture.", "$6.49")
        },

        // Desserts
        ["Desserts"] = new List<MenuItem>
        {
            new MenuItem("Desserts", "New York Cheesecake with Strawberry Topping", "- **Smooth and creamy cheesecake** topped with fresh strawberries.\n- A classic dessert with a sweet and tangy topping.", "$6.99"),
            new MenuItem("Desserts", "Tiramisu", "- **Coffee-soaked ladyfingers** with mascarpone cream.\n- A rich and indulgent Italian dessert.", "$7.49"),
            new MenuItem("Desserts", "Chocolate Lava Cake", "- **Warm chocolate cake** with a gooey molten center.\n- Served with vanilla ice cream for the perfect combination.", "$8.99"),
            new MenuItem("Desserts", "Fruit Salad with Honey and Mint", "- **Fresh seasonal fruits** drizzled with honey and topped with mint.\n- A light and refreshing dessert option.", "$5.99")
        },

        // Drinks
        ["Drinks"] = new List<MenuItem>
        {
            new MenuItem("Drinks", "Fresh Lemonade", "- **Sweet and sour lemonade** made with fresh lemons.\n- A refreshing drink to cool you down.", "$3.99"),
            new MenuItem("Drinks", "Iced Green Tea", "- **Cool iced green tea** perfect for a hot day.\n- A light and refreshing beverage to accompany your meal.", "$3.49"),
            new MenuItem("Drinks", "Espresso", "- **Strong Italian espresso** served black.\n- A rich and bold coffee experience.", "$2.99"),
            new MenuItem("Drinks", "Mango Smoothie", "- **Creamy mango smoothie** made with fresh mangoes.\n- Sweet and tropical, perfect for a treat.", "$4.99"),
            new MenuItem("Drinks", "Red Wine (Merlot/Cabernet Sauvignon)", "- **Smooth red wine** available in Merlot or Cabernet Sauvignon.\n- A perfect pairing with any of your main courses.", "$7.49")
        }
    };
    
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
    }

    public bool DeleteItem(string dishName)
    {
        foreach (var category in _menuItems.Values)
        {
            var item = category.FirstOrDefault(i => i.Dish.Equals(dishName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                category.Remove(item);
                return true; // Exit after removing the item
            }
        }
        return false;
    }

    // Updates a menu item based on its dish name
    public void UpdateItem(string dishName, string? newCategory, string? newDish, string? newDescription, string? newPrice)
    {
        foreach (var category in _menuItems.Values)
        {
            var item = category.FirstOrDefault(i => i.Dish.Equals(dishName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                if (!string.IsNullOrEmpty(newCategory)) item.Category = newCategory;
                if (!string.IsNullOrEmpty(newDish)) item.Dish = newDish;
                if (!string.IsNullOrEmpty(newDescription)) item.Description = newDescription;
                if (!string.IsNullOrEmpty(newPrice)) item.Price = newPrice;
                return; // Exit after updating the item
            }
        }
    }

    // Method to display the food menu using Spectre.Console table
    public void DisplayFoodMenu()
    {
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
