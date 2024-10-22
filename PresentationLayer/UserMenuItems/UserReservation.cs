using Spectre.Console;
static class UserReservation
{
    public static void MakeReservation()
    {
        var date = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a date: "));

        var time = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a time: "));
        
        var person = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter an amount of people: "));


        Console.WriteLine($"\nDate: {date}, Time: {time}, Amount of persons: {person}");
        Console.WriteLine($"\nReservation complete!");
        UserMenu.UserMenuStart();
    }

    public static void CancelReservation()
    {
        // ViewReservation() aan roepen
        // hierzo code om te laten zien over welke reservering het gaat
        // date: \n time: \ person:
        // use the json file i guess
        var confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>("Do you want to cancel your reservation?")
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        Console.WriteLine(confirmation ? "Confirmed, reservation cancelled." : "Declined, reservation is still there.");
        UserMenu.UserMenuStart();
    }

    public static void ViewReservation()
    {
        Console.WriteLine("beep boop");
    }

    public static void Calendar()
    {
        var calendar = new Calendar(2024,10);
        AnsiConsole.Write(calendar);
    }
}