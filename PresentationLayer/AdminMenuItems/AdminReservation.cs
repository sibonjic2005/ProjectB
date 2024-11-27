using Spectre.Console;
static class AdminReservation
{
    public static void MakeReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

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

        DateTime date = Calendar.CalendarDate();
        List<string> timeOptions = Calendar.GetTimeOptions(date);

        var time = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a reservation time:")
                .PageSize(10)
                .AddChoices(timeOptions)
        );

        int selectedHour = int.Parse(time.Split(':')[0]);
        date = date.AddHours(selectedHour);

        if (accountsLogic.HasReservationForTimeSlot(email, date, time))
        {
            Console.WriteLine("You already have a reservation for this time slot.");
            return;
        }
        
        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the amount of people: ")
        );
        
        TableLayout.SeatingPlan();

        List<Tables> availableTables = accountsLogic.GetAvailableTables(date, time, int.Parse(person));

        if (!availableTables.Any())
        {
            Console.WriteLine("No tables available for the selected time.");
            return;
        }

        var tableSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Tables>()
                .Title("Select a table:")
                .PageSize(10)
                .AddChoices(availableTables)
                .UseConverter(table => $"Table {table.TableNumber} ({table.Capacity}-person)")
        );

        Console.WriteLine($"You selected Table {tableSelection.TableNumber}.");

        var reservation = new Reservation(date, time, person, tableSelection.TableNumber);

        accountsLogic.AddNewReservation(name, email, phonenumber, allergies, reservation);

        Console.WriteLine($"\nName: {name}, Phone Number {phonenumber}, Email: {email}, Date: {date:dddd, MMMM dd, yyyy}, Time: {time}, Amount of persons: {person}, End time: {reservation.EndTime}");
        Console.WriteLine($"\nReservation complete!");
        // AdminMenu.AdminMenuStart();
    }

    public static void CancelReservation()
    {
        ViewReservation();

        Console.WriteLine("What is the email of the guest whose reservation you want to cancel?");
        string email = Console.ReadLine();
        
        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.GetByEmail(email);

        if (user == null || user.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations found for this email.");
            return;
        }

        var reservationSelection = AnsiConsole.Prompt(
        new SelectionPrompt<Reservation>()
            .Title("Select the reservation you want to cancel:")
            .PageSize(10)
            .AddChoices(user.Reservations)
            .UseConverter(reservation =>
                $"Date: {reservation.Date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"))} Time: {reservation.Time} for {reservation.PersonCount} people at Table {reservation.TableNumber}")
        );

        var confirmation = AnsiConsole.Confirm($"Are you sure you want to cancel this reservation?");
        if (confirmation)
        {
            accountsLogic.RemoveSpecificReservation(email, reservationSelection);
            Console.WriteLine("Reservation cancelled successfully.");
        }
        else
        {
            Console.WriteLine("Cancellation aborted.");
        }
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
                foreach (Reservation reservation in user.Reservations)
                {
                    Console.WriteLine($"  - Date: {reservation.Date.ToString("dd-MM-yyyy")}\n  - Start Time: {reservation.Time}\n  - End time: {reservation.EndTime}\n  - People: {reservation.PersonCount}\n  - Table: {reservation.TableNumber}\n");
                }
            }
        }
        // AdminMenu.AdminMenuStart();
    }

    public static void ChangeReservation()
    {
        Console.WriteLine("Enter the email of the guest whose reservation you want to change?");
        string getEmail = Console.ReadLine();

        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.GetByEmail(getEmail);
    
        if (user == null || user.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations found for this email.");
            return;
        }

        var reservationSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Reservation>()
                .Title("Select the reservation you want to change:")
                .PageSize(10)
                .AddChoices(user.Reservations)
                .UseConverter(reservation =>
                    $"Date: {reservation.Date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"))} Time: {reservation.Time} for {reservation.PersonCount} people at Table {reservation.TableNumber}")
        );

        DateTime date = Calendar.CalendarDate();
        List<string> timeOptions = Calendar.GetTimeOptions(date);

        var time = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a reservation time:")
                .PageSize(10)
                .AddChoices(timeOptions)
        );

        int selectedHour = int.Parse(time.Split(':')[0]);
        date = date.AddHours(selectedHour);
        
        var personCount = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the updated amount of people: ")
            
        );
        TableLayout.SeatingPlan();
        List<Tables> availableTables = accountsLogic.GetAvailableTables(date, time, int.Parse(personCount));

        if (!availableTables.Any())
        {
            Console.WriteLine("No tables available for the selected time.");
            return;
        }

        var tableSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Tables>()
                .Title("Select a table:")
                .PageSize(10)
                .AddChoices(availableTables)
                .UseConverter(table => $"Table {table.TableNumber} ({table.Capacity}-person)")
        );

        reservationSelection.Date = date;
        reservationSelection.Time = time;
        reservationSelection.PersonCount = personCount;
        reservationSelection.TableNumber = tableSelection.TableNumber;

        accountsLogic.UpdateReservation(getEmail, reservationSelection);

        Console.WriteLine($"Reservation updated:\nDate: {date:dddd, MMMM dd, yyyy}\nStart Time: {time}\nEnd Time: {reservationSelection.EndTime}\nPeople: {personCount}\nTable: {tableSelection.TableNumber}");
    }
}