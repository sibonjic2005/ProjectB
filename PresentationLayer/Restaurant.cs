using System;

class Restaurant
{
    public void Menu()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Sign Up");
        Console.WriteLine("3. Enter as Guest");
        Console.WriteLine("3. Admin Login");
        Console.WriteLine("5. Exit");
    }

    public void MenuLogin()
    {
        Console.WriteLine("1. Make a reservation");
        Console.WriteLine("2. Cancel a reservation");
        Console.WriteLine("3. View your reservations");
        Console.WriteLine("4. Log out");
    }

    public void MenuAdmin()
    {
        Console.WriteLine("1. Make a reservation");
        Console.WriteLine("2. Cancel a reservation");
        Console.WriteLine("3. View reservations");
        Console.WriteLine("4. Log out");
    }
}