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

        var account = accountsLogic.GetByEmail(userMail);

        Console.WriteLine($"\nName: {account.Name}\nBirthday: {account.DateOfBirth}\nAddress: {account.Address}\nPhone Number: {account.PhoneNumber}\nEmail: {account.EmailAddress}");

        foreach (string allergy in account.Preferences)
        {
            AnsiConsole.WriteLine($"Allergic to: {allergy}");
        }

        GoBack.GoBackClientOption();
    }

    public static void ChangeClientInfo()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        List<string> allUsersMail = accountsLogic.LoadAllUsersMail();

        var userMail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a user you want to change:")
                .PageSize(10)
                .AddChoices(allUsersMail)
        );

        var user = accountsLogic.GetByEmail(userMail);
       
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter a Name: [grey](press enter to keep {user.Name})[/]").AllowEmpty());

        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter a phone number: [grey](press enter to keep {user.PhoneNumber})[/]").AllowEmpty());

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter an email: [grey](press enter to keep {user.EmailAddress})[/]").AllowEmpty());
            
        var dateOfBirth = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter an date of birth: [grey](press enter to keep {user.DateOfBirth})[/]").AllowEmpty());
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter an address: [grey](press enter to keep {user.Address})[/]").AllowEmpty());

        var allergies = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Does the client have any allergies?")
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

        accountsLogic.UpdateChangesClientInfo(userMail, newName, phoneNumber, email, dateOfBirth, address, allergies);

        Console.WriteLine($"\nChanges made!");
        
        GoBack.GoBackClientOption();
    }
}