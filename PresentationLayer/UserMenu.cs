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
                        "Make a reservation", "Cancel a reservation", "View a reservation",
                        "See Foodmenu", "Information", "Log out"           
                }));

        switch (choices)
        {
            case "Make a reservation":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Cancel a reservation":
                Reservation.CancelReservation();
                break;
            case "View your reservation":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "See Foodmenu":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Information":
                RestaurantInformation.PrintRestaurantInformation();
                break;
            case "Log out":
                Console.WriteLine("You sucessfully logged out.");
                StartingMenu.Menu();
                break;
            default: //Not neccessary needed
                Console.WriteLine("Invalid option selected. Please try again.");
                UserMenuStart();
                break;
        }
    }
}