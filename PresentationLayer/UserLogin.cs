using Spectre.Console;
static class UserLogin
{
    public static void Login()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your phonenumber: "));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your email address: "));

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your password: ")
                .Secret());

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


        Console.WriteLine($"{name}{phonenumber}{email}{password}");

        //call logic method and see if user exists
        //if exsist go to user menu
        //else no account exists
    }
}