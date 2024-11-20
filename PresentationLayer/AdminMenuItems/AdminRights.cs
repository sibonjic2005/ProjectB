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

        accountsLogic.ChangeRights(account, newUserRights);

    }

    public static void DeleteAccount()
    {
        Console.WriteLine("Enter the email of the account you want to delete:");
        string getEmail = Console.ReadLine();

        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.GetByEmail(getEmail);
    
        if (user == null)
        {
            Console.WriteLine("No user found");
            return;
        }

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>("Are you sure you want to delete your account? This action cannot be undone. (yes/no)")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n"));

        if (confirmation)
        {
            //accountsLogic.AdminDeleteAccount();
        }
        else //dont delete the account and return to the previous admin menu
        {

        }
    }
}