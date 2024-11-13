using Spectre.Console;
class ClientInfo
{
    public static void ViewClientInfo()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var allUsers = accountsLogic.LoadAllUsers();
        List<string> allUsersMail = accountsLogic.LoadAllUsersMail();

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a user you want to view:")
                .PageSize(10)
                .AddChoices(allUsersMail)
        );

        UserModel account = accountsLogic.GetByEmail(userMail);

        Console.WriteLine($"\nName: {account.Name}, \nBirthday: {account.DateOfBirth}, \nAddress: {account.Address} \nPhonenumber: {account.PhoneNumber}, \nEmail: {account.EmailAddress}, \nPassword: {account.Password}");
        foreach (string allergy in account.Preferences)
        {
            AnsiConsole.WriteLine($"Allergic to: {allergy}");
        }
    }
}