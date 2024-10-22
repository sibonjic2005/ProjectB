using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

class AccountsLogic
{
    private List<UserModel> _accounts;

    public static UserModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
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
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        CurrentAccount = _accounts.Find(i =>
            i.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            i.Password == password);

        return CurrentAccount;

    }

    public void CreateUser(string name, string email, string phone, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var user = new UserModel(name, email, phone, password, dateOfBirth, address, preferences);
        UpdateList(user);
    }

    public void CreateAdmin(string name, string email, string phone, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var admin = UserModel.CreateAdmin(name, email, phone, password, dateOfBirth, address, preferences);
        UpdateList(admin);
    }
}