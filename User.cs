class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string DateOfBirth;
    public string Address;
    public string Preferences;

    public User(string name, string email, string phoneNumber, string password, string dateOfBirth = "", string address = "", string preferences = "")
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        DateOfBirth = dateOfBirth;
        Address = address;
        Preferences = preferences;
    }

}