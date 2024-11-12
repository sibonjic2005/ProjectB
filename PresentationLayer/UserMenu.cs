using Spectre.Console;

 static class UserMenu

 {
    public static void UserMenuStart()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the Login Menu")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Make a reservation", "Cancel a reservation", "View your reservation",
                        "See Foodmenu", "Information", "Log out"           
                }));

        switch (choices)
        {
            case "Make a reservation":
                Console.Clear();
                UserReservation.MakeReservation();
                break;
            case "Cancel a reservation":
                Console.Clear();
                UserReservation.CancelReservation();
                break;
            case "View your reservation":
                Console.Clear();
                UserReservation.ViewReservation();
                UserMenuStart();
                break;
            case "See Foodmenu":
                Console.Clear();
                FoodMenu.DisplayFoodMenu();
                GoBack.GoBackUserMenu();
                break;
            case "Information":
                Console.Clear();
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackUserMenu();
                break;
            case "Log out":
                Console.Clear();
                Console.WriteLine("You sucessfully logged out.");
                StartingMenu.Menu();
                break;
            default: //Not necessary
                Console.Clear();
                Console.WriteLine("Invalid option selected. Please try again.");
                UserMenuStart();
                break;
        }
    }
 }