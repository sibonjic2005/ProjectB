using System;
using System.Text.RegularExpressions;

public class Log_in_Checker
{
    public string PasswordRules(Func<string> getPassword)
    {
        string password;
        while (true)
        {
            password = getPassword();

            if (password.Length < 8)
            {
                return "Password must be at least 8 characters long.";
            }
            else if (!Regex.IsMatch(password, "[A-Z]"))
            {
                return "Password must contain at least one uppercase letter.";
            }
            else if (!Regex.IsMatch(password, "[0-9]"))
            {
                return "Password must contain at least one digit.";
            }
            else if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                return "Password must contain at least one special character.";
            }
            else
            {
                return password;
            }

        }
    }

    public string ValidateEmail(Func<string> getEmail)
    {
        string email;
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        while (true)
        {
            email = getEmail();

            if (!Regex.IsMatch(email, emailPattern))
            {
                return "Email address is invalid.";
            }
            else
            {
                return email;
            }
        }
    }

    public string PhoneNumberRules(Func<string> getPhoneNumber)
    {
        string phoneNumber;
        string phonePattern = @"^\+?\d{10,15}$";

        while (true)
        {
            phoneNumber = getPhoneNumber();

            if (!Regex.IsMatch(phoneNumber, phonePattern))
            {
                return "Phone number is invalid. It must contain 10 to 15 digits.";
            }
            else
            {
                return phoneNumber;
            }
        }
    }
}
