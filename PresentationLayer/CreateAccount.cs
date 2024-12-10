using Spectre.Console;
static class CreateAccount
{
    public static void CreateAcc()
    {
        SignUpChecker signUpChecker = new SignUpChecker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = signUpChecker.ValidateName(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Name: ")));


        var dateOfBirth = signUpChecker.ValidateDate(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter Date of Birth (DD-MM-YYYY): ").AllowEmpty()));
        
        var address = signUpChecker.ValidateAddress(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Address: ").AllowEmpty()));

        var phoneNumber = signUpChecker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Phone Number: "))
        );
        string email;

        while (true)
        {
            email = signUpChecker.ValidateEmail(() => 
                AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email Address:"))
            );
            if (!accounts_logic.EmailExists(email))
            {
                break;
            }
                EmailInUse();
        }

        var password = signUpChecker.PasswordRules(() => 
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

        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }

        accounts_logic.CreateUser(name, email, phoneNumber, "User rights", password, dateOfBirth, address, allergies);

        Console.Clear();

        UserLogin.NewUserLogin(email, password);
    }

    public static void CreateGuestAcc()
    {
        SignUpChecker signUpChecker = new SignUpChecker();
        AccountsLogic accountsLogic = new AccountsLogic();

        var name = signUpChecker.ValidateName(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Name: ")));

        var dateOfBirth = signUpChecker.ValidateDate(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter Date of Birth (DD-MM-YYYY): ").AllowEmpty()));
        
        var address = signUpChecker.ValidateAddress(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Address: ").AllowEmpty()));

        var phoneNumber = signUpChecker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter your Phone Number: "))
        );

        string email;

        while (true)
        {
            email = signUpChecker.ValidateEmail(() => 
                AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email Address:"))
            );
            if (!accountsLogic.EmailExists(email))
            {
                break;
            }
                EmailInUse();
        }

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

        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }

        accountsLogic.CreateGuestUser(name, email, phoneNumber, "" ,dateOfBirth, address, allergies);

        Console.Clear();
        UserLogin.NewUserLogin(email, "");
    }

    public static void CreateAdmin()
    {
        SignUpChecker signUpChecker = new SignUpChecker();
        AccountsLogic accounts_logic = new AccountsLogic();

        var name = signUpChecker.ValidateName(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter a Name: ")));

        var dateOfBirth = signUpChecker.ValidateDate(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter a Date of Birth (DD-MM-YYYY): ")));
        
        var address = signUpChecker.ValidateAddress
        (() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter an Address: ")));


        var phoneNumber = signUpChecker.PhoneNumberRules(() =>
            AnsiConsole.Prompt(new TextPrompt<string>("Enter a Phone Number: "))
        );

        string email;

        while (true)
        {
            email = signUpChecker.ValidateEmail(() => 
                AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email Address:"))
            );
            if (!accounts_logic.EmailExists(email))
            {
                break;
            }
                EmailInUse();
        }

        var password = signUpChecker.PasswordRules(() => 
            AnsiConsole.Prompt(new TextPrompt<string>("Enter a Password: ").Secret())
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

        accounts_logic.CreateUser(name, email, phoneNumber, rights, password, dateOfBirth, address, allergies);

        Console.Clear();

        AnsiConsole.MarkupLine($"[green]User ({name}) created successfully[/]");

        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "continue"            
                }));   
                
        if (choices == "continue")
        {
            Console.Clear();
        }
    }

    public static void EmailInUse()
    {
        Console.WriteLine("An account with this email already exists.");
    }
}