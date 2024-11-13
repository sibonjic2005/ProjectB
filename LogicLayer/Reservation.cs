public class Reservation
{
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string PersonCount { get; set; }
    public int TableNumber { get; set; }

    public Reservation(DateTime date, string time, string personCount, int tableNumber)
    {
        Date = date;
        Time = time;
        PersonCount = personCount;
        TableNumber = tableNumber;
    }
    
}