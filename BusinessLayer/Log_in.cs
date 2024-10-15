using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

class Log_in
{
    static string filePath = "users.json";
    static Dictionary<string, string> users = new Dictionary<string, string>();
    static bool changesMade = false;
    Restaurant restaurant = new Restaurant();
    bool isLoggedIn = false;

    public void Menupage()
    {
        LoadUsersFromFile();

        while (!isLoggedIn)
        {
            restaurant.Menu();
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                case "3":
                    if (changesMade)
                    {
                        SaveUsersToFile();
                    }
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    public void Login()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();

        Console.Write("Enter your password: ");
        string password = ReadPassword();

        if (ValidateLogin(username, password))
        {
            isLoggedIn = true;
            Console.WriteLine("Login successful! Welcome, " + username + ".");
            restaurant.MenuLogin();
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
        }
    }

    static void Register()
    {
        Console.Write("Enter a new username: ");
        string username = Console.ReadLine();

        if (users.ContainsKey(username))
        {
            Console.WriteLine("Username already exists. Please choose a different username.");
            return;
        }

        Console.Write("Enter a password: ");
        string password = ReadPassword();

        users[username] = HashPassword(password);
        changesMade = true;

        Console.WriteLine("User registered successfully!");
        SaveUsersToFile();
    }

    static bool ValidateLogin(string username, string password)
    {
        if (users.ContainsKey(username) && users[username] == HashPassword(password))
        {
            return true;
        }
        return false;
    }

    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }

    static void LoadUsersFromFile()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                users = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading users: " + ex.Message);
                users = new Dictionary<string, string>();
            }
        }
        else
        {
            changesMade = true;
        }
    }

    static void SaveUsersToFile()
    {
        try
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, json);
            changesMade = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving users: " + ex.Message);
        }
    }
}
