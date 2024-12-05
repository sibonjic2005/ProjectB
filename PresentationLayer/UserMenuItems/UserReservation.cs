using Spectre.Console;
static class UserReservation
{
    public static void MakeReservation()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        string email = AccountsLogic.CurrentAccount?.EmailAddress;
        string accountName = AccountsLogic.CurrentAccount?.Name; // Haal de naam van de ingelogde gebruiker
        var user = accountsLogic.GetByEmail(email);

        if (email == null || accountName == null)
        {
            Console.WriteLine("No account is currently logged in.");
            return;
        }

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
            return;
        }

        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the amount of people:")
        );

        if (!int.TryParse(person, out int personCount) || personCount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            UserMenu.UserMenuStart();
            return;
        }

        if (personCount > 6)
        {
            Console.Clear();
            Console.WriteLine("For reservations of more than 6 people, please call the restaurant.");
            UserMenu.UserMenuStart();
            return;
        }

        TableLayout.SeatingPlan();

        List<Tables> availableTables = accountsLogic.GetAvailableTables(date, time, personCount);

        if (!availableTables.Any())
        {
            Console.WriteLine("No tables available for the selected time.");
            UserMenu.UserMenuStart();
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

        Console.Clear();
        string formattedDate = date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"));

        var reservation = new Reservation(date, time, person, tableSelection.TableNumber);

        var foodMenu = new FoodMenu();

        List<string> allergyOptions = AccountsLogic.GetAllergyOptions();

        Console.WriteLine($"The first person is the account holder: {accountName}");
        var personReservation = new PersonReservation(accountName);

        Console.WriteLine($"Would you like to change the allergies of {accountName}?:");
        foreach (string allergy in user.Preferences)
        {
            personReservation.Allergies.Add(allergy);
        }

        var allergyPrompt = new MultiSelectionPrompt<string>()
                .Title("Update allergies [grey](use <space> to select/deselect)[/]:")
                .NotRequired()
                .PageSize(10)
                .AddChoices(allergyOptions);
        
        foreach (var allergy in user.Preferences.Where(allergyOptions.Contains))
        {
            allergyPrompt.Select(allergy);
        }

        var updatedAllergies = AnsiConsole.Prompt(allergyPrompt);

        personReservation.Allergies = updatedAllergies;
        user.Preferences = updatedAllergies;

        HandleFoodSelection(personReservation, foodMenu, accountName);
        reservation.People.Add(personReservation);

        for (int i = 1; i < personCount; i++)
        {
            Console.Write($"Enter the name for person {i + 1}: ");
            string personName = Console.ReadLine();

            var otherPersonReservation = new PersonReservation(personName);

            Console.WriteLine($"\n{personName}, do you have any allergies? (y/n)");
            string hasOtherAllergies = Console.ReadLine();
            if (hasOtherAllergies == "y" || hasOtherAllergies == "Y")
            {
                var allergies = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your allergies (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText("[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(FoodMenu.GetAllergyOptions())
                );

                otherPersonReservation.Allergies.AddRange(allergies);
            }
            else
            {
                Console.WriteLine($"{personName} has no allergies.");
            }

            HandleFoodSelection(otherPersonReservation, foodMenu, personName);
            reservation.People.Add(otherPersonReservation);
        }

        var payment = new Payment();
        payment.StartPayment();

        Console.WriteLine($"\nDate: {formattedDate:dddd, MMMM dd, yyyy, hh:mm tt}\nStart Time: {time}\nEnd Time: {reservation.EndTime}\nAmount of persons: {personCount}\n");

        accountsLogic.AddReservation(email, reservation);

        Console.WriteLine($"\nReservation complete!");
        UserMenu.UserMenuStart();
    }

    private static void HandleFoodSelection(PersonReservation personReservation, FoodMenu foodMenu, string personName)
    {
        Console.WriteLine($"\n{personName}, do you want a blind experience? (y/n)");
        string chooseBlindExperience = Console.ReadLine();
        if (chooseBlindExperience == "y" || chooseBlindExperience == "Y")
        {
            personReservation.BlindExperience = true;
            Console.WriteLine($"{personName} has chosen a blind experience.");
            personReservation.Food.Add("SURPRISE-MEAL");
        }
        else
        {
            Console.WriteLine($"\n{personName}: Do you want to select your food? (y/n)");
            string chooseFood = Console.ReadLine();
            if (chooseFood == "y" || chooseFood == "Y")
            {
                foodMenu.DisplayFoodMenu();

                var selectedAppetizers = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your Appetizer(s) (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText(
                            "[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(foodMenu.GetAppetizersItems())
                        .Required(false)
                );

                var selectedSoupsAndSalads = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your Soups and Salads (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText(
                            "[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(foodMenu.GetSoupsandSaladsItems())
                        .Required(false)
                );

                var selectedMainCourses = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your Main course(s) (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText(
                            "[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(foodMenu.GetMainCourserItems())
                        .Required(false)
                );

                var selectedSideDishes = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your Side dishe(s) (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText(
                            "[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(foodMenu.GetSideDishesItems())
                        .Required(false)
                );

                var selectedDesserts = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your Dessert(s) (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText(
                            "[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(foodMenu.GetDessertsItems())
                        .Required(false)
                );

                var selectedDrinks = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"{personName}, select your Drink(s) (Press <enter> to skip):")
                        .PageSize(10)
                        .InstructionsText(
                            "[grey](Use <space> to toggle an item, <enter> to confirm your selections)[/]")
                        .AddChoices(foodMenu.GetDrinksItems())
                        .Required(false)
                );

                personReservation.Food.AddRange(selectedAppetizers);
                personReservation.Food.AddRange(selectedSoupsAndSalads);
                personReservation.Food.AddRange(selectedMainCourses);
                personReservation.Food.AddRange(selectedSideDishes);
                personReservation.Food.AddRange(selectedDesserts);
                personReservation.Food.AddRange(selectedDrinks);
            }
            else
            {
                Console.WriteLine($"{personName} chose not to select any food.");
                personReservation.Food.Add("Will choose in the restaurant");
            }
        }
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

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Are you sure you want to cancel this reservation?")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n"));

        if (confirmation)
        {
            accountsLogic.RemoveSpecificReservation(email, reservationSelection);

            Console.WriteLine("Reservation cancelled successfully.\n");
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
            Console.WriteLine("No reservations found.\n");
        }
        else
        {
            if (user.Reservations.Count == 1)
                Console.WriteLine("Your reservation:");
            else
                Console.WriteLine("Your reservations:");
            foreach (Reservation reservation in user.Reservations)
            {
                Console.WriteLine($"  - Date: {reservation.Date.ToString("dd-MM-yyyy")}\n  - Start Time: {reservation.Time}\n  - End Time: {reservation.EndTime}\n  - People: {reservation.PersonCount}\n  - Table: {reservation.TableNumber}\n");
            }
        }
        // UserMenu.UserMenuStart();
    }
}