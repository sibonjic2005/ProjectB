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


        Console.WriteLine($"\nDate: {date}, Cancelation: {time}, View: {person}");
        
    }
}