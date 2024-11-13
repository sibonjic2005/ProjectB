using Spectre.Console;
class ClientInfo
{
    public static void ViewClientInfo()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
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

    public static void ChangeClientInfo()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        List<string> allUsersMail = accountsLogic.LoadAllUsersMail();

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a user you want to view:")
                .PageSize(10)
                .AddChoices(allUsersMail)
        );

        var user = accountsLogic.GetByEmail(userMail);
            if (user == null)
            {
                Console.WriteLine("No reservation found with the given email.");
                // AdminMenu.AdminMenuStart();
                return;
            }

        
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a Name: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a phone number: "));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an email: "));

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an password: "));

        var dateOfBirth = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an date of birth: "));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an address: "));

        var allergies = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Do have any allergies?")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to choose your allergy, " + 
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(new[] {
                    "Tree Nuts", "Soy", "Fish",
                    "Peanuts", "Shellfish", "Eggs",
                    "Wheats", "Dairy"
        }));


        accountsLogic.UpdateChangesClientInfo(userMail, newName, phonenumber, email, password, dateOfBirth, address, allergies);
        Console.WriteLine($"\nName: {newName}, Phone Number {phonenumber}");
        Console.WriteLine($"\nReservation complete!");
        
    }
}