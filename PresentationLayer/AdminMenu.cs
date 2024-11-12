using Spectre.Console;

 static class AdminMenu

 {
    public static void AdminMenuStart()
    {
        
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the starting menu!")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                        "Make a reservation", "Change a reservation","Cancel reservation", "View reservations", 
                        "Change Client info", "View Client info", 
                        "Edit Food menu", "Edit restaurant information",
                        "Create employee account", "Promote/Demote accounts", "Log out"        
                }));

        switch (choices)
        {
            case "Make a reservation": // for people that called
                AdminReservation.MakeReservation();
                AdminMenuStart();
                break;
            case "Change a reservation": // Cancel it, change time, change table
                AdminReservation.ChangeReservation();
                AdminMenuStart();
                break;
            case "Cancel reservation": // Cancel it, change time, change table
                AdminReservation.CancelReservation();
                AdminMenuStart();
                break;
            case "View reservations":
                AdminReservation.ViewReservation();
                AdminMenuStart();
                break;
            case "Change Client info":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "View Client info":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Edit Food menu": // Edit foods and prices
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Edit restaurant information":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Create employee account":
                CreateAccount.CreateAdmin();
                AdminMenuStart();
                break;
            case "Promote/Demote accounts":
                // Console.WriteLine("This feature is not yet implemented");
                AdminRights.PromoteDemoteUser();
                AdminMenuStart();
                break;
             case "Log out":
                Console.WriteLine("You sucessfully logged out.");
                StartingMenu.Menu();
                break;
            default: //Not neccessary needed
                Console.WriteLine("Invalid option selected. Please try again.");
                AdminMenuStart();
                break;
        }
    }
}