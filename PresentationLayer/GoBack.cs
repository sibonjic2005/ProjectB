using Spectre.Console;

static class GoBack
{
    public static void GoBackUserMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Press Enter to go back"            
                }));   
                
        if (choices == "Press Enter to go back")
        {
            Console.Clear();
            UserMenu.UserMenuStart();
        }
    }
    public static void GoBackStartingMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Press Enter to go back"            
                }));   
                
        if (choices == "Press Enter to go back")
        {
            Console.Clear();  
            StartingMenu.Menu();
        }
    }

    public static void GoBackProfileMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        "Press Enter to go back"            
                }));   
                
        if (choices == "Press Enter to go back")
        {
            Console.Clear();    
        }
    }
}