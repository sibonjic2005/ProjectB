using System.Runtime.InteropServices;
using System.Windows.Markup;
using Spectre.Console;
static class UserReservation
{
    public static void MakeReservation()
    {
        string email = AccountsLogic.CurrentAccount?.EmailAddress;

        if (email == null)
        {
            Console.WriteLine("No account is currently logged in.");
            return;
        }

        // var date = AnsiConsole.Prompt(
        //     new TextPrompt<string>("Enter a date: "));
        DateTime date = Calendar.CalendarDate();
        List<string> timeOptions = Calendar.GetTimeOptions(date);

        var time = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a reservation time:")
                .PageSize(10)
                .AddChoices(timeOptions)
        );

        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the amount of people: "));

        Console.Clear();
        Console.WriteLine($"\nDate: {date:dddd, MMMM dd, yyyy}, Time: {time}, Amount of persons: {person}");

        var reservation = new Reservation(
            date,
            time,
            person 
        );

        AccountsLogic accountsLogic = new AccountsLogic();
        accountsLogic.AddReservation(email, reservation);


        Console.WriteLine($"\nReservation complete!");
        UserMenu.UserMenuStart();
    }

    public static void CancelReservation()
    {
        // ViewReservation() aan roepen
        // hierzo code om te laten zien over welke reservering het gaat
        // date: \n time: \ person:
        // use the json file i guess

        string email = AccountsLogic.CurrentAccount?.EmailAddress;
        if (email == null)
        {
            Console.WriteLine("No account is currently logged in.");
            return;
        }

        ViewReservation();

        var confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>("\nDo you want to cancel your reservation?")
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        if (confirmation)
        {
            AccountsLogic accountsLogic = new AccountsLogic();
            accountsLogic.RemoveReservations(email);
        }

        Console.Clear();
        Console.WriteLine(confirmation ? "Confirmed, reservation cancelled." : "Declined, reservation is still there.");
        UserMenu.UserMenuStart();
    }

    public static void ViewReservation()
    {
        var currentUser = AccountsLogic.CurrentAccount;
        if (currentUser == null)
        {
            Console.WriteLine("No account is currently logged in.");
        }
        else if (currentUser.Reservations == null || currentUser.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
        }
        else
        {
            if (currentUser.Reservations.Count == 1)
                Console.WriteLine("Your reservation:");
            else
                Console.WriteLine("Your reservations:");
            foreach (Reservation reservation in currentUser.Reservations)
            {
                Console.WriteLine($"  - Date: {reservation.Date}\n  - Time: {reservation.Time}\n  - People: {reservation.PersonCount}\n");
            }
        }
        // UserMenu.UserMenuStart();
    }
}