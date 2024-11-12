using Spectre.Console;
public static class RestaurantInformation
{
    
    public static void PrintRestaurantInformation()
    {
<<<<<<< Updated upstream
        Console.WriteLine("Restaurant Name: xx ");
        Console.WriteLine("Address: Wijnhaven 61 3011WJ Rotterdam");
        Console.WriteLine("Phone Number: +31 10 223 4598");
        Console.WriteLine("Cuisine Type: Blind experience or A la Carte");
        Console.WriteLine("Cancellation Policy: You can always cancel your reservation in the application or cancel by calling.");
        Console.WriteLine("House Rules: Pets are allowed, but need to behave. \nNo takeaway");
        Console.WriteLine("Opening time Mon-Sun: 10:00 - 00-00");
=======
        Console.WriteLine("- Restaurant Name: xx ");
        Console.WriteLine("- Address: Wijnhaven 61 3011WJ Rotterdam");
        Console.WriteLine("- Phone Number: +31 10 223 4598");
        Console.WriteLine("- Cuisine Type: Blind experience or A la Carte");
        Console.WriteLine("- Cancellation Policy: You can always cancel your reservation in the application or cancel by calling.");
        Console.WriteLine("- House Rules: Pets are allowed, but need to behave. \n- No takeaway");
        Console.WriteLine("- Opening time: 10:00 - 00-00");
        Console.WriteLine("- 7 Days in a week open!\n");
>>>>>>> Stashed changes

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