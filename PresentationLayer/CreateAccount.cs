using System.Security.Cryptography.X509Certificates;
using Spectre.Console;
static class CreateAccount
{
    public static void CreateAcc()
    {
        SignUpChecker sign_up_checker = new SignUpChecker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var dateofbirth = sign_up_checker.ValidateDate(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter date of birth (DD-MM-YYYY): ")));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your address: "));

        var phonenumber = sign_up_checker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your phonenumber: "))
        );

        var email = sign_up_checker.ValidateEmail(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your email address:"))
        );

        var password = sign_up_checker.PasswordRules(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your password: (Password must be at least 8 characters long, include at least one uppercase letter, one number, and one special character.)").Secret())
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

    public static void CreateGuestAcc()
    {
        SignUpChecker signUpChecker = new SignUpChecker();
        AccountsLogic accountsLogic = new AccountsLogic();

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your email address: "));

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your phonenumber: "));

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

    // Write the selected fruits to the terminal
        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }

        Console.WriteLine($"Email: {email}\nName:{name}\nPhone number:{phonenumber}");
    
    }

    public static void CreateAdmin()
    {
        SignUpChecker sign_up_checker = new SignUpChecker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var dateofbirth = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter date of birth (DD-MM-YYYY): "));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your address: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your phonenumber: "));

        var email = sign_up_checker.ValidateEmail(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your email address:"))
        );

        var password = sign_up_checker.PasswordRules(() => 
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