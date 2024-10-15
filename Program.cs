using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

class Program
{
    // Define the path to the JSON file
    static string filePath = "users.json";

    // Dictionary to store username and hashed passwords
    static Dictionary<string, string> users = new Dictionary<string, string>();

    static void Main()
    {
        // Load existing users from the JSON file if it exists
        LoadUsersFromFile();

        Console.WriteLine("Welcome to the secure login system!");

        while (true)
        {
            Console.WriteLine("\n1. Login\n2. Register\n3. Exit");
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
                    SaveUsersToFile();
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    // Method to login
    static void Login()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();

        Console.Write("Enter your password: ");
        string password = ReadPassword();

        if (ValidateLogin(username, password))
        {
            Console.WriteLine("Login successful! Welcome, " + username + ".");
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
        }
    }

    // Method to register a new user
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

        // Hash the password and store the user
        users[username] = HashPassword(password);

        Console.WriteLine("User registered successfully!");

        // Save the new user to the JSON file
        SaveUsersToFile();
    }

    // Method to validate username and password
    static bool ValidateLogin(string username, string password)
    {
        if (users.ContainsKey(username) && users[username] == HashPassword(password))
        {
            return true;
        }
        return false;
    }

    // Method to hash the password using SHA-256
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

    // Method to hide password input (optional)
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

    // Method to load users from a JSON file
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
                users = new Dictionary<string, string>(); // Start fresh if there's an error
            }
        }
    }

    // Method to save users to a JSON file
    static void SaveUsersToFile()
    {
        try
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Users saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving users: " + ex.Message);
        }
    }
}
