using Spectre.Console;

public  class FoodMenuManager
{
    private  readonly FoodMenu foodMenu;

    public  FoodMenuManager()

    {
        foodMenu = new FoodMenu();  // Create an instance of FoodMenu to manage the menu
    }

    public void ManageMenu()
    {
        while (true)
        {
            // Display options to the user
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Select an option to manage the menu:[/]")
                    .AddChoices("Add Item", "Delete Item", "Update Item", "Display Menu", "Exit"));

            switch (choice)
            {
                case "Add Item":
                    // Gather input for the new item
                    var category = AnsiConsole.Ask<string>("Enter the [green]category[/]:");
                    var dish = AnsiConsole.Ask<string>("Enter the [green]dish name[/]:");
                    var description = AnsiConsole.Ask<string>("Enter the [green]description[/]:");
                    var price = AnsiConsole.Ask<string>("Enter the [green]price[/]:");

                    // Add the new item
                    foodMenu.AddItem(category, dish, description, price);
                    AnsiConsole.MarkupLine("[green]Item added successfully![/]");
                    break;

                case "Delete Item":
                    // Ask for the name of the dish to delete
                    var dishToDelete = AnsiConsole.Ask<string>("Enter the [red]dish name[/] to delete:");

                    // Attempt to delete the item
                    if (foodMenu.DeleteItem(dishToDelete))
                    {
                        AnsiConsole.MarkupLine("[green]Item deleted successfully![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Item not found![/]");
                    }
                    break;

                case "Update Item":
                    // Ask for the name of the dish to update
                    var dishToUpdate = AnsiConsole.Ask<string>("Enter the [yellow]dish name[/] to update:");

                    // Check if the dish exists within any category before asking for new values
                    var itemToUpdate = foodMenu._menuItems
                        .SelectMany(pair => pair.Value)  // Flatten the dictionary to access each MenuItem
                        .FirstOrDefault(i => i.Dish.Equals(dishToUpdate, StringComparison.OrdinalIgnoreCase));

                    if (itemToUpdate != null)
                    {
                        // Prompt for updated fields (skip any that user doesn't want to change)
                        var newCategory = AnsiConsole.Ask<string>("Enter the new [green]category[/] (or press Enter to skip):", "");
                        var newDish = AnsiConsole.Ask<string>("Enter the new [green]dish name[/] (or press Enter to skip):", "");
                        var newDescription = AnsiConsole.Ask<string>("Enter the new [green]description[/] (or press Enter to skip):", "");
                        var newPrice = AnsiConsole.Ask<string>("Enter the new [green]price[/] (or press Enter to skip):", "");

                        // Update the item
                        foodMenu.UpdateItem(
                            dishToUpdate,
                            string.IsNullOrEmpty(newCategory) ? null : newCategory,
                            string.IsNullOrEmpty(newDish) ? null : newDish,
                            string.IsNullOrEmpty(newDescription) ? null : newDescription,
                            string.IsNullOrEmpty(newPrice) ? null : newPrice);

                        AnsiConsole.MarkupLine("[green]Item updated successfully![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Dish not found![/]");
                    }
                    break;
                case "Display Menu":
                    foodMenu.DisplayFoodMenu();
                    break;

                case "Exit":
                    // Exit the management loop
                    AnsiConsole.MarkupLine("[bold yellow]Exiting menu management.[/]");
                    return;
            }
        }
    }
}