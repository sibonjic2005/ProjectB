using System.Collections;
using Spectre.Console;

 static class AdminMenu

 {
    public static void AdminMenuStart()
    {
        
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the Admin menu!")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                        "Reservations", "Clients", 
                        "Food Menu", "Restaurant Information",
                        "Create User", "Promote/Demote Accounts","Delete User", "Log out"        
                }));

        switch (choices)
        {
            case "Reservations":
                Console.Clear();
                ChooseReservationOption();
                break;
            case "Clients":
                Console.Clear();
                ChooseClientOption();
                break;
             case "Food Menu":
                Console.Clear();
                var foodMenuManager = new FoodMenuManager();
                foodMenuManager.ManageMenu();
                AdminMenuStart();
                break;
            case "Restaurant Information":
                Console.Clear();
                ChooseRestaurantInfoOption();
                break;
            case "Create User":
                CreateAccount.CreateAdmin();
                AdminMenuStart();
                break;
            case "Promote/Demote Accounts":
                AdminRights.PromoteDemoteUser();
                AdminMenuStart();
                break;
            case "Delete User":
                AdminRights.DeleteAccount();
                GoBack.GoBackAdminMenu();
                break;
            case "Log out":
                Console.WriteLine("You successfully logged out.");
                StartingMenu.Menu();
                break;
            default:
                break;
        }
    }

    public static void ChooseReservationOption()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an option")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                        "Make a Reservation", "Change a Reservation","Cancel Reservation", "View Reservations", "Go back"       
                }));
        
        switch (choice)
        {
            case "Make a Reservation":
                Console.Clear();
                AdminReservation.MakeReservation();
                break;
            case "Change a Reservation":
                Console.Clear();
                AdminReservation.ChangeReservation();
                break;
            case "Cancel Reservation":
                Console.Clear();
                AdminReservation.CancelReservation();
                break;
            case "View Reservations":
                Console.Clear();
                AdminReservation.ViewReservation();
                break;
            case "Go back":
                Console.Clear();
                AdminMenuStart();
                break;
            default:
                break;
        }
    }

    public static void ChooseClientOption()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an option")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                        "View Client Info", "Change Client Info", "Go back"       
                }));
        
        switch (choice)
        {
            case "View Client Info":
                Console.Clear();
                ClientInfo.ViewClientInfo();
                break;
            case "Change Client Info":
                Console.Clear();
                ClientInfo.ChangeClientInfo();
                break;
            case "Go back":
                Console.Clear();
                AdminMenuStart();
                break;
            default:
                break;
        }
    }

    public static void ChooseRestaurantInfoOption()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an option")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                        "View Restaurant Info", "Change Restaurant Info", "Go back"       
                }));

        switch (choice)
        {
            case "View Restaurant Info":
                Console.Clear();
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackRestaurantInfo();
                break;
            case "Change Restaurant Info":
                Console.Clear();
                RestaurantInformation.EditRestaurantInformation();
                break;
            case "Go back":
                Console.Clear();
                AdminMenuStart();
                break;
            default:
                break;
        }
    }
}