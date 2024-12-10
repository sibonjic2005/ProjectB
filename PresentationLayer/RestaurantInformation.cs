using Spectre.Console;
using System.Text.Json;
using System.IO;

public static class RestaurantInformation
{
    private static string dataFilePath = "restaurant_info.json";

    private static string restaurantName;
    private static string address;
    private static string phoneNumber;
    private static string cuisineType;
    private static string cancellationPolicy;
    private static string paymentMethods;
    private static List<string> houseRules;
    private static string openingHours;

    static RestaurantInformation()
    {
        LoadRestaurantInformation();
    }

    // public static void PrintRestaurantInformationAdmin()
    // {
    //     AnsiConsole.MarkupLine($"[maroon]Restaurant Name:[/] {restaurantName}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Address:[/] {address}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Phone Number:[/] {phoneNumber}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Cuisine Type:[/] {cuisineType}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Cancellation Policy:[/] {cancellationPolicy}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Payment:[/] {paymentMethods}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine("[maroon]House Rules:[/]");

    //     for (int i = 0; i < houseRules.Count; i++)
    //     {
    //         AnsiConsole.MarkupLine($"[blue3]House Rule {i + 1}:[/] {houseRules[i]}");
    //     }
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Opening time Mon-Sun:[/] {openingHours}");

    //     GoBack.GoBackRestaurantInfo();
    // }
    // public static void PrintRestaurantInformationGuest()
    // {
    //     AnsiConsole.MarkupLine($"[maroon]Restaurant Name:[/] {restaurantName}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Address:[/] {address}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Phone Number:[/] {phoneNumber}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Cuisine Type:[/] {cuisineType}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Cancellation Policy:[/] {cancellationPolicy}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Payment:[/] {paymentMethods}");
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine("[maroon]House Rules:[/]");

    //     for (int i = 0; i < houseRules.Count; i++)
    //     {
    //         AnsiConsole.MarkupLine($"[blue3]House Rule {i + 1}:[/] {houseRules[i]}");
    //     }
    //     AnsiConsole.WriteLine("");
    //     AnsiConsole.MarkupLine($"[maroon]Opening time Mon-Sun:[/] {openingHours}");

    //     GoBack.GoBackStartingMenu();
    // }
    public static void PrintRestaurantInformation()
    {
        AnsiConsole.MarkupLine($"[maroon]Restaurant Name:[/] {restaurantName}");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"[maroon]Address:[/] {address}");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"[maroon]Phone Number:[/] {phoneNumber}");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"[maroon]Cuisine Type:[/] {cuisineType}");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"[maroon]Cancellation Policy:[/] {cancellationPolicy}");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"[maroon]Payment:[/] {paymentMethods}");
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine("[maroon]House Rules:[/]");

        for (int i = 0; i < houseRules.Count; i++)
        {
            AnsiConsole.MarkupLine($"[blue3]House Rule {i + 1}:[/] {houseRules[i]}");
        }
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"[maroon]Opening time Mon-Sun:[/] {openingHours}");

        // GoBack.GoBackUserMenu();
    }

    public static void EditRestaurantInformation()
    {
        restaurantName = AnsiConsole.Ask<string>("Enter the [green]restaurant name[/]:", restaurantName);
        address = AnsiConsole.Ask<string>("Enter the [green]address[/]:", address);
        phoneNumber = AnsiConsole.Ask<string>("Enter the [green]phone number[/]:", phoneNumber);
        cuisineType = AnsiConsole.Ask<string>("Enter the [green]cuisine type[/]:", cuisineType);
        cancellationPolicy = AnsiConsole.Ask<string>("Enter the [green]cancellation policy[/]:", cancellationPolicy);
        paymentMethods = AnsiConsole.Ask<string>("Enter the [green]payment methods[/]:", paymentMethods);

        AnsiConsole.MarkupLine("[yellow]Edit House Rules:[/]");
        for (int i = 0; i < houseRules.Count; i++)
        {
            houseRules[i] = AnsiConsole.Ask<string>($"House Rule {i + 1}:", houseRules[i]);
        }

        if (AnsiConsole.Confirm("Do you want to add a new house rule?"))
        {
            string newRule;
            do
            {
                newRule = AnsiConsole.Prompt(new TextPrompt<string>("Enter a new house rule (or leave empty to stop):").AllowEmpty());
                if (!string.IsNullOrWhiteSpace(newRule))
                {
                    houseRules.Add(newRule);
                }
            } while (!string.IsNullOrWhiteSpace(newRule));
        }

        openingHours = AnsiConsole.Ask<string>("Enter the [green]opening hours[/]:", openingHours);

        SaveRestaurantInformation();

        AnsiConsole.MarkupLine("[green]Restaurant information updated successfully![/]");

        GoBack.GoBackRestaurantInfo();
    }

    private static void LoadRestaurantInformation()
    {
        if (File.Exists(dataFilePath))
        {
            var jsonData = File.ReadAllText(dataFilePath);
            var data = JsonSerializer.Deserialize<RestaurantData>(jsonData);

            restaurantName = data.RestaurantName;
            address = data.Address;
            phoneNumber = data.PhoneNumber;
            cuisineType = data.CuisineType;
            cancellationPolicy = data.CancellationPolicy;
            paymentMethods = data.PaymentMethods;
            houseRules = data.HouseRules;
            openingHours = data.OpeningHours;
        }
        else
        {
            // Default values
            restaurantName = "Blind Ate";
            address = "Wijnhaven 61 3011WJ Rotterdam";
            phoneNumber = "+31 10 223 4598";
            cuisineType = "Blind experience or A la Carte";
            cancellationPolicy = "You can always cancel your reservation in the application or cancel by calling.";
            paymentMethods = "IDeal or Credit Card";
            houseRules = new List<string>
            {
                "No takeaway.",
                "Pets are allowed, but need to behave.",
                "We do not serve alcohol under the age of 18.",
                "No smoking in the restaurant.",
                "Kindly keep your personal belongings with you; we are not responsible for lost items."
            };
            openingHours = "10:00 - 00:00";

            SaveRestaurantInformation();
        }
    }

    private static void SaveRestaurantInformation()
    {
        var data = new RestaurantData
        {
            RestaurantName = restaurantName,
            Address = address,
            PhoneNumber = phoneNumber,
            CuisineType = cuisineType,
            CancellationPolicy = cancellationPolicy,
            PaymentMethods = paymentMethods,
            HouseRules = houseRules,
            OpeningHours = openingHours
        };

        var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataFilePath, jsonData);
    }

    private class RestaurantData
    {
        public string RestaurantName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CuisineType { get; set; }
        public string CancellationPolicy { get; set; }
        public string PaymentMethods { get; set; }
        public List<string> HouseRules { get; set; }
        public string OpeningHours { get; set; }
    }
}
