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
        if (user.Address.Length > 0)
        {
            Console.WriteLine($"Address: {user.Address}");
        }
        if (user.Preferences.Count > 0 )
        {
            Console.WriteLine($"Allergies: {accountsLogic.GetAllergies()}\n");
        }
        else
        {
            Console.WriteLine("Allergies: None");
        }
    }

    public static void EditProfile()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        SignUpChecker signUpChecker = new SignUpChecker();

        var user = accountsLogic.GetByEmail(AccountsLogic.CurrentAccount?.EmailAddress);

        List<string> allergyOptions = AccountsLogic.GetAllergyOptions();
        List<string> userAllergies = user.Preferences;

        var updatedName = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter Name [grey](current: {user.Name})[/]:").AllowEmpty());

        var updatedPhone = signUpChecker.EditPhoneNumber(() =>
            AnsiConsole.Prompt(new TextPrompt<string>($"Enter your Phone Number [grey](current: {user.PhoneNumber})[/]").AllowEmpty()));

        var updatedAddress = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter your Address [grey](current: {user.Address})[/]").AllowEmpty());

        var allergyPrompt = new MultiSelectionPrompt<string>()
                .Title("Update allergies [grey](use <space> to select/deselect)[/]:")
                .NotRequired()
                .PageSize(10)
                .AddChoices(allergyOptions);
        
        foreach (var allergy in userAllergies.Where(allergyOptions.Contains))
        {
            allergyPrompt.Select(allergy);
        }

        var updatedAllergies = AnsiConsole.Prompt(allergyPrompt);

        accountsLogic.EditUserInfo(updatedName, updatedPhone, updatedAddress, updatedAllergies);
        AnsiConsole.MarkupLine("[green]Profile updated successfully![/]\n");
    }

    public static void DeleteAccount()
    {
        AccountsLogic accountsLogic = new AccountsLogic();

        accountsLogic.UserDeleteAccount();
    }
}