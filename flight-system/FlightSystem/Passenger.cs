namespace FlightSystem;

public class Passenger: BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Ticket> Tickets { get; set; }
}