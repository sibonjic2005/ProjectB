using System.Security.Cryptography.X509Certificates;
using Spectre.Console;
static class CreateAccount
{
    public static void CreateAcc()
    {
        SignUpChecker sign_up_checker = new SignUpChecker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your Name: "));

        var dateofbirth = sign_up_checker.ValidateDate(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter Date of Birth (DD-MM-YYYY): ")));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your Address: "));

        var phonenumber = sign_up_checker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Phone Number: "))
        );

        var email = sign_up_checker.ValidateEmail(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email Address:"))
        );

        var password = sign_up_checker.PasswordRules(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Password: (Password must be at least 8 characters long, include at least one uppercase letter, one number, and one special character.)").Secret())
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

        // Console.WriteLine($"\nName: {name}, Birthday: {dateofbirth}, Address: {address} \nPhonenumber: {phonenumber}, Email: {email}, Password: {password}");
        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }

        // Create a new user
        accounts_logic.CreateUser(name, email, phonenumber, "User rights" ,password ,dateofbirth, address, allergies);

        Console.Clear();

        UserLogin.NewUserLogin(email, password);
    }

    public static void CreateGuestAcc()
    {
        SignUpChecker signUpChecker = new SignUpChecker();
        AccountsLogic accountsLogic = new AccountsLogic();

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your Email Address: "));

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your Name: "));

        var phonenumber = signUpChecker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Phone Number: "))
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
            new TextPrompt<string>("Enter a name: "));

        var dateofbirth = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter date of birth (DD-MM-YYYY): "));
        
        var address = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an address: "));

        var phonenumber = sign_up_checker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your phonenumber: "))
        );

        var email = sign_up_checker.ValidateEmail(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter an email address:"))
        );

        var password = sign_up_checker.PasswordRules(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter a password: ").Secret())
        );

        var rights = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What [green]rights[/] do you want this user to have?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to select rights)[/]")
            .AddChoices(new[] {
                "Admin rights", "Employee rights", "User rights"
            })
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

        accounts_logic.CreateUser(name, email, phonenumber, rights, password, dateofbirth, address, allergies);
        // accounts_logic.CreateAdmin(name, email, phonenumber, password, dateofbirth, address, allergies);
    }
}