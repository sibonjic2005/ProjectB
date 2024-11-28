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
        SignUpChecker signUpChecker = new SignUpChecker();

        var user = accountsLogic.GetByEmail(AccountsLogic.CurrentAccount?.EmailAddress);

        List<string> allergyOptions = AccountsLogic.GetAllergyOptions();

        var updatedName = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter Name [grey](current: {user.Name})[/]:").AllowEmpty());

        var updatedPhone = signUpChecker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>($"Enter your Phone Number [grey](current: {user.PhoneNumber})[/]").AllowEmpty()));

        var updatedAllergies = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Update allergies [grey](use <space> to select/deselect)[/]:")
                .NotRequired()
                .PageSize(10)
                .AddChoices(allergyOptions));

        accountsLogic.EditUserInfo(updatedName, updatedPhone, updatedAllergies);
        AnsiConsole.MarkupLine("[green]Profile updated successfully![/]\n");
    }

    public static void DeleteAccount()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        accountsLogic.UserDeleteAccount();
    }
}