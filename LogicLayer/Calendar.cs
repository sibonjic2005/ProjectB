using Spectre.Console;
static class Calendar
{
     public static DateTime CalendarDate()
    {
        DateTime currentTime = DateTime.Now;
        DateTime selectedDate = currentTime.Hour >= 21 ? DateTime.Today.AddDays(1) : DateTime.Today;

        while (true)
        {
            Console.Clear();
            DisplayCalendar(selectedDate);

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (selectedDate > DateTime.Today.AddDays(currentTime.Hour >= 21 ? 1 : 0))
                    {
                        selectedDate = selectedDate.AddDays(-1);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    selectedDate = selectedDate.AddDays(1);
                    break;
                case ConsoleKey.UpArrow:
                    if (selectedDate > DateTime.Today.AddDays(6))
                    {
                        selectedDate = selectedDate.AddDays(-7);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    selectedDate = selectedDate.AddDays(7);
                    break;
                case ConsoleKey.Enter:
                    string formattedDate = selectedDate.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"));
                    AnsiConsole.MarkupLine($"You selected: [bold yellow]{formattedDate}[/]");
                    return selectedDate;
            }
        }
    }

    public static List<string> GetTimeOptions(DateTime date)
    {
        int startHour = 10;
        if (date.Date == DateTime.Today)
        {
            startHour = Math.Max(10, DateTime.Now.Hour + 1);
        }

        var timeOptions = new List<string> ();
        for (int hour = startHour; hour <= 21; hour++)
        {
            timeOptions.Add($"{hour:00}:00");
        }

        return timeOptions;
    }

    static void DisplayCalendar(DateTime selectedDate)
    {
        DateTime firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);

        string formattedMonth = selectedDate.ToString("MMMM yyyy", new System.Globalization.CultureInfo("en-US"));

        AnsiConsole.MarkupLine($"[underline bold]Select a Date (Use Arrows)[/]\n");
        AnsiConsole.MarkupLine($"{formattedMonth:MMMM yyyy}");
        AnsiConsole.MarkupLine("Su Mo Tu We Th Fr Sa");

        int leadingSpaces = (int)firstDayOfMonth.DayOfWeek;
        for (int i = 0; i < leadingSpaces; i++)
        {
            Console.Write("   ");
        }

        for (int day = 1; day <= daysInMonth; day++)
        {
            var currentDate = new DateTime(selectedDate.Year, selectedDate.Month, day);

            if (currentDate == selectedDate)
            {
                AnsiConsole.Markup($"[bold yellow]{day,2}[/] ");
            }
            else
            {
                AnsiConsole.Markup($"{day,2} ");
            }

            if ((day + leadingSpaces) % 7 == 0)
            {
                Console.WriteLine();
            }            
        }

        Console.WriteLine("\n\n[Press Enter to confirm selection]");
    }

    public static string FormatDate(DateTime date)
    {
        return date.ToString("dddd, MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US"));
    }
}