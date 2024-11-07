using System.Text.Json.Serialization;


class UserModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("dateOfBirth")]
    public string DateOfBirth { get; set; } = "";
    
    [JsonPropertyName("address")]
    public string Address { get; set; } = "";
    
    [JsonPropertyName("preferences")]
    public List<string> Preferences { get; set; } = new List<string>();

    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("isGuest")]
    public bool IsGuest { get; set;} = false;

    [JsonPropertyName("isEmployee")]
    public bool IsEmployee { get; set; }

    [JsonPropertyName("reservations")]
    public List<Dictionary<string, string>> Reservations { get; set; } = new List<Dictionary<string, string>>();

    public UserModel(string name, string emailAddress, string phoneNumber, string password = "", string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
        DateOfBirth = dateOfBirth;
        Address = address;
        Preferences = preferences ?? new List<string>();
        Reservations = new List<Dictionary<string, string>>();
        IsAdmin = false;
        IsEmployee = false;
    }

    public static UserModel CreateAdmin(string name, string emailAddress, string phoneNumber, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var admin = new UserModel(name, emailAddress, phoneNumber, password, dateOfBirth, address, preferences)
        {
            IsAdmin = true
        };
        return admin;
    }

    public static UserModel CreateEmployee(string name, string emailAddress, string phoneNumber, string password, string dateOfBirth = "", string address = "", List<string> preferences = null)
    {
        var employee = new UserModel(name, emailAddress, phoneNumber, password, dateOfBirth, address, preferences)
        {
            IsEmployee = true
        };
        return employee;
    }
}