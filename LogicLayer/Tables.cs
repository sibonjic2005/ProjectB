public class Tables
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Tables (int tableNumber, int capacity)
    {
        TableNumber = tableNumber;
        Capacity = capacity;
    }
}
