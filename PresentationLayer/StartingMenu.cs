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
                    //"Login as guest", 
                    "Exit",
                    "FoodMenu",
                    "Information"
                }));

        switch (choices)
        {
            case "Login":
                Console.Clear();
                UserLogin.Login();
                break;
            case "Create an account":
                Console.Clear();
                CreateAccount.CreateAcc();
                break;
            // case "Login as guest":
            //     Console.Clear();
            //     GuestLogin.LoginGuest();
            //     break;
            case "Exit":
                Console.Clear();
                Console.WriteLine("Thank you for visiting our website!");
                break;
            case "FoodMenu":
                Console.Clear();
                FoodMenu.DisplayFoodMenu();
                GoBack.GoBackStartingMenu();
                break;
            case "Information":
                Console.Clear();
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackStartingMenu();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid option selected. Please try again.");
                Menu();
                break;
        }
    }
}