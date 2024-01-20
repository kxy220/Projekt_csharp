namespace FlightSystem;

public class Reservation: BaseEntity
{
    public Ticket Ticket { get; set; }
    public string ReservationCode { get; set; }
    public bool IsConfirmed { get; set; }
}