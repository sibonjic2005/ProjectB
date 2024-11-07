using System.Text.Json;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataSources/accounts.json"));

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