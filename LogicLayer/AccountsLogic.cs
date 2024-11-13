using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

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
        // if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
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

    public void CreateGuestUser(string name, string email, string phone)
    {
        var user = new UserModel(name, email, phone) { IsGuest = true };
        UpdateList(user);
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
        var unavailableTables = _accounts
            .SelectMany(account => account.Reservations)
            .Where(r => r.Date == date && r.Time == time)
            .Select(r => r.TableNumber);

        return tables
            .Where(t => !unavailableTables.Contains(t.TableNumber) && t.Capacity >= personCount)
            .ToList();
    }
}