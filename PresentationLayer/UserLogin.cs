using System.ComponentModel.DataAnnotations;
using Spectre.Console;
static class UserLogin
{
    public static void Login()
    {
        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your email address:"));

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your password:").Secret());

        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.CheckLogin(email, password);

        if (user != null)
        {
            Console.WriteLine($"Welcome back, {user.Name}!");
            AccountsLogic.CurrentAccount = user;
            UserMenu.UserMenuStart();
        }
        else
        {
            Console.WriteLine("Invalid login credentials");
            StartingMenu.Menu();
        }
    }

    public static void NewUserLogin(string email, string password)
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.CheckLogin(email, password);

        if (user != null)
        {
            Console.WriteLine($"Welcome {user.Name}!");
            AccountsLogic.CurrentAccount = user;
            UserMenu.UserMenuStart();
        }
        else
        {
            Console.WriteLine("Invalid field(s), please try again.");
            StartingMenu.Menu();
        }
    }
}