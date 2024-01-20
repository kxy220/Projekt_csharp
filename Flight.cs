namespace FlightSystem;

public class Flight: BaseEntity
{
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public PassengerAircraft Aircraft { get; set; }
    public List<Reservation> Reservations { get; set; }
    public int BasePrice { get; set; }

    public bool isBussinessSeat()
    {
        int businessReservationsCount = Reservations.Count(r => r.Ticket.Class == TicketClass.Business);

        return businessReservationsCount < Aircraft.BusinessSeats;
    }
    
    public bool isEconomySeat()
    {
        int economyReservationsCount = Reservations.Count(r => r.Ticket.Class == TicketClass.Economy);

        return economyReservationsCount < Aircraft.BusinessSeats;    }
    
    public bool isFirstClassSeat()
    {
        int firstClassReservationsCount = Reservations.Count(r => r.Ticket.Class == TicketClass.FirstClass);
        
        return firstClassReservationsCount < Aircraft.BusinessSeats;    }
}