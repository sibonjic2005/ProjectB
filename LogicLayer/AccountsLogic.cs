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

    public void CreateUser(string name, string email, string phone, string password = "", string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var user = new UserModel(name, email, phone, password, dateOfBirth, address, preferences);
        UpdateList(user);
    }

    public void CreateGuestUser(string name, string email, string phone)
    {
        var user = new UserModel(name, email, phone) { IsGuest = true };
        UpdateList(user);
    }

    public void CreateAdmin(string name, string email, string phone, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var admin = UserModel.CreateAdmin(name, email, phone, password, dateOfBirth, address, preferences);
        UpdateList(admin);
    }

    public void AddNewReservation(string name, string email, string phone, List<string> preferences, Dictionary<string, string> reservation)
    {
        var user = GetByEmail(email);
        if (user != null)
        {
            user.Reservations.Add(reservation);
            UpdateList(user);
        }
        else
        {
            CreateUser(name, email, phone, "NAjnjdnjn1241RFW@R$#%#$%#GERegnrejgnjr", "", "", preferences);
            var newUser = GetByEmail(email);
            if (newUser != null)
            {
                newUser.Reservations.Add(reservation);
                UpdateList(newUser);
            }
        }
    }

    public void AddReservation(string email, Dictionary<string, string> reservation)
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
}