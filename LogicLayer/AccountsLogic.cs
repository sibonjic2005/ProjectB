class AccountsLogic
{
    private List<UserModel> _accounts;

    public static UserModel? CurrentAccount { get; set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }

    public List<UserModel> LoadAllUsers()
    {
        return _accounts;
    }

    public List<string> LoadAllUsersMail()
    {
        List<string> accountMails = new List<string>();

        foreach (UserModel user in _accounts)
        {
            accountMails.Add(user.EmailAddress);
        }
        return accountMails;
    }

    public string CheckUserRights(UserModel user)
    {
        if (user.IsAdmin)
            return "Admin";
        else if (user.IsEmployee)
            return "Employee";

        return null;
    }

    public void UpdateList(UserModel account)
    {
        int index = _accounts.FindIndex(s => s.EmailAddress.Equals(account.EmailAddress, StringComparison.OrdinalIgnoreCase));

        if (index != -1)
        {
            _accounts[index] = account;
        }
        else
        {
            _accounts.Add(account);
        }
        AccountsAccess.WriteAll(_accounts);
    }

    public UserModel GetByEmail (string email)
    {
        return _accounts.Find(i => i.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public UserModel? CheckLogin(string email, string password)
    {
        if (string.IsNullOrEmpty(email))
        {
            return null;
        }

        CurrentAccount = _accounts.Find(i =>
            i.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            i.Password == password);

        return CurrentAccount;

    }

    public void CreateUser(string name, string email, string phone, string rights, string password = "", string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        if (rights == "Employee rights")
        {
            CreateEmployee(name, email, phone, password, dateOfBirth, address, preferences);
        }
        else if (rights == "Admin rights")
        {
            CreateAdmin(name, email, phone, password, dateOfBirth, address, preferences);
        }
        else
        {
            var user = new UserModel(name, email, phone, password, dateOfBirth, address, preferences);
            UpdateList(user);
        }
        
    }

    public void CreateGuestUser(string name, string email, string phone, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var guest = new UserModel(name, email, phone, "", dateOfBirth, address, preferences)
        {
            IsGuest = true
        };
        UpdateList(guest);
    }

    public void CreateEmployee(string name, string email, string phone, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var employee = UserModel.CreateEmployee(name, email, phone, password, dateOfBirth, address, preferences);
        UpdateList(employee);
    }

    public void CreateAdmin(string name, string email, string phone, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var admin = UserModel.CreateAdmin(name, email, phone, password, dateOfBirth, address, preferences);
        UpdateList(admin);
    }

    public void ChangeRights(UserModel user, string rights)
    {
        switch (rights)
        {
            case "Admin":
                user.IsAdmin = true;
                user.IsEmployee = false;
                user.IsGuest = false;
                UpdateList(user);
                break;
            case "Employee":
                user.IsAdmin = false;
                user.IsEmployee = true;
                user.IsGuest = false;
                UpdateList(user);
                break;
            case "User":
                user.IsAdmin = false;
                user.IsEmployee = false;
                user.IsGuest = false;
                UpdateList(user);
                break;
            default:
                break;
        }
    }

    public void AddNewReservation(string name, string email, string phone, List<string> preferences, Reservation reservation)
    {
        var user = GetByEmail(email);
        if (user != null)
        {
            user.Reservations.Add(reservation);
            UpdateList(user);
        }
        else
        {
            CreateUser(name, email, phone, "NAjnjdnjn1241RFW@R$#%#$%#GERegnrejgnjr", "User rights", "", "", preferences);
            var newUser = GetByEmail(email);
            if (newUser != null)
            {
                newUser.Reservations.Add(reservation);
                UpdateList(newUser);
            }
        }
    }

    public void AddReservation(string email, Reservation reservation)
    {
        var user = GetByEmail(email);
        if (user != null)
        {
            user.Reservations.Add(reservation);
            UpdateList(user);

            if (CurrentAccount != null && CurrentAccount.EmailAddress == email)
            {
                CurrentAccount.Reservations.Add(reservation);
            }
        }
    }

    public void RemoveReservations(string email)
    {
        var user = GetByEmail(email);
        if (user != null)
        {
            user.Reservations.Clear();
            UpdateList(user);

            if (CurrentAccount != null & CurrentAccount.EmailAddress == email)
            {
                CurrentAccount.Reservations.Clear();
            }
        }
    }

    public void RemoveSpecificReservation(string email, Reservation reservation)
    {
        var user = GetByEmail(email);
        if (user != null)
        {
            user.Reservations.Remove(reservation);
            UpdateList(user);

            if (CurrentAccount != null && CurrentAccount.EmailAddress == email)
            {
                CurrentAccount.Reservations.Remove(reservation);
            }
        }
    }

    public void UpdateReservation(string email, Reservation updatedReservation)
    {
        var user = GetByEmail(email);
        if (user != null)
        {
            var reservation = user.Reservations.FirstOrDefault(r => r.Equals(updatedReservation));
            if (reservation != null)
            {
                reservation.Date = updatedReservation.Date;
                reservation.Time = updatedReservation.Time;
                reservation.PersonCount = updatedReservation.PersonCount;
                reservation.TableNumber = updatedReservation.TableNumber;
                UpdateList(user);
            }
        }
    }

    public void UpdateChangesReservation(string email, Reservation reservation)
    {
        var user = GetByEmail(email);
        if (user != null && user.Reservations != null)
        {
            
            user.Reservations[0].Date = reservation.Date;
            user.Reservations[0].Time = reservation.Time;
            user.Reservations[0].PersonCount = reservation.PersonCount;
            user.Reservations[0].TableNumber = reservation.TableNumber;
            UpdateList(user);
        }
    }

    public void UpdateChangesClientInfo(string email, string newName, string newPhoneNumber, string newEmail, string newDateOfBirth, string newAddress, List<string> preferences)
    {
        var user = GetByEmail(email);
        if (user != null && user.Reservations != null)
        {
            user.Name = string.IsNullOrEmpty(newName) ? user.Name : newName;
            user.EmailAddress = string.IsNullOrEmpty(newEmail) ? user.EmailAddress : newEmail;
            user.PhoneNumber = string.IsNullOrEmpty(newPhoneNumber) ? user.PhoneNumber : newPhoneNumber;
            user.DateOfBirth = string.IsNullOrEmpty(newDateOfBirth) ? user.DateOfBirth : newDateOfBirth;
            user.Address = string.IsNullOrEmpty(newAddress) ? user.Address : newAddress;
            user.Preferences = preferences; // allergies
            UpdateList(user);
        }
    }

    public static List<Tables> InitializeTables()
    {
        List<Tables> tables = new List<Tables>();

        for (int i = 1; i <= 8; i++)
            tables.Add(new Tables(i, 2));

        for (int i = 9; i<= 13; i++)
            tables.Add(new Tables(i, 4));
        
        for (int i = 14; i <= 15; i++)
            tables.Add(new Tables(i, 6));
        
        for (int i = 16; i <= 23; i++)
            tables.Add(new Tables(i, 1));

        return tables;
    }

    public List<Tables> GetAvailableTables(DateTime date, string time, int personCount)
    {
        var tables = InitializeTables();

        int selectedHour = int.Parse(time.Split(':')[0]);
        DateTime selectedStartTime = date.Date.AddHours(selectedHour);
        DateTime selectedEndTime = selectedStartTime.AddHours(selectedHour == 20 ? 3 : 2);

        var unavailableTables = _accounts
            .SelectMany(account => account.Reservations)
            .Where(reservation =>
            {
                int reservationHour = int.Parse(reservation.Time.Split(':')[0]);
                DateTime reservationStartTime = reservation.Date.Date.AddHours(reservationHour);
                DateTime reservationEndTime = reservationStartTime.AddHours(reservationHour == 20 ? 3 : 2);

                return reservation.Date.Date == date.Date &&
                    !(selectedEndTime <= reservationStartTime || selectedStartTime >= reservationEndTime);
            })
            .Select(r => r.TableNumber);

        var availableTables = tables
            .Where(table => !unavailableTables.Contains(table.TableNumber) && table.Capacity >= personCount)
            .OrderBy(table => table.Capacity)
            .ToList();

        if (!availableTables.Any())
        {
            return new List<Tables>();
        }

        int minCapacity = availableTables.First().Capacity;
        return availableTables.Where(table => table.Capacity == minCapacity).ToList();
    }


    public bool HasReservationForTimeSlot(string email, DateTime date, string time)
    {
        var user = GetByEmail(email);
        if (user == null)
            return false;
        
        return user.Reservations.Any(reservation => reservation.Date.Date == date.Date && reservation.Time == time || reservation.Date.Date == date.Date && Convert.ToInt32(reservation.Time.Split(':')[0]) + 1 == Convert.ToInt32(time.Split(':')[0]));
    }

    public bool HasReservationForDay(string email, DateTime date)
    {
        var user = _accounts.FirstOrDefault(account => account.EmailAddress == email);

        if (user == null || user.Reservations == null)
            return false;

        return user.Reservations.Any(reservation => reservation.Date.Date == date.Date);
    }

    public string? GetAllergies()
    {
        if (CurrentAccount?.Preferences.Count > 0)
            return string.Join(", ",  CurrentAccount.Preferences);
        else
            return null;
    }
//     public List<string> AllergyOptions()
// {
//     if (CurrentAccount?.Preferences == null)
//     {
//         CurrentAccount.Preferences = new List<string>();
//     }

//     var allergyOptions = new List<string>
//     {
//         CurrentAccount.Preferences.Contains("Tree Nuts") ? "[X] Tree Nuts" : "[ ] Tree Nuts",
//         CurrentAccount.Preferences.Contains("Soy") ? "[X] Soy" : "[ ] Soy",
//         CurrentAccount.Preferences.Contains("Fish") ? "[X] Fish" : "[ ] Fish",
//         CurrentAccount.Preferences.Contains("Peanuts") ? "[X] Peanuts" : "[ ] Peanuts",
//         CurrentAccount.Preferences.Contains("Shellfish") ? "[X] Shellfish" : "[ ] Shellfish",
//         CurrentAccount.Preferences.Contains("Eggs") ? "[X] Eggs" : "[ ] Eggs",
//         CurrentAccount.Preferences.Contains("Wheats") ? "[X] Wheats" : "[ ] Wheats",
//         CurrentAccount.Preferences.Contains("Dairy") ? "[X] Dairy" : "[ ] Dairy"
//     };

//     return allergyOptions;
// }

    // public List<string> CleanAllergyOptions(List<string> allergies)
    // {
    //     return allergies
    //         .Select(a => a.Replace("[X] ", "").Replace("[ ] ", ""))
    //         .ToList();
    // }

    public static List<string> GetAllergyOptions()
    {
        return new List<string> {
                    "Tree Nuts", "Soy", "Fish",
                    "Peanuts", "Shellfish", "Eggs",
                    "Wheats", "Dairy"
        };
    }

    public void EditUserInfo(string name, string phone, string address, List<string> allergies)
    {
        if (!string.IsNullOrWhiteSpace(name)) CurrentAccount.Name = name;
        if (!string.IsNullOrWhiteSpace(phone)) CurrentAccount.PhoneNumber = phone;
        if (!string.IsNullOrWhiteSpace(address)) CurrentAccount.Address = address;
        CurrentAccount.Preferences = allergies;

        UpdateList(CurrentAccount);
    }

    public void AdminDeleteAccount(string email)
    {
        var userToDelete = GetByEmail(email);
        if (userToDelete != null)
        {
            Console.WriteLine($"Are you sure you want to delete the following account? (yes/no)");
            Console.WriteLine($"Name: {userToDelete.Name}");
            Console.WriteLine($"Email: {userToDelete.EmailAddress}");

        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation == "yes")
        {
            _accounts.Remove(userToDelete);
            AccountsAccess.WriteAll(_accounts);
            Console.WriteLine($"Account with email {email} has been successfully deleted.");
        }
        else
        {
            Console.WriteLine("Operation canceled. No account was deleted.");
        }
    }
    else
    {
        Console.WriteLine($"No account found with the email {email}.");
    }
}

    public void UserDeleteAccount()
    {
        if (CurrentAccount == null)
        {
            Console.WriteLine("No user is currently logged in.");
            return;
        }

        Console.WriteLine($"Are you sure you want to delete your account? This action cannot be undone. (yes/no)");
        Console.WriteLine($"Name: {CurrentAccount.Name}");
        Console.WriteLine($"Email: {CurrentAccount.EmailAddress}");

        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation == "yes")
        {
            var email = CurrentAccount.EmailAddress;
            var userToDelete = GetByEmail(email);

            if (userToDelete != null)
            {
                _accounts.Remove(userToDelete);
                AccountsAccess.WriteAll(_accounts);
                CurrentAccount = null; // Log the user out
                Console.WriteLine("Your account has been successfully deleted.");
                Console.ReadLine();
                StartingMenu.Menu();
            }
            else
            {
                Console.WriteLine("An error occurred while trying to delete your account.");
            }
        }
        else
        {
            Console.WriteLine("Operation canceled. Your account was not deleted.");
        }
    }

}