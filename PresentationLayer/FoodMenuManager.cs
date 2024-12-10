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
        List<string> options = foodMenu._menuItems.Keys.ToList();
        options.Add("Go back to Admin Menu");
        while (true)
        {
            // Display options to the user
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Select an option to manage the menu:[/]")
                    .AddChoices("Add Item", "Delete Item", "Update Item", "Display Menu", "Go back"));

            switch (choice)
            {
                case "Add Item":
                    var category = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select a [green]category[/]:")
                            .AddChoices(options));
                    
                    if (category == "Go back to Admin Menu")
                    {
                        AdminMenu.AdminMenuStart();
                    }

                    var dish = AnsiConsole.Ask<string>("Enter the [green]dish name[/]:");
                    var description = AnsiConsole.Ask<string>("Enter the [green]description[/]:");
                    var price = AnsiConsole.Ask<string>("Enter the [green]price[/]:");

                    // Add the new item
                    bool isAdded = foodMenu.AddItem(category, dish, description, price);
                    if (isAdded)
                    {
                        AnsiConsole.MarkupLine("[green]Item added successfully![/]");
                        GoBack.GoBackManageMenu();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]No item has been added![/]");
                        GoBack.GoBackManageMenu();
                    }
                    break;

                case "Delete Item":      
                    var category1 = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Select the [red]category[/] of the dish you'd like to delete:")
                        .AddChoices(options)
                    );

                    if (category1 == "Go back to Admin Menu")
                    {
                        AdminMenu.AdminMenuStart();
                    }

                    List<string> dishesInCategory = new List<string>();

                    foreach (var itemList in foodMenu._menuItems.Values)
                    {
                        foreach (MenuItem item in itemList)
                        {
                            if (item.Category == category1)
                            {
                                dishesInCategory.Add(item.Dish);
                            }
                        }
                    }

                    var dishToDelete = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Select the [red]dish[/] you'd like to delete:")
                        .AddChoices(dishesInCategory)
                    );

                    if (foodMenu.DeleteItem(dishToDelete))
                    {
                        AnsiConsole.MarkupLine("[green]Item deleted successfully![/]");
                        GoBack.GoBackManageMenu();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Item not found![/]");
                        GoBack.GoBackManageMenu();
                    }
                    break;

                case "Update Item":
                    var category2 = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Select the [green]category[/] of the dish you'd like to update:")
                        .AddChoices(options)
                    );
                    List<string> dishesInCategory1 = new List<string>();

                    foreach (var itemList in foodMenu._menuItems.Values)
                    {
                        foreach (MenuItem item in itemList)
                        {
                            if (item.Category == category2)
                            {
                                dishesInCategory1.Add(item.Dish);
                            }
                        }
                    }

                    var itemToUpdate = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Select the [green]dish[/] you'd like to update:")
                        .AddChoices(dishesInCategory1)
                    );

                    if (itemToUpdate != null)
                    {
                        // Prompt for updated fields (skip any that user doesn't want to change)
                        var newCategory = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter the new [green]category[/] (or press Enter to skip):").AllowEmpty()
                        );
                        var newDish = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter the new [green]dish name[/] (or press Enter to skip):").AllowEmpty()
                        );
                        var newDescription = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter the new [green]description[/] (or press Enter to skip):").AllowEmpty()
                        );
                        var newPrice = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter the new [green]price[/] (or press Enter to skip):").AllowEmpty()
                        );

                        bool isUpdated = foodMenu.UpdateItem(
                            itemToUpdate,
                            string.IsNullOrEmpty(newCategory) ? null : newCategory,
                            string.IsNullOrEmpty(newDish) ? null : newDish,
                            string.IsNullOrEmpty(newDescription) ? null : newDescription,
                            string.IsNullOrEmpty(newPrice) ? null : newPrice);

                        if (isUpdated)
                        {
                            AnsiConsole.MarkupLine("[green]Item updated successfully![/]");

                            GoBack.GoBackManageMenu();
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Item not updated![/]");

                            GoBack.GoBackManageMenu();
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Dish not found![/]");
                        
                        GoBack.GoBackManageMenu();
                    }
                    break;
                case "Display Menu":
                    foodMenu.DisplayFoodMenu();
                    GoBack.GoBackManageMenu();
                    break;

                case "Go back":
                    // Exit the management loop
                    AnsiConsole.MarkupLine("[bold yellow]Exiting menu management.[/]");
                    return;
            }
        }
    }
}