namespace FlightSystem;

public class Ticket: BaseEntity
{
    public Flight Flight { get; set; }
    public Passenger Passenger { get; set; }
    public string SeatNumber { get; set; }
    public decimal Price { get; set; }
    public TicketClass Class { get; set; }

    public void adjustPrice()
    {
        if (Class == TicketClass.Business)
        {
            Price *= (decimal)1.4;
        }
        else if (Class == TicketClass.FirstClass)
        {
            Price *= 2;
        }
    }
}

public enum TicketClass
{
    Economy,
    Business,
    FirstClass
}