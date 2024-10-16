static class UserMenu
{
    public static void UserMenuStart()
    {
        Console.WriteLine("1. Make a reservation");
        Console.WriteLine("2. Cancel a reservation");
        Console.WriteLine("3. View your reservations");
        Console.WriteLine("4. Log out");

        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("This feature is not yet implemented");
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
            Console.WriteLine("You succesfully logged out!");
            return;
        }
    }
}