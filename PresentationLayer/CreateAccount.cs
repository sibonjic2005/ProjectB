using Spectre.Console;
static class CreateAccount
{
    public static void CreateAcc()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your name: "));

        var phonenumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your phonenumber: "));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your email address: "));

        var birthday = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter birthday (DD-MM-YYYY): "));

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

        Console.WriteLine($"\nName: {name}, Phonenumber: {phonenumber}, Email: {email} \nBirthday: {birthday}, Password: {password}");
        foreach (string allergy in allergies)
        {
            AnsiConsole.WriteLine(allergy);
        }
    }
}