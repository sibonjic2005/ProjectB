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

        var allergies = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Do have any allergies?")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to choose your allergy, " + 
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(new[] {
                    "Tree Nuts", "Soy", "Fish",
                    "Peanuts", "Shellfish", "Eggs",
                    "Wheats", "Dairy"
        }));

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
            new TextPrompt<string>("Enter an amount of people: "));

        Dictionary<string, string> reservation = new Dictionary<string, string>
        {
            { "date", date.ToString("dd-MM-yyyy") },
            { "time", time },
            { "amount", person }
        };

        AccountsLogic accountsLogic = new AccountsLogic();
        accountsLogic.AddNewReservation(name, email, phonenumber, allergies, reservation);

        Console.WriteLine($"\nName: {name}, Phone Number {phonenumber}, Email: {email}, Date: {date:dddd, MMMM dd, yyyy}, Time: {time}, Amount of persons: {person}");
        Console.WriteLine($"\nReservation complete!");
        AdminMenu.AdminMenuStart();
    }

    public static void CancelReservation()
    {
        ViewReservation();
        // Console.WriteLine("What is the name on the reservation");
        // string name = Console.ReadLine();
        // Console.WriteLine("What is the phonenumber of the reservation");
        // string phonenumber = Console.ReadLine();
        Console.WriteLine("What is the email of the guest?");
        string email = Console.ReadLine();

        var confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>($"Do you want to cancel the reservation of {email}?")
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        if (confirmation)
        {
            AccountsLogic accountsLogic = new AccountsLogic();
            accountsLogic.RemoveReservations(email);
        }

        Console.WriteLine(confirmation ? "Confirmed, reservation cancelled." : "Declined, reservation is still there.");
        AdminMenu.AdminMenuStart();
    }

    public static void ViewReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var allUsers = accountsLogic.LoadAllUsers();

        Console.WriteLine($"Reservations: \n");
        foreach (var user in allUsers)
        {
            if (user.Reservations.Count > 0)
            {
                Console.WriteLine($"Name: {user.Name}\nemail: {user.EmailAddress}:");
                foreach (var reservation in user.Reservations)
                {
                    Console.WriteLine($"  - Date: {reservation["date"]}\n  - Time: {reservation["time"]}\n  - People: {reservation["amount"]}\n");
                }
            }
        }
        // AdminMenu.AdminMenuStart();
    }

    public static void ChangeReservation()
    {
        Console.WriteLine("Give the email of the reservation who you want to change?");
        string getemail = Console.ReadLine();

        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.GetByEmail(getemail);
            if (user == null)
            {
                Console.WriteLine("No reservation found with the given email.");
                AdminMenu.AdminMenuStart();
                return;
            }


        //accountsLogic.ChangeReservation(getemail);
    
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