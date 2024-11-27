public class Reservation
{

    public DateTime Date { get; set; }
    public string Time { get; set; }
    // public string StartTime { get; set; }
    // public string EndTime { get; set; }
    public string PersonCount { get; set; }
    public int TableNumber { get; set; }
    public bool BlindExperience { get; set; }

    public Reservation(DateTime date, string time, string personCount, int tableNumber)
    {
        Date = date;
        Time = time;
        // StartTime = time;
        // EndTime = time;
        PersonCount = personCount;
        TableNumber = tableNumber;
        BlindExperience = false;
    }
}