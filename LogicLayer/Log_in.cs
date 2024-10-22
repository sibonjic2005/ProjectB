// using Newtonsoft.Json;
// using System.Reflection.Metadata;
// using System.Security.Cryptography;
// using System.Text;

// class Log_in
// {
//     static string filePath = "credentials.json";
//     static List<User> users = new List<User>();
//     static Dictionary<string, string> credentials = new Dictionary<string, string>();
//     static bool changesMade = false;
//     static bool isLoggedIn = false;
//     Restaurant restaurant = new Restaurant();
//     public void Menupage()
//     {
//         LoadUsersFromFile();

//         while (!isLoggedIn)
//         {
//             restaurant.Menu();
//             Console.Write("Choose an option: ");
//             string choice = Console.ReadLine();

//             switch (choice)
//             {
//                 case "1":
//                     Login();
//                     break;
//                 case "2":
//                     Register();
//                     break;
//                 case "3":
//                     if (changesMade)
//                     {
//                         SaveUsersToFile();
//                     }
//                     return;
//                 default:
//                     Console.WriteLine("Invalid choice, please try again.");
//                     break;
//             }
//         }
//     }

//     public void Login()
//     {
//         Console.Write("Enter your email: ");
//         string email = Console.ReadLine();

//         Console.Write("Enter your password: ");
//         string password = ReadPassword();

//         if (ValidateLogin(email, password))
//         {
//             isLoggedIn = true;
//             Console.WriteLine("Login successful! Welcome, " + email + ".");
//             restaurant.MenuLogin();
//         }
//         else
//         {
//             Console.WriteLine("Invalid username or password. Please try again.");
//         }
//     }

//     static void Register()
//     {
//         Console.Write("Enter your name: ");
//         string name = Console.ReadLine();

//         Console.Write("Enter your email: ");
//         string email = Console.ReadLine();

//         if (credentials.ContainsKey(email))
//         {
//             Console.WriteLine("Email already taken. Please choose a different email.");
//             return;
//         }

//         Console.Write("Enter your phone number: ");
//         string phoneNumber = Console.ReadLine();

//         Console.Write("Enter a password: ");
//         string password = ReadPassword();
//         string hashedPassword = HashPassword(password);

//         credentials[email] = hashedPassword;
//         changesMade = true;

//         Console.Write("Enter your date of birth (DD-MM-YYYY) (optional): ");
//         string dateOfBirth = Console.ReadLine();

//         Console.Write("Enter your address (optional): ");
//         string address = Console.ReadLine();

//         Console.Write("Enter your food preferences (e.g., diet, allergies, halal): ");
//         string preferences = Console.ReadLine();

//         User user = new User(name, email, phoneNumber, hashedPassword, dateOfBirth, address, preferences);

//         users.Add(user);

//         Console.WriteLine("User registered successfully!");
//         SaveUsersToFile();
//     }

//     static bool ValidateLogin(string email, string password)
//     {
//         if (credentials.ContainsKey(email) && credentials[email] == HashPassword(password))
//         {
//             return true;
//         }
//         return false;
//     }

//     static string HashPassword(string password)
//     {
//         using (SHA256 sha256 = SHA256.Create())
//         {
//             byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//             StringBuilder builder = new StringBuilder();
//             for (int i = 0; i < bytes.Length; i++)
//             {
//                 builder.Append(bytes[i].ToString("x2"));
//             }
//             return builder.ToString();
//         }
//     }

//     static string ReadPassword()
//     {
//         string password = "";
//         ConsoleKeyInfo key;

//         do
//         {
//             key = Console.ReadKey(true);
//             if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
//             {
//                 password += key.KeyChar;
//                 Console.Write("*");
//             }
//             else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
//             {
//                 password = password.Substring(0, password.Length - 1);
//                 Console.Write("\b \b");
//             }
//         }
//         while (key.Key != ConsoleKey.Enter);

//         Console.WriteLine();
//         return password;
//     }

//     static void LoadUsersFromFile()
//     {
//         if (File.Exists(filePath))
//         {
//             try
//             {
//                 string json = File.ReadAllText(filePath);
//                 credentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Error loading users: " + ex.Message);
//                 credentials = new Dictionary<string, string>();
//             }
//         }
//         else
//         {
//             changesMade = true;
//         }
//     }

//     static void SaveUsersToFile()
//     {
//         try
//         {
//             string json = JsonConvert.SerializeObject(credentials, Formatting.Indented);
//             File.WriteAllText(filePath, json);
//             changesMade = false;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Error saving users: " + ex.Message);
//         }
//     }
// }
