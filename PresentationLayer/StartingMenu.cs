using Spectre.Console;

public static class StartingMenu
{
    public static void Menu()
    {
        AnsiConsole.MarkupLine("[deepskyblue1]**************************************************[/]");
        AnsiConsole.MarkupLine("[violet]*                                                *[/]");
        AnsiConsole.MarkupLine("[deeppink3]*       ██████╗ ██╗     ██╗███╗   ██╗██████╗     *[/]");
        AnsiConsole.MarkupLine("[paleturquoise1]*       ██╔══██╗██║     ██║████╗  ██║██╔══██╗    *[/]");
        AnsiConsole.MarkupLine("[magenta3]*       ██████╔╝██║     ██║██╔██╗ ██║██║  ██║    *[/]");
        AnsiConsole.MarkupLine("[darkslategray3]*       ██╔══██ ██║     ██║██║╚██╗██║██║  ██║    *[/]");
        AnsiConsole.MarkupLine("[mediumpurple]*       ██████╗ ███████╗██║██║ ╚████║██████╔╝    *[/]");
        AnsiConsole.MarkupLine("[seagreen1_1]*       ╚═════╝ ╚══════╝╚═╝╚═╝  ╚═══╝╚═════╝     *[/]");
        AnsiConsole.MarkupLine("[steelblue1]*                                                *[/]");
        AnsiConsole.MarkupLine("[mediumspringgreen]*             █████╗ ████████╗███████╗           *[/]");
        AnsiConsole.MarkupLine("[fuchsia]*            ██╔══██╗╚══██╔══╝██╔════╝           *[/]");
        AnsiConsole.MarkupLine("[green1]*            ███████║   ██║   █████╗             *[/]");
        AnsiConsole.MarkupLine("[khaki1]*            ██╔══██║   ██║   ██╔══╝             *[/]");
        AnsiConsole.MarkupLine("[lightcyan1]*            ██║  ██║   ██║   ███████╗           *[/]");
        AnsiConsole.MarkupLine("[orchid]*            ╚═╝  ╚═╝   ╚═╝   ╚══════╝           *[/]");
        AnsiConsole.MarkupLine("[lightsteelblue]*                                                *[/]");
        AnsiConsole.MarkupLine("[palegreen1]**************************************************[/]");
        AnsiConsole.WriteLine("");
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the starting menu!\n[grey](Move up and down with arrow keys)[/]")
                .PageSize(7)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                    "Login", 
                    "Create an account", 
                    "Login as guest", 
                    "View FoodMenu",
                    "View Restaurant Information",
                    "Exit"
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
            case "Login as guest":
                Console.Clear();
                GuestLogin.LoginGuest();
                break;
            case "View FoodMenu":
                Console.Clear();
                var foodMenu = new FoodMenu();
                foodMenu.DisplayFoodMenu();
                GoBack.GoBackStartingMenu();
                break;
            case "View Restaurant Information":
                Console.Clear();
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackStartingMenu();
                break;
            case "Exit":
                Console.Clear();
                Console.WriteLine("Thank you for visiting our website!");
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid option selected. Please try again.");
                Menu();
                break;
        }
    }
}