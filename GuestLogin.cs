using Spectre.Console;
class GuestLogin
{
    public static void LoginGuest()
    {
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

        Console.WriteLine($"{email}{name}{phonenumber}");

        //call logic method and see if user exists
        //if exsist go to user menu
        //else no account exists
    }
}