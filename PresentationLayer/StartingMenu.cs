using Spectre.Console;

public static class StartingMenu
{
    public static void Menu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the starting menu?")
                .PageSize(7)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                    "Login", 
                    "Create an account", 
                    "Login as guest", 
                    "Exit", 
                    "User menu (for testing)", 
                    "Admin menu (for testing)", 
                    "FoodMenu", 
                    "Information"
                }));

        switch (choices)
        {
            case "Login":
                UserLogin.Login();
                break;
            case "Create an account":
                CreateAccount.CreateAcc();
                break;
            case "Login as guest":
                GuestLogin.LoginGuest();
                break;
            case "Exit":
                Console.WriteLine("Thank you for visiting our website!");
                break;
            case "User menu (for testing)":
                UserMenu.UserMenuStart();
                break;
            case "Admin menu (for testing)":
                AdminMenu.AdminMenuStart();
                break;
            case "FoodMenu":
                FoodMenu.DisplayFoodMenu();
                GoBack.GoBackStartingMenu();
                break;
            case "Information":
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackStartingMenu();
                break;
            default:
                Console.WriteLine("Invalid option selected. Please try again.");
                Menu();
                break;
        }
    }
}