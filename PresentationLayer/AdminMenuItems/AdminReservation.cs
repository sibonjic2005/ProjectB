using Spectre.Console;
static class AdminReservation
{
    public static void MakeReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a Name: "));

        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a phone number: "));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an email: "));

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

        var personCountInput = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the amount of people: "));

        if (!int.TryParse(personCountInput, out int personCount) || personCount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        TableLayout.SeatingPlan();
        List<Tables> availableTables = accountsLogic.GetAvailableTables(date, time, personCount);

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

        var reservation = new Reservation(date, time, personCount.ToString(), tableSelection.TableNumber);

        List<PersonReservation> personReservations = new List<PersonReservation>();

        Console.WriteLine($"\nThe first person is: {name}");
        var adminReservation = new PersonReservation(name);

        HandleBlindExperience(adminReservation, name);
        personReservations.Add(adminReservation);

        for (int i = 2; i <= personCount; i++)
        {
            var otherPersonName = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter the name for person {i}: ")
            );

            var otherPersonReservation = new PersonReservation(otherPersonName);

            HandleBlindExperience(otherPersonReservation, otherPersonName);
            personReservations.Add(otherPersonReservation);
        }

        foreach (var person in personReservations)
        {
            reservation.People.Add(person);
        }

        accountsLogic.AddNewReservation(name, email, phoneNumber, new List<string>(), reservation);

        Console.WriteLine($"\nReservation complete!");
        Console.WriteLine($"\nName: {name}, Phone Number: {phoneNumber}, Email: {email}, Date: {date:dddd, MMMM dd, yyyy}, Time: {time}, Amount of persons: {personCount}, End time: {reservation.EndTime}");
    }

    private static void HandleBlindExperience(PersonReservation personReservation, string personName)
    {
        Console.WriteLine($"\n{personName}, do you want a blind experience? (y/n)");
        string blindExperienceResponse = Console.ReadLine();

        if (blindExperienceResponse == "y" || blindExperienceResponse == "Y")
        {
            personReservation.BlindExperience = true;
            Console.WriteLine($"{personName} has chosen a blind experience.");
            personReservation.Food.Add("SURPRISE-MEAL");

            var allergies = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title($"{personName}, do you have any allergies?")
                    .NotRequired()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more allergies)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to choose an allergy, [green]<enter>[/] to accept)[/]")
                    .AddChoices(FoodMenu.GetAllergyOptions())
            );

            personReservation.Allergies.AddRange(allergies);
        }
        else
        {
            Console.WriteLine($"{personName} chose not to have a blind experience.");
        }
    }


    public static void CancelReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        ViewReservation();

        List<string> mailWithReservation = accountsLogic.GetMailsWithReservation();

        if (mailWithReservation.Count < 1)
        {
            Console.WriteLine("How can you cancel a reservation without one?");
            GoBack.GoBackReservationOption();
        }

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the user's mail whose reservation should be cancelled:")
                .PageSize(10)
                .AddChoices(mailWithReservation)
        );

        var user = accountsLogic.GetByEmail(userMail);

        var reservationSelection = AnsiConsole.Prompt(
        new SelectionPrompt<Reservation>()
            .Title("\nSelect the reservation you want to cancel:")
            .PageSize(10)
            .AddChoices(user.Reservations)
            .UseConverter(reservation =>
                $"Date: {reservation.Date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"))} Time: {reservation.Time} for {reservation.PersonCount} people at Table {reservation.TableNumber}")
        );

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Are you sure you want to cancel this reservation?")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n"));
                
        if (confirmation)
        {
            accountsLogic.RemoveSpecificReservation(userMail, reservationSelection);
            Console.WriteLine("Reservation cancelled successfully.");
            GoBack.GoBackReservationOption();
        }
        else
        {
            Console.WriteLine("Cancellation aborted.");
            GoBack.GoBackReservationOption();
        }
    }

    public static void ViewReservations()
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
        GoBack.GoBackReservationOption();
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
    }

    public static void ChangeReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        ViewReservation();

        List<string> mailWithReservation = accountsLogic.GetMailsWithReservation();

        if (mailWithReservation.Count < 1)
        {
            Console.WriteLine("How can you change a reservation without one?");
            GoBack.GoBackReservationOption();
        }

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the user's mail whose reservation should be changed:")
                .PageSize(10)
                .AddChoices(mailWithReservation)
        );
        var user = accountsLogic.GetByEmail(userMail);

        var reservationSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Reservation>()
                .Title("\nSelect the reservation you want to change:")
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

        if (availableTables.Count == 0)
        {
            Console.WriteLine("No tables available for the selected time.");
            GoBack.GoBackReservationOption();
        }

        var tableSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Tables>()
                .Title("Select a table:")
                .PageSize(10)
                .AddChoices(availableTables)
                .UseConverter(table => $"Table {table.TableNumber} ({table.Capacity}-person)")
        );

        Console.WriteLine($"Table Selected: {tableSelection.TableNumber}");

        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Press <enter> to continue")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Continue"            
                }));   
                
        if (choices == "Continue")
        {
            Console.Clear();
        }

        reservationSelection.Date = date;
        reservationSelection.Time = time;
        reservationSelection.PersonCount = personCount;
        reservationSelection.TableNumber = tableSelection.TableNumber;

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Are you sure you want to change this reservation?")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n"));
                
        if (confirmation)
        {
            accountsLogic.UpdateReservation(userMail, reservationSelection);

            Console.WriteLine($"Reservation updated:\nDate: {date:dddd, MMMM dd, yyyy}\nStart Time: {time}\nEnd Time: {reservationSelection.EndTime}\nPeople: {personCount}\nTable: {tableSelection.TableNumber}\n");
            
            GoBack.GoBackReservationOption();
        }
        else
        {
            Console.WriteLine("Changing Reservation aborted.");

            GoBack.GoBackReservationOption();
        }
    }
}