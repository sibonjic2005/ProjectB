using System.Net.Quic;

static class StartingMenu
{
    static public void Menu()
    {
        Console.WriteLine("Welcome to the secure login system!");

        Console.WriteLine("1. Login");
        Console.WriteLine("2. Create an account");
        Console.WriteLine("3. Enter as Guest");
        Console.WriteLine("4. Exit");
        Console.WriteLine("5. User menu (for testing)"); // for testing ONLY
        Console.WriteLine("6. Admin menu (for testing)"); // for testing ONLY

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else if (input == "3")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else if (input == "4")
        { 
            Console.WriteLine("Thank for visiting our website!");
            return;
        }
        else if (input == "5")
        {
            UserMenu.UserMenuStart();
        }
        else if (input == "6")
        {
            AdminMenu.AdminMenuStart();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Menu();
        }
    }
}