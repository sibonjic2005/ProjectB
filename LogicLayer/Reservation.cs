public class Reservation
{
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string EndTime { get; set; }
    public string PersonCount { get; set; }
    public int TableNumber { get; set; }
    public bool BlindExperience { get; set; }
    public List<string> Food { get; set; }

    public Reservation(DateTime date, string time, string personCount, int tableNumber)
    {
        Date = date;
        Time = time;
        PersonCount = personCount;
        TableNumber = tableNumber;
        int startHour = int.Parse(time.Split(':')[0]);
        int endHour = startHour == 20 ? startHour + 3 : startHour + 2;
        EndTime = $"{endHour:00}:00";
        BlindExperience = false;
        Food = new List<string>();
    }
}