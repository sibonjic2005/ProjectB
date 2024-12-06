using Spectre.Console;

 static class EmployeeMenu

 {
    public static void EmployeeMenuStart()
    {
        
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the employee menu!")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                        "Make a reservation", "Change a reservation",
                        "Cancel reservation", "View reservations", 
                        "Edit Food menu", "Log out"        
                }));

        switch (choices)
        {
            case "Make a reservation":
                AdminReservation.MakeReservation();
                EmployeeMenuStart();
                break;
            case "Change a reservation":
                AdminReservation.ChangeReservation();
                EmployeeMenuStart();
                break;
            case "Cancel reservation":
                AdminReservation.CancelReservation();
                EmployeeMenuStart();
                break;
            case "View reservations":
                AdminReservation.ViewReservation();
                EmployeeMenuStart();
                break;
            case "Edit Food menu": // Edit foods and prices
                Console.WriteLine("This feature is not yet implemented");
                break;
             case "Log out":
                Console.WriteLine("You successfully logged out.");
                StartingMenu.Menu();
                break;
            default: //Not necessary needed
                Console.WriteLine("Invalid option selected. Please try again.");
                EmployeeMenuStart();
                break;
        }
    }
}