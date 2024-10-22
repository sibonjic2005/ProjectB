using Spectre.Console;
static class AdminReservation
{
    public static void MakeReservation()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a Name: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a phone number: "));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an email: "));

        var date = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a date: "));

        var time = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a time: "));
        
        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an amount of people: "));


        Console.WriteLine($"\nName: {name}, Phone Number {phonenumber}, Email: {email}, Date: {date}, Time: {time}, Amount of persons: {person}");
        Console.WriteLine($"\nReservation complete!");
        AdminMenu.AdminMenuStart();
    }

    public static void CancelReservation()
    {
        Console.WriteLine("What is the name on the reservation");
        string name = Console.ReadLine();
        Console.WriteLine("What is the phonenumber of the reservation");
        string phonenumber = Console.ReadLine();
        Console.WriteLine("What is the email of the reservation");
        string email = Console.ReadLine();
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
        AdminMenu.AdminMenuStart();
    }

    public static void ViewReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var allUsers = accountsLogic.LoadAllUsers();

        foreach (var user in allUsers)
        {
            if (user.Reservations.Count > 0)
            {
                Console.WriteLine($"Reservations for {user.Name}:");
                foreach (var reservation in user.Reservations)
                {
                    Console.WriteLine($"Date: {reservation["date"]}, Time: {reservation["time"]}, People: {reservation["amount"]}");
                }
            }
        }
        AdminMenu.AdminMenuStart();
    }

    public static void ChangeReservation()
    {
        //View old information

        //New information
        //if left empty(null) keep old information
        
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a new Name: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a new phone number: "));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a new email: "));

        var date = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a new date: "));

        var time = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a new time: "));
        
        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an amount of people: "));


        Console.WriteLine($"\nName: {name}, Phone Number {phonenumber}, Email: {email}, Date: {date}, Time: {time}, Amount of persons: {person}");
        Console.WriteLine($"\nReservation complete!");
        AdminMenu.AdminMenuStart();
    }
}