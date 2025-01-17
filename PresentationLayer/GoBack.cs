using Spectre.Console;
using System;

static class GoBack
{
    public static string Enter = "[yellow]Press Enter to go back[/]";
    public static void GoBackUserMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter           
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();
            UserMenu.UserMenuStart();
        }
    }
    public static void GoBackAdminMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();
            AdminMenu.AdminMenuStart();
        }
    }
    public static void GoBackReservationOption()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();  
            AdminMenu.ChooseReservationOption();
        }
    }

    public static void GoBackEmployeeMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();  
            EmployeeMenu.EmployeeMenuStart();
        }
    }

    public static void GoBackRestaurantInfo()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();  
            AdminMenu.ChooseRestaurantInfoOption();
        }
    }
    
    public static void GoBackManageMenu()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();  
            var foodMenuManager = new FoodMenuManager();
            foodMenuManager.ManageMenu();
        }
    }

    public static void GoBackClientOption()
    {
        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)")
                .AddChoices(new[] {
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();  
            AdminMenu.ChooseClientOption();
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
                        Enter            
                }));   
                
        if (choices == Enter)
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
                        Enter            
                }));   
                
        if (choices == Enter)
        {
            Console.Clear();    
        }
    }
}