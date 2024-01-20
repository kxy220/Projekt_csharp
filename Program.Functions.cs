using FlightSystem;


partial class Program
{
      public static List<Flight> SearchFlights(string city, DateTime? date, List<Flight> flights)
        {
            return flights.Where(flight =>
                (flight.DepartureAirport.City.Equals(city, StringComparison.OrdinalIgnoreCase) ||
                 flight.ArrivalAirport.City.Equals(city, StringComparison.OrdinalIgnoreCase)) &&
                (!date.HasValue || flight.DepartureTime.Date == date.Value.Date)
            ).ToList();
        }

        public static void PrintPassengerList(int flightId, List<Flight> flights)
        {
            var flight = flights.FirstOrDefault(f => f.Id == flightId);
            if (flight != null)
            {
                foreach (var reservation in flight.Reservations)
                {
                    Console.WriteLine($"Pasażer: {reservation.Ticket.Passenger.FirstName} {reservation.Ticket.Passenger.LastName}");
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono danego lotu.");
            }
        }



        public static void MakeReservation(Flight? flight, List<Passenger> passengers, List<Reservation> reservations)
         {
             
             if (flight != null)
             {
                 Console.WriteLine("Wprowadź imię pasażera:");
                 string firstName = Console.ReadLine();
                 Console.WriteLine("Wprowadź nazwisko pasażera:");
                 string lastName = Console.ReadLine();
                 Console.WriteLine("Wybierz klasę: 1 - Ekonomiczna, 2 - Biznesowa, 3 - Pierwsza Klasa");
                 int classOption = Convert.ToInt32(Console.ReadLine());
                 TicketClass selectedClass = (TicketClass)(classOption - 1); 


                 var passenger = passengers.FirstOrDefault(p => p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
                 if (passenger == null)
                 {
                     passenger = new Passenger { FirstName = firstName, LastName = lastName };
                     passengers.Add(passenger); 
                 }

                 var ticket = new Ticket
                 {
                     Flight = flight,
                     Passenger = passenger,
                     Price = flight.BasePrice,
                     Class = selectedClass
                 };
                 
                 ticket.adjustPrice(); // dostosowanie ceny w zaleznosci od klasy
                 
                 var reservation = new Reservation
                 {
                     Ticket = ticket,
                     ReservationCode = Guid.NewGuid().ToString(), // Generowanie unikalnego kodu rezerwacji
                 };

                 if (selectedClass == TicketClass.Economy)
                 {
                     if (flight.isEconomySeat())
                     {
                         reservations.Add(reservation); 
                         flight.Reservations.Add(reservation); 
                     }
                     else
                     {
                         Console.WriteLine("Brak miejsc w klasie ekonomicznej.");

                     }
                 }
                 else if (selectedClass == TicketClass.Business)
                 {
                     if (flight.isBussinessSeat())
                     {
                         reservations.Add(reservation); 
                         flight.Reservations.Add(reservation); 
                     }
                     else
                     {
                         Console.WriteLine("Brak miejsc w klasie biznesowej.");
                     }
                 }
                 else if (selectedClass == TicketClass.FirstClass)
                 {
                     if (flight.isFirstClassSeat())
                     {
                         reservations.Add(reservation);
                         flight.Reservations.Add(reservation); 
                     }
                     else
                     {
                         Console.WriteLine("Brak miejsc w pierwszej klasie.");
                     }
                 }

                 Console.WriteLine($"Rezerwacja złożona z kodem: {reservation.ReservationCode}");
                 Console.WriteLine($"Cena biletu: {reservation.Ticket.Price}");
             }
             else
             {
                 Console.WriteLine("Nie odnaleziono lotu.");
             }
         }

            static void CancelReservation(string? reservationCode, List<Flight> flights, List<Reservation> reservations)
          {

              var reservation = reservations.FirstOrDefault(r => r.ReservationCode.Equals(reservationCode, StringComparison.OrdinalIgnoreCase));

              if (reservation != null)
              {
                  reservations.Remove(reservation); // Usunięcie rezerwacji z listy

                  var flight = flights.FirstOrDefault(f => f.Id == reservation.Ticket.Flight.Id);
                  if (flight != null)
                  {
                      flight.Reservations.Remove(reservation); 
                  }

                  Console.WriteLine("Rezerwacja anulowana.");
              }
              else
              {
                  Console.WriteLine("Nie znaleziono rezerwacji.");
              }
          }

}