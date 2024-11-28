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
        Console.WriteLine("Enter your credit card details below:");
        Console.Write("Card Number: ");
        string cardNumber = Console.ReadLine();
        Console.Write("Expiration Date (MM/YY): ");
        string expirationDate = Console.ReadLine();
        Console.Write("CVV: ");
        string cvv = Console.ReadLine();

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
        Console.WriteLine(@"
 _______________________
|                       |
|      QR CODE HERE     |
|                       |
|_______________________|

Scan this QR code with your Tikkie app to complete the payment.");
    }
}