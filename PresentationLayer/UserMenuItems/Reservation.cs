using Spectre.Console;
static class Reservation
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
        
    }

    public static void CancelReservation()
    {
        // hierzo code om te laten zien over welke reservering het gaat
        // date: \n time: \ person:
        // use the json file i guess
        var confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>("Do you want to cancel your reservation?")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(false)
            .WithConverter(choice => choice ? "y" : "n"));

        Console.WriteLine(confirmation ? "Confirmed, reservation cancelled." : "Declined, reservation is still there.");
    }

    public static void ViewReservation()
    {

    }
}