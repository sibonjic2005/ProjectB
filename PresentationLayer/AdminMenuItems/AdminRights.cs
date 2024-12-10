using System.Net.Sockets;
using Spectre.Console;
class AdminRights
{
    public static void PromoteDemoteUser()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var allUsers = accountsLogic.LoadAllUsers();
        List<string> allUsersMail = accountsLogic.LoadAllUsersMail();

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a user you want to [green]promote[/]/[red]demote[/]:")
                .PageSize(10)
                .AddChoices(allUsersMail)
        );

        UserModel account = accountsLogic.GetByEmail(userMail);
        string userRights = accountsLogic.CheckUserRights(account);

        Console.WriteLine($"Rights of {account.Name}: {userRights}\n");

        var newUserRights = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"What rights should {account.Name} have?")
                .PageSize(10)
                .AddChoices(new[] {
                    "Admin", "Employee", "User"
                })
        );

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Are you sure you want to promote/demote this user?")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n"));
                
        if (confirmation)
        {
            accountsLogic.ChangeRights(account, newUserRights);
            AnsiConsole.MarkupLine("[green]User right changed successfully.[/]");
            GoBack.GoBackAdminMenu();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Changing User rights cancelled.[/]");
            GoBack.GoBackAdminMenu();
        }
    }

    public static void DeleteAccount()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var allUsers = accountsLogic.LoadAllUsers();
        List<string> allUsersMail = accountsLogic.LoadAllUsersMail();
        allUsersMail.Add("[yellow]Go back[/]");

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the user you want to [red]delete[/]:")
                .PageSize(10)
                .AddChoices(allUsersMail)
        );

        if (userMail == "[yellow]Go back[/]")
        {
            AdminMenu.AdminMenuStart();
        }

        accountsLogic.AdminDeleteAccount(userMail);
        GoBack.GoBackAdminMenu();
    }
}