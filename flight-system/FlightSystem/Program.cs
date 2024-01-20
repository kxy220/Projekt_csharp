// See https://aka.ms/new-console-template for more information


using FlightSystem;

int flightId;


// var airports = new List<Airport>
// {
//     new Airport { Id = 1, Name = "John F. Kennedy International Airport", City = "New York", Country = "USA" },
//     new Airport { Id = 2, Name = "Los Angeles International Airport", City = "Los Angeles", Country = "USA" },
// };

// var aircraft = new List<PassengerAircraft>
// {
//     new PassengerAircraft { Id = 1, Model = "Boeing 747", TotalSeats = 366, EconomySeats = 280, BusinessSeats = 70, FirstClassSeats = 16 },
//     new PassengerAircraft { Id = 2, Model = "Airbus A320", TotalSeats = 150, EconomySeats = 120, BusinessSeats = 20, FirstClassSeats = 10 },
// };

// var flights = new List<Flight>
// {
//     new Flight
//     {
//         Id = 1,
//         DepartureAirport = airports[0],
//         ArrivalAirport = airports[1],
//         DepartureTime = new DateTime(2024, 1, 20, 8, 0, 0),
//         ArrivalTime = new DateTime(2024, 1, 20, 11, 30, 0),
//         Aircraft = aircraft[0],
//         Reservations = new List<Reservation>()
//     },
//     // Add more flights as needed
// };

// var passengers = new List<Passenger>
// {
//     new Passenger { Id = 1, FirstName = "John", LastName = "Doe", Tickets = new List<Ticket>() },
//     new Passenger { Id = 2, FirstName = "Jane", LastName = "Smith", Tickets = new List<Ticket>() },
//     // Add more passengers as needed
// };


List<Flight> flights = FileDatabase.LoadFlights();
List<Reservation> reservations = FileDatabase.LoadReservations();
List<Passenger> passengers = FileDatabase.LoadPassengers();



        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("2. Dokonaj rezerwacji");            
            Console.WriteLine("1. Szukaj lotu");
            Console.WriteLine("3. Anuluj rezerwację");
            Console.WriteLine("4. Wypisz listę pasażerów danego lotu");
            Console.WriteLine("5. Wyjdź");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Podaj miasto odlotu lub przylotu:");
                    string city = Console.ReadLine();
                    Console.WriteLine("Wprowadź datę (rrrr-mm-dd) lub zostaw puste pole:");
                    string dateString = Console.ReadLine();
                    DateTime? date = string.IsNullOrEmpty(dateString) ? (DateTime?)null : DateTime.Parse(dateString);
                    var matchingFlights = SearchFlights(city, date, flights);
                    foreach (var flight in matchingFlights)
                    {
                        Console.WriteLine($"Loty z {flight.DepartureAirport.City} do {flight.ArrivalAirport.City} dnia {flight.DepartureTime}");
                    }
                    break;
                case 2:
                    Console.WriteLine("Podaj ID lotu w celu rezerwacji:");
                    flightId = Convert.ToInt32(Console.ReadLine());
                    var searchFlight = flights.FirstOrDefault(f => f.Id == flightId);
                    MakeReservation(searchFlight, passengers, reservations);
                    break;
                case 3:
                    Console.WriteLine("Wprowadź kod rezerwacji, którą chcesz anulować:");
                    string reservationCode = Console.ReadLine();
                    CancelReservation(reservationCode, flights, reservations);
                    break;
                case 4:
                    Console.WriteLine("Wprowadź ID lotu:");
                    flightId = Convert.ToInt32(Console.ReadLine());
                    PrintPassengerList(flightId, flights);
                    break;
                case 5:
                    exit = true;
                    FileDatabase.SaveReservations(reservations);
                    FileDatabase.SaveFlights(flights);
                    FileDatabase.SavePassengers(passengers);
                    break;
                default:
                    Console.WriteLine("Niepoprawna opcja. Spróbuj ponownie.");
                    break;
            }
        }

      

