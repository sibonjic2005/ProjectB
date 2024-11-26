using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Spectre.Console;

class UserModel : GuestModel
{
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("dateOfBirth")]
    public string DateOfBirth { get; set; } = "";
    
    [JsonPropertyName("address")]
    public string Address { get; set; } = "";
    
    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("isGuest")]
    public bool IsGuest { get; set;} = false;

    [JsonPropertyName("isEmployee")]
    public bool IsEmployee { get; set; }

    [JsonPropertyName("reservations")]
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    public UserModel(string name, string emailAddress, string phoneNumber, string password = "", string dateOfBirth = "", string address = "", List<string> preferences = null)
        : base(name, emailAddress, phoneNumber, preferences)
    {
        Password = password;
        DateOfBirth = dateOfBirth;
        Address = address;
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