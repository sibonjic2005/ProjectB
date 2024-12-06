using Spectre.Console;

 static class UserMenu

 {
    public static void UserMenuStart()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the User Menu!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Make a Reservation", "Cancel a Reservation", "View your Reservation",
                        "View Food Menu", "View Restaurant Information", "Profile", "Log out"           
                }));

        switch (choices)
        {
            case "Make a Reservation":
                Console.Clear();
                UserReservation.MakeReservation();
                break;
            case "Cancel a Reservation":
                Console.Clear();
                UserReservation.CancelReservation();
                break;
            case "View your Reservation":
                Console.Clear();
                UserReservation.ViewReservation();
                UserMenuStart();
                break;
            case "View Food Menu":
                Console.Clear();
                var foodMenu = new FoodMenu();
                foodMenu.DisplayFoodMenu();
                GoBack.GoBackUserMenu();
                Console.Clear();
                UserMenuStart();
                break;
            case "View Restaurant Information":
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
                        .AddChoices("View Profile", "Edit Profile", "Delete Account", "Go Back to User Menu"));

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
                    case "Delete Account":
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