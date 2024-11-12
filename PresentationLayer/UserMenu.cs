using Spectre.Console;

 static class UserMenu

 {
    public static void UserMenuStart()
    {
        
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the starting menu?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Make a reservation", "Cancel a reservation", "View your reservation",
                        "See Foodmenu", "Information", "Log out"           
                }));

        switch (choices)
        {
            case "Make a reservation":
                UserReservation.MakeReservation();
                break;
            case "Cancel a reservation":
                UserReservation.CancelReservation();
                break;
            case "View your reservation":
                UserReservation.ViewReservation();
                UserMenuStart();
                break;
            case "See Foodmenu":
                FoodMenu.DisplayFoodMenu();
                GoBack.GoBackUserMenu();
                break;
            case "Information":
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackUserMenu();
                break;
            case "Log out":
                Console.WriteLine("You sucessfully logged out.");
                StartingMenu.Menu();
                break;
            default: //Not necessary
                Console.WriteLine("Invalid option selected. Please try again.");
                UserMenuStart();
                break;
        }
    }
 }