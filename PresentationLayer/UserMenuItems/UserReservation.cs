using Spectre.Console;
static class UserReservation
{
    public static void MakeReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        string email = AccountsLogic.CurrentAccount?.EmailAddress;

        if (email == null)
        {
            Console.WriteLine("No account is currently logged in.");
            return;
        }

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want a blind experience?")
                .PageSize(10)
                .AddChoices("Blind Experience", "Normal Dining")
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

        if (accountsLogic.HasReservationForTimeSlot(email, date, time))
        {
            Console.WriteLine("You already have a reservation for this time slot.");
            UserMenu.UserMenuStart();
        }

        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the amount of people: "));
            TableLayout.SeatingPlan();

        List<Tables> availableTables = accountsLogic.GetAvailableTables(date, time, int.Parse(person));

        if (!availableTables.Any())
        {
            Console.WriteLine("No tables available for the selected time.");
            UserMenu.UserMenuStart();
        }

        var tableSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Tables>()
                .Title("Select a table:")
                .PageSize(10)
                .AddChoices(availableTables)
                .UseConverter(table => $"Table {table.TableNumber} ({table.Capacity}-person)")
        );

        Console.WriteLine($"You selected Table {tableSelection.TableNumber}.");

        Console.Clear();
        string formattedDate = date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"));

        var reservation = new Reservation(date, time, person, tableSelection.TableNumber);
        if (option == "Blind Experience")
        {
            reservation.BlindExperience = true;
        }
        else
        {
            reservation.BlindExperience = false;
        }

        Console.WriteLine($"\nDate: {formattedDate:dddd, MMMM dd, yyyy, hh:mm tt}\nStart Time: {time}\nEnd Time: {reservation.EndTime}\nAmount of persons: {person}\n");

        accountsLogic.AddReservation(email, reservation);

        Console.WriteLine($"\nReservation complete!");
        UserMenu.UserMenuStart();
    }

    public static void CancelReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        string email = AccountsLogic.CurrentAccount?.EmailAddress;
        var user = accountsLogic.GetByEmail(email);

        if (email == null)
        {
            Console.WriteLine("No account is currently logged in.");
            StartingMenu.Menu();
        }

        // ViewReservation();

        if (user.Reservations == null || user.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            UserMenu.UserMenuStart();
        }

        var reservationSelection = AnsiConsole.Prompt(
        new SelectionPrompt<Reservation>()
            .Title("Select the reservation you want to cancel:")
            .PageSize(10)
            .AddChoices(user.Reservations)
            .UseConverter(reservation =>
                $"Date: {reservation.Date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"))} Time: {reservation.Time} for {reservation.PersonCount} people at Table {reservation.TableNumber}")
        );

        var confirmation = AnsiConsole.Confirm("Are you sure you want to cancel this reservation?");
        if (confirmation)
        {
            accountsLogic.RemoveSpecificReservation(email, reservationSelection);

            Console.WriteLine("Reservation cancelled successfully.");
        }
        else
        {
            Console.WriteLine("Cancellation aborted.");
        }

        UserMenu.UserMenuStart();
    }

    public static void ViewReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        string email = AccountsLogic.CurrentAccount?.EmailAddress;
        var user = accountsLogic.GetByEmail(email);

        if (user == null)
        {
            Console.WriteLine("No account is currently logged in.");
        }
        else if (user.Reservations == null || user.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
        }
        else
        {
            if (user.Reservations.Count == 1)
                Console.WriteLine("Your reservation:");
            else
                Console.WriteLine("Your reservations:");
            foreach (Reservation reservation in user.Reservations)
            {
                Console.WriteLine($"  - Date: {reservation.Date.ToString("dd-MM-yyyy")}\n  - Start Time: {reservation.Time}\n  - End Time: {reservation.EndTime}\n- People: {reservation.PersonCount}\n  - Table: {reservation.TableNumber}\n");
            }
        }
        // UserMenu.UserMenuStart();
    }
}