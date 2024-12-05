using System;
using Spectre.Console;

public class Payment
{
    public void StartPayment()
    {
        var paymentOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select your payment method:")
                .AddChoices("iDEAL", "Credit Card")
        );

        if (paymentOption == "iDEAL")
        {
            PayWithIdeal();
        }
        else if (paymentOption == "Credit Card")
        {
            PayWithCreditCard();
        }
    }

    private void PayWithIdeal()
    {
        var bankOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select your bank:")
                .AddChoices("ING", "Rabobank", "ABN AMRO", "SNS Bank", "Bunq")
        );

        Console.WriteLine($"You selected {bankOption} for iDEAL payment.");
        GenerateTikkieQR();
    }

    private void PayWithCreditCard()
    {
    AnsiConsole.MarkupLine("Enter your credit card details below [grey](enter a 16 digit number)[/]:");

    // Validate card number (16 digits)
    string cardNumber;
    while (true)
    {
        Console.Write("Card Number: ");
        cardNumber = Console.ReadLine();
        if (cardNumber.Length == 16 && long.TryParse(cardNumber, out _))
        {
            break;
        }
        Console.WriteLine("Invalid card number. It must be 16 digits long.");
    }

    // Validate expiration date (MM/YY format)
    string expirationDate;
    while (true)
    {
        Console.Write("Expiration Date (MM/YY): ");
        expirationDate = Console.ReadLine();
        if (DateTime.TryParseExact(expirationDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime date) && date > DateTime.Now)
        {
            break;
        }
        Console.WriteLine("Invalid expiration date. Please enter a valid future date in MM/YY format.");
    }

    // Validate CVV (3 digits)
    string cvv;
    while (true)
    {
        AnsiConsole.MarkupLine("CVV [grey](enter a 3 digit cvv numbers)[/]:");
        cvv = Console.ReadLine();
        if (cvv.Length == 3 && int.TryParse(cvv, out _))
        {
            break;
        }
        Console.WriteLine("Invalid CVV. It must be 3 digits long.");
    }

    Console.WriteLine("Processing credit card payment...");
    // Simulate a delay for processing
    System.Threading.Thread.Sleep(2000);
    Console.WriteLine("Payment successful! Thank you for your purchase.");
}

    private void GenerateTikkieQR()
    {
        // Simulating a QR code display for Tikkie
        Console.WriteLine("Generating Tikkie QR code...");
        System.Threading.Thread.Sleep(1000); // Simulate loading time
        AnsiConsole.WriteLine("");
        AnsiConsole.WriteLine("        ████████████                                       █████████████                                    ");
        AnsiConsole.WriteLine("  ██████            ████              ████              ████            ██████  ");
        AnsiConsole.WriteLine("  ████    ████████  ████    ██        ████████    ██    ████  ████████    ████  ");
        AnsiConsole.WriteLine("  ████  ██████████  ████          ██  ██████  ██████    ████  ██████████  ████  ");
        AnsiConsole.WriteLine("  ████              ████                          ████  ████              ████  ");
        AnsiConsole.WriteLine("    ████████████████████  ████  ████  ██          ████  ████████████████████    ");
        AnsiConsole.WriteLine("                              ████████                                          ");
        AnsiConsole.WriteLine("  ████████                            ████    ████        ██        ████  ████  ");
        AnsiConsole.WriteLine("    ██  ██████████    ████████    ████        ████    ██    ████████████████    ");
        AnsiConsole.WriteLine("          ██      ████    ████    ████████          ██            ██            ");
        AnsiConsole.WriteLine("            ██    ████████████████    ██████    ██    ██████      ██████  ████  ");
        AnsiConsole.WriteLine("    ██████████████    ████████        ████████████          ██  ████████████    ");
        AnsiConsole.WriteLine("            ████                ██████████  ██████████████    ██████            ");
        AnsiConsole.WriteLine("                          ████  ████    ██████    ████        ██████████  ████  ");
        AnsiConsole.WriteLine("    ████████████████████  ██████    ██            ████        ████      ████    ");
        AnsiConsole.WriteLine("  ████              ████        ██          ████  ████        ████      ██      ");
        AnsiConsole.WriteLine("  ████  ██████████  ████            ██  ████      ████          ██████████      ");
        AnsiConsole.WriteLine("  ████    ████████  ████    ██      ██████████      ████    ████        ████    ");
        AnsiConsole.WriteLine("  ██████            ████    ████████████          ██████    ██            ██       ");
        AnsiConsole.WriteLine("        █████████████                                         ████████████                                 ");
        AnsiConsole.WriteLine("");
        Console.WriteLine("Scan this QR Code with your Tikkie app to complete your payment.");
    }
}