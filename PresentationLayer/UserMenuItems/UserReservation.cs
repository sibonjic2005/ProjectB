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

        var date = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a date: "));

        var time = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a time: "));
        
        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the amount of people: "));


        Console.WriteLine($"\nDate: {date}, Time: {time}, Amount of persons: {person}");

        Dictionary<string, string> reservation = new Dictionary<string, string>
        {
            { "date", date },
            { "time", time },
            { "amount", person }
        };

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
        var confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>("Do you want to cancel your reservation?")
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

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
            foreach (var reservation in currentUser.Reservations)
            {
                Console.WriteLine($"Date: {reservation["date"]}, Time: {reservation["time"]}, People: {reservation["amount"]}");
            }
        }
        UserMenu.UserMenuStart();
    }

    public static void Calendar()
    {
        var calendar = new Calendar(2024,10);
        AnsiConsole.Write(calendar);
    }
}