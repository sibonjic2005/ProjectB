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
                        "Go back"            
                }));   
                
        if (choices == "Go back")
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
                        "Go back"            
                }));   
                
        if (choices == "Go back")
        {
            Console.Clear();
            StartingMenu.Menu();
        }
    }
}