using System.Text.Json.Serialization;


class GuestModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("preferences")]
    public List<string> Preferences { get; set; } = new List<string>();

    [JsonPropertyName("reservations")]
    public List<Dictionary<string, string>> Reservations { get; set; } = new List<Dictionary<string, string>>();

    public GuestModel(string name, string emailAddress, string phoneNumber, List<string> preferences = null)
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Preferences = preferences ?? new List<string>();
        Reservations = new List<Dictionary<string, string>>();
    }
}