using Spectre.Console;
static class CreateAccount
{
    public static void CreateAcc()
    {
        Log_in_Checker log_in_checker = new Log_in_Checker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var dateofbirth = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter date of birth (DD-MM-YYYY): "));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your address: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your phonenumber: "));

        var email = log_in_checker.ValidateEmail(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your email address:"))
        );

        var password = log_in_checker.PasswordRules(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your password: ").Secret())
        );

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

        Console.WriteLine($"\nName: {name}, Birthday: {dateofbirth}, Address: {address} \nPhonenumber: {phonenumber}, Email: {email}, Password: {password}");
        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }

        // Create a new user
        accounts_logic.CreateUser(name, email, phonenumber, password, dateofbirth, address, allergies);

        UserLogin.NewUserLogin(email, password);
    }

    public static void CreateAdmin()
    {
        Log_in_Checker log_in_checker = new Log_in_Checker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var dateofbirth = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter date of birth (DD-MM-YYYY): "));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your address: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your phonenumber: "));

        var email = log_in_checker.ValidateEmail(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your email address:"))
        );

        var password = log_in_checker.PasswordRules(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your password: ").Secret())
        );

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

        Console.WriteLine($"\nName: {name}, Birthday: {dateofbirth}, Address: {address} \nPhonenumber: {phonenumber}, Email: {email}, Password: {password}");
        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }

        accounts_logic.CreateAdmin(name, email, phonenumber, password, dateofbirth, address, allergies);
    }
}