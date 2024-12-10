using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class SignUpChecker
{
    public string PasswordRules(Func<string> getPassword)
    {
        string password;
        while (true)
        {
            password = getPassword();

            if (password.Length < 8)
            {
                Console.WriteLine("Password must be at least 8 characters long.");
                continue;
            }
            else if (!Regex.IsMatch(password, "[A-Z]"))
            {
                Console.WriteLine("Password must contain at least one uppercase letter.");
                continue;
            }
            else if (!Regex.IsMatch(password, "[0-9]"))
            {
                Console.WriteLine("Password must contain at least one digit.");
                continue;
            }
            else if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                Console.WriteLine("Password must contain at least one special character.");
                continue;
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
                Console.WriteLine("Email address is invalid.");
                continue;
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
                Console.WriteLine("Phone number is invalid. It must contain 10 to 15 digits.");
                continue;
            }
            else
            {
                return phoneNumber;
            }
        }
    }

    public string EditPhoneNumber(Func<string> getPhoneNumber)
    {
        string phoneNumber;
        string phonePattern = @"^\+?\d{10,15}$";

        while (true)
        {
            phoneNumber = getPhoneNumber();
            if (!Regex.IsMatch(phoneNumber, phonePattern) && phoneNumber != String.Empty)
            {
                Console.WriteLine("Phone number is invalid. It must contain 10 to 15 digits.");
                continue;
            }
            else
            {
                return phoneNumber;
            }
        }
    }

    public string ValidateDate(Func<string> getDate)
    {
        string date;
        string datePattern = @"^\d{2}-\d{2}-\d{4}$";

        while (true)
        {
            date = getDate();

            if (date == string.Empty)
            {
                return date;
            }

            if (!Regex.IsMatch(date, datePattern))
            {
                Console.WriteLine("Date is invalid. Please enter a valid date in the format DD-MM-YYYY.");
                continue;
            }

            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedDate))
            {
                Console.WriteLine("Date is invalid. Please enter a valid calendar date.");
                continue;
            }

            if (parsedDate.Year < 1900)
            {
                Console.WriteLine("Year must not be earlier than 1900.");
                continue;
            }

            return date;
        }
    }
    public string ValidateName(Func<string> getName)
    {
        string name;
        string namePattern = @"^[A-Za-z\s]{2,}$";

        while (true)
        {
            name = getName();

            if (!Regex.IsMatch(name, namePattern))
            {
                Console.WriteLine("Name is invalid. It must contain only letters and be at least 2 characters long.");
                continue;
            }

            return name;
        }
    }

    public string ValidateAddress(Func<string> getAddress)
    {
        string address;
        string addressPattern = @"^(?=.*[A-Za-z])(?=.*\d).{5,}$";

        while (true)
        {
            address = getAddress();

            if (address == string.Empty) {
                return address;
            }

            if (!Regex.IsMatch(address, addressPattern))
            {
                Console.WriteLine("Address is invalid.");
                continue;
            }

            return address;
        }
    }
}