using System.Text.Json;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataSources/accounts.json"));
    // static string path1 = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataSources/guests.json"));

    public static List<UserModel> LoadAll()
    {
        try
        {
            if (!File.Exists(path))
            {
                return new List<UserModel>();
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading accounts: {ex.Message}");
            return new List<UserModel>();
        }
    }

    // public static List<GuestModel> LoadAllGuests()
    // {
    //     try
    //     {
    //         if (!File.Exists(path1))
    //         {
    //             return new List<GuestModel>();
    //         }

    //         string json = File.ReadAllText(path1);
    //         return JsonSerializer.Deserialize<List<GuestModel>>(json) ?? new List<GuestModel>();
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Error loading accounts: {ex.Message}");
    //         return new List<GuestModel>();
    //     }
    // }

    // public static void WriteAllGuests(List<GuestModel> guests)
    // {
    //     try
    //     {
    //         var options = new JsonSerializerOptions { WriteIndented = true };
    //         string json = JsonSerializer.Serialize(guests, options);

    //         string directory = Path.GetDirectoryName(path1);
    //         if (!Directory.Exists(directory))
    //         {
    //             Directory.CreateDirectory(directory);
    //         }

    //         File.WriteAllText(path1, json);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Error writing accounts: {ex.Message}");
    //     }
    // }

    public static void WriteAll(List<UserModel> accounts)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(accounts, options);

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing accounts: {ex.Message}");
        }
    }   
}