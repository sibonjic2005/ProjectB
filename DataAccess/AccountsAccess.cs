using System.Text.Json;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));


    public static List<UserModel> LoadAll()
    {
        if (!File.Exists(path))
        {
            return new List<UserModel>();
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
    }


    public static void WriteAll(List<UserModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}