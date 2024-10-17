using Spectre.Console;

 static class AdminMenu

 {
    public static void AdminMenuStart()
    {
        
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the starting menu?")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Make a reservation", "Cancel a reservation", "View reservations", 
                        "Log out", "Edit restaurant information"           
                }));

        switch (choices)
        {
            case "Make a reservation":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Cancel a reservation":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "View reservations":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "Log out":
                Console.WriteLine("You sucessfully logged out.");
                StartingMenu.Menu();
                break;
            case "Edit restaurant information":
                Console.WriteLine("This feature is not yet implemented");
                break;
            default: //Not neccessary needed
                Console.WriteLine("Invalid option selected. Please try again.");
                AdminMenuStart();
                break;
        }
    }
}