using Spectre.Console;
public static class RestaurantInformation
{
    
    public static void PrintRestaurantInformation()
    {
        AnsiConsole.MarkupLine("[maroon]Restaurant Name:[/] Blind Ate ");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]Address:[/] Wijnhaven 61 3011WJ Rotterdam");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]Phone Number:[/] +31 10 223 4598");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]Cuisine Type:[/] Blind experience or A la Carte");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]Cancellation Policy:[/] You can always cancel your reservation in the application or cancel by calling.");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]Payment:[/] IDeal or Credit card");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]House Rules:[/]");
        AnsiConsole.MarkupLine("[blue3]House Rule 1:[/] No takeaway.");
        AnsiConsole.MarkupLine("[blue3]House Rule 2:[/] Pets are allowed, but need to behave.");
        AnsiConsole.MarkupLine("[blue3]House Rule 2:[/] We do not serve alcohol under the age of 18.");
        AnsiConsole.MarkupLine("[blue3]House Rule 3:[/] No smoking in the restaurant");
        AnsiConsole.MarkupLine("[blue3]House Rule 4:[/] Kindly keep your personal belongings with you; we are not responsible for lost items.");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]Opening time Mon-Sun:[/] 10:00 - 00-00");
    }
}