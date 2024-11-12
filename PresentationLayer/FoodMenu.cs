using Spectre.Console;

public class FoodMenu
{
    public static void DisplayFoodMenu()
    {
        // Maak een tabel om het foodmenu weer te geven
        var table = new Table();

        // Voeg kolommen toe aan de tabel
        table.AddColumn("Category");
        table.AddColumn("Dish");
        table.AddColumn("Description");
        table.AddColumn("Price");

        // Appetizers
        table.AddRow(
            "Appetizers", 
            "Bruschetta with Tomato and Basil", 
            "- **Crispy bread** topped with fresh tomatoes, garlic, and basil.\n- A light and flavorful start to your meal.", 
            "$6.99"
        );
        table.AddRow(
            "", // Leave category blank for subsequent dishes
            "Garlic Parmesan Breadsticks", 
            "- **Warm breadsticks** coated with garlic butter and parmesan.\n- Perfectly crispy and savory.", 
            "$5.49"
        );
        table.AddRow(
            "", 
            "Spinach and Artichoke Dip", 
            "- **Creamy dip** made with spinach, artichokes, and cheese.\n- Served with crunchy crackers for dipping.", 
            "$7.99"
        );

        // Voeg een lijn tussen de secties
        table.AddRow(
            "[grey]────────────────────────────────────────────[/]",
            "",
            "",
            ""
        );

        // Soups & Salads
        table.AddRow(
            "Soups & Salads", 
            "Classic Caesar Salad", 
            "- **Fresh romaine lettuce**, croutons, parmesan, and Caesar dressing.\n- A perfect balance of crunchy and creamy.", 
            "$8.49"
        );
        table.AddRow(
            "", 
            "Tomato Basil Soup", 
            "- **Creamy tomato soup** with a hint of basil.\n- Perfect for dipping with your favorite bread.", 
            "$5.99"
        );
        table.AddRow(
            "", 
            "Greek Salad with Feta Cheese and Olives", 
            "- **Cucumbers, tomatoes, feta, and olives** with a lemon-oregano dressing.\n- Light and refreshing with bold flavors.", 
            "$9.49"
        );

        // Voeg een lijn tussen de secties
        table.AddRow(
            "[grey]────────────────────────────────────────────[/]",
            "",
            "",
            ""
        );

        // Main Courses
        table.AddRow(
            "Main Courses", 
            "Grilled Chicken with Lemon Herb Sauce", 
            "- **Grilled chicken** served with a fresh lemon herb sauce.\n- Tender and flavorful with a citrusy touch.", 
            "$15.99"
        );
        table.AddRow(
            "", 
            "Spaghetti Carbonara", 
            "- **Pasta with eggs, bacon, parmesan, and black pepper**.\n- A creamy and savory Italian classic.", 
            "$14.99"
        );
        table.AddRow(
            "", 
            "Beef Tenderloin with Garlic Mashed Potatoes", 
            "- **Juicy beef tenderloin** paired with creamy garlic mashed potatoes.\n- A hearty and satisfying combination.", 
            "$22.99"
        );
        table.AddRow(
            "", 
            "Vegetable Stir-Fry with Tofu", 
            "- **Stir-fried vegetables** and tofu in a savory sauce.\n- A vibrant and healthy option for vegetarians.", 
            "$12.99"
        );
        table.AddRow(
            "", 
            "Shrimp Scampi with Linguine", 
            "- **Juicy shrimp** cooked in a garlic butter sauce.\n- Served over linguine pasta for a satisfying meal.", 
            "$18.99"
        );

        // Voeg een lijn tussen de secties
        table.AddRow(
            "[grey]────────────────────────────────────────────[/]",
            "",
            "",
            ""
        );

        // Side Dishes
        table.AddRow(
            "Side Dishes", 
            "Roasted Brussels Sprouts with Balsamic Glaze", 
            "- **Crispy roasted Brussels sprouts** drizzled with balsamic glaze.\n- A sweet and savory complement to any dish.", 
            "$5.99"
        );
        table.AddRow(
            "", 
            "Sweet Potato Fries", 
            "- **Golden sweet potato fries** with a perfect balance of sweetness and salt.\n- A crispy and satisfying side.", 
            "$4.99"
        );
        table.AddRow(
            "", 
            "Creamed Spinach", 
            "- **Rich and creamy spinach** cooked to perfection.\n- A classic favorite with a velvety texture.", 
            "$6.49"
        );

        // Voeg een lijn tussen de secties
        table.AddRow(
            "[grey]────────────────────────────────────────────[/]",
            "",
            "",
            ""
        );

        // Desserts
        table.AddRow(
            "Desserts", 
            "New York Cheesecake with Strawberry Topping", 
            "- **Smooth and creamy cheesecake** topped with fresh strawberries.\n- A classic dessert with a sweet and tangy topping.", 
            "$6.99"
        );
        table.AddRow(
            "", 
            "Tiramisu", 
            "- **Coffee-soaked ladyfingers** with mascarpone cream.\n- A rich and indulgent Italian dessert.", 
            "$7.49"
        );
        table.AddRow(
            "", 
            "Chocolate Lava Cake", 
            "- **Warm chocolate cake** with a gooey molten center.\n- Served with vanilla ice cream for the perfect combination.", 
            "$8.99"
        );
        table.AddRow(
            "", 
            "Fruit Salad with Honey and Mint", 
            "- **Fresh seasonal fruits** drizzled with honey and topped with mint.\n- A light and refreshing dessert option.", 
            "$5.99"
        );

        // Voeg een lijn tussen de secties
        table.AddRow(
            "[grey]────────────────────────────────────────────[/]",
            "",
            "",
            ""
        );

        // Drinks
        table.AddRow(
            "Drinks", 
            "Fresh Lemonade", 
            "- **Sweet and sour lemonade** made with fresh lemons.\n- A refreshing drink to cool you down.", 
            "$3.99"
        );
        table.AddRow(
            "", 
            "Iced Green Tea", 
            "- **Cool iced green tea** perfect for a hot day.\n- A light and refreshing beverage to accompany your meal.", 
            "$3.49"
        );
        table.AddRow(
            "", 
            "Espresso", 
            "- **Strong Italian espresso** served black.\n- A rich and bold coffee experience.", 
            "$2.99"
        );
        table.AddRow(
            "", 
            "Mango Smoothie", 
            "- **Creamy mango smoothie** made with fresh mangoes.\n- Sweet and tropical, perfect for a treat.", 
            "$4.99"
        );
        table.AddRow(
            "", 
            "Red Wine (Merlot/Cabernet Sauvignon)", 
            "- **Smooth red wine** available in Merlot or Cabernet Sauvignon.\n- A perfect pairing with any of your main courses.", 
            "$7.49"
        );

        // Pas stijl toe en render de tabel
        table.Border = TableBorder.Rounded;
        table.Centered();

        // Print het foodmenu
        AnsiConsole.Write(table);
    }
}
