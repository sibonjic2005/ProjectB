using Spectre.Console;
public static class RestaurantInformation
{
    
    public static void PrintRestaurantInformation()
    {
        Console.WriteLine("Restaurant Name: xx ");
        Console.WriteLine("Address: Wijnhaven 61 3011WJ Rotterdam");
        Console.WriteLine("Phone Number: +31 10 223 4598");
        Console.WriteLine("Cuisine Type: Blind experience or A la Carte");
        Console.WriteLine("Cancellation Policy: You can always cancel your reservation in the application or cancel by calling.");

         var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the starting menu?")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Go back"            
                }));   
                
        if (choices == "Go back")
        {
            StartingMenu.Menu();
        }
    }
}