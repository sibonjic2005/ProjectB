using Spectre.Console;

public class UserInfo()
{
    public static void ViewProfile()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        var user = accountsLogic.GetByEmail(AccountsLogic.CurrentAccount?.EmailAddress);

        AnsiConsole.MarkupLine("[bold cyan]Your Profile Information:[/]");
        Console.WriteLine($"Name: {user.Name}");
        if (user.DateOfBirth.Length > 0)
            Console.WriteLine($"Date of birth: {user.DateOfBirth}");
        Console.WriteLine($"Email: {user.EmailAddress}");
        Console.WriteLine($"Phone: {user.PhoneNumber}");
        if (user.Preferences.Count > 0 )
            Console.WriteLine($"Allergies: {accountsLogic.GetAllergies()}\n");
    }

    public static void EditProfile()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        var user = accountsLogic.GetByEmail(AccountsLogic.CurrentAccount?.EmailAddress);

        List<string> allergyOptions = AccountsLogic.GetAllergyOptions();

        var updatedName = AnsiConsole.Prompt(
                    new TextPrompt<string>($"Enter new name [grey](current: {user.Name})[/]:")
                        .AllowEmpty());
        var updatedPhone = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter new phone number [grey](current: {user.PhoneNumber})[/]:")
                .AllowEmpty());
        var updatedAllergies = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Update allergies [grey](use <space> to select/deselect)[/]:")
                .NotRequired()
                .PageSize(10)
                .AddChoices(allergyOptions));

        accountsLogic.EditUserInfo(updatedName, updatedPhone, updatedAllergies);
        AnsiConsole.MarkupLine("[green]Profile updated successfully![/]\n");
    }
}