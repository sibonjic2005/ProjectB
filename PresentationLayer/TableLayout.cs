public static class TableLayout
{
    public static void SeatingPlan()
    {
        string[] divider = new string[] { "+", "-", "+", "-" }; // To create borders

        
        Console.WriteLine($"{divider[0]}-----------------------------------------------------------------{divider[2]}");
        Console.WriteLine("|                      Restaurant Seating Plan                    |");
        Console.WriteLine($"{divider[0]}-----------------------------------------------------------------{divider[2]}");

        Console.WriteLine("|   ┌────┐   ┌────┐       ┌────┐   ┌────┐       ┌────┐   ┌────┐   |");
        Console.WriteLine("|   │ T1 │   │ T2 │       │ T3 │   │ T4 │       │ T5 │   │ T6 │   |");
        Console.WriteLine("|   └────┘   └────┘       └────┘   └────┘       └────┘   └────┘   |");
        Console.WriteLine("|    (2P)     (2P)         (2P)     (2P)         (2P)     (2P)    |");
        Console.WriteLine("|                                                                 |");
        Console.WriteLine("|   ┌────┐   ┌────┐    ┌────┐         ┌────┐    ┌────┐   ┌────┐   |");
        Console.WriteLine("|   │ T7 │   │ T8 │    │ T9 │         │ T10│    │ T11│   │ T12│   |");
        Console.WriteLine("|   └────┘   └────┘    └────┘         └────┘    └────┘   └────┘   |");
        Console.WriteLine("|    (2P)     (2P)      (4P)          (4P)       (4P)     (4P)    |");
        Console.WriteLine("|                                                                 |");
        Console.WriteLine("|        ┌────┐        ┌────┐      ┌────┐        ┌────┐           |");
        Console.WriteLine("|        │ T13│        │ T14│      │ T15│        │ T16│           |");
        Console.WriteLine("|        └────┘        └────┘      └────┘        └────┘           |");
        Console.WriteLine("|         (6P)          (6P)        (6P)          (6P)            |");
        Console.WriteLine("|                                                                 |");
        Console.WriteLine("|   ┌────┐   ┌────┐   ┌────┐   ┌────┐   ┌────┐   ┌────┐   ┌────┐  |");
        Console.WriteLine("|   │ T17│   │ T18│   │ T19│   │ T20│   │ T21│   │ T22│   │ T23│  |");
        Console.WriteLine("|   └────┘   └────┘   └────┘   └────┘   └────┘   └────┘   └────┘  |");
        Console.WriteLine("|    (1P)     (1P)     (1P)     (1P)     (1P)     (1P)     (1P)   |");

        Console.WriteLine($"{divider[0]}-----------------------------------------------------------------{divider[2]}");
    }
}
