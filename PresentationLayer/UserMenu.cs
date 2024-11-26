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
                        "See Foodmenu", "Information", "Profile", "Log out"           
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
                var foodMenu = new FoodMenu();
                foodMenu.DisplayFoodMenu();
                GoBack.GoBackStartingMenu();
                break;
            case "Information":
                Console.Clear();
                RestaurantInformation.PrintRestaurantInformation();
                GoBack.GoBackUserMenu();
                break;
            case "Profile":
                Console.Clear();

                bool inProfile = true;
                while (inProfile)
                {
                var profileChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Profile", "Edit Profile", "Delete account", "Go Back to User Menu"));

                Console.Clear();

                switch (profileChoice)
                {
                    case "View Profile":
                        UserInfo.ViewProfile();
                        GoBack.GoBackProfileMenu();
                        break;
                    case "Edit Profile":
                        UserInfo.EditProfile();
                        GoBack.GoBackProfileMenu();
                        break;
                    case "Delete account":
                        UserInfo.DeleteAccount();
                        // if user deletes their account it should go back to main menu
                        GoBack.GoBackProfileMenu();
                        break;
                    case "Go Back to User Menu":
                        inProfile = false;
                        UserMenuStart();
                        break;
                    default:
                        break;
                }
                }
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