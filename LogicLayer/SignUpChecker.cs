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

    // Methode voor datumvalidatie met het formaat DD-MM-YYYY
    public string ValidateDate(Func<string> getDate)
    {
        string date;
        string datePattern = @"^\d{2}-\d{2}-\d{4}$"; // DD-MM-YYYY formaat

        while (true)
        {
            date = getDate();

            // Eerst controleren op het formaat
            if (!Regex.IsMatch(date, datePattern))
            {
                Console.WriteLine("Date is invalid. Please enter a valid date in the format DD-MM-YYYY.");
                continue;
            }

            // Vervolgens controleren of het een geldige datum is
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                Console.WriteLine("Date is invalid. Please enter a valid calendar date.");
                continue;
            }
            else
            {
                return date; // Geldige datum
            }
        }
    }
}