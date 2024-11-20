using System;

class RestaurantSeatingPlan
{
    static void Main()
    {
        string[] divider = new string[] { "+", "-", "+", "-" }; // To create borders

        // Print top border
        Console.WriteLine($"{divider[0]}-----------------------------------------------------------------{divider[2]}");
        Console.WriteLine("|                      Restaurant Seating Plan                    |");
        Console.WriteLine($"{divider[0]}-----------------------------------------------------------------{divider[2]}");

        // Print the layout rows
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
        

        // Print bottom border
        Console.WriteLine($"{divider[0]}-----------------------------------------------------------------{divider[2]}");
    }
}
