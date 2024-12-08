using Newtonsoft.Json;


public class Reservation
{
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string EndTime { get; set; }
    public string PersonCount { get; set; }
    public int TableNumber { get; set; }
    public double TotalPrice => People.Sum(p => p.price);
    public bool isPaid { get; set; }
    public List<PersonReservation> People { get; set; }

    public Reservation(DateTime date, string time, string personCount, int tableNumber)
    {
        Date = date;
        Time = time;
        PersonCount = personCount;
        TableNumber = tableNumber;
        isPaid = false;
        int startHour = int.Parse(time.Split(':')[0]);
        int endHour = startHour == 20 ? startHour + 3 : startHour + 2;
        EndTime = $"{endHour:00}:00";
        People = new List<PersonReservation>();
        
    }

    public static bool ValidPersonCount(string PersonCount)
    {
        return !int.TryParse(PersonCount, out int personCount) || personCount <= 0;
    }
}

public class PersonReservation
{
    public string Name { get; set; }
    public bool BlindExperience { get; set; }
    public List<string> Food { get; set; }
    public List<string> Allergies { get; set; }

    public double price { get; set; }

    public PersonReservation(string name)
    {
        Name = name;
        BlindExperience = false;
        Food = new List<string>();
        price = 0.00;
        Allergies = new List<string>();
    }
}
