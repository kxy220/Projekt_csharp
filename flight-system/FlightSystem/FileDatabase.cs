using Newtonsoft.Json;

namespace FlightSystem;

public static class FileDatabase
{
    private const string FlightsFile = "flights.json";
    private const string ReservationsFile = "reservations.json";
    private const string PassengersFile = "passengers.json";
    private static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };
    // Metoda do zapisu listy lotów do pliku
    public static void SaveFlights(List<Flight> flights)
    {
        string json = JsonConvert.SerializeObject(flights, Formatting.Indented, settings);
        File.WriteAllText(FlightsFile, json);
    }

    // Metoda do odczytu listy lotów z pliku
    public static List<Flight> LoadFlights()
    {
        if (!File.Exists(FlightsFile))
            return new List<Flight>();

        string json = File.ReadAllText(FlightsFile);
        return JsonConvert.DeserializeObject<List<Flight>>(json);
    }

    // Metoda do zapisu listy rezerwacji do pliku
    public static void SaveReservations(List<Reservation> reservations)
    {
        string json = JsonConvert.SerializeObject(reservations, Formatting.Indented, settings);
        File.WriteAllText(ReservationsFile, json);
    }

    // Metoda do odczytu listy rezerwacji z pliku
    public static List<Reservation> LoadReservations()
    {
        if (!File.Exists(ReservationsFile))
            return new List<Reservation>();

        string json = File.ReadAllText(ReservationsFile);
        return JsonConvert.DeserializeObject<List<Reservation>>(json);
    }
    
    // Metoda do zapisu listy pasażerów do pliku
    public static void SavePassengers(List<Passenger> passengers)
    {
        string json = JsonConvert.SerializeObject(passengers, Formatting.Indented, settings);
        File.WriteAllText(PassengersFile, json);
    }

    // Metoda do odczytu listy pasażerów z pliku
    public static List<Passenger> LoadPassengers()
    {
        if (!File.Exists(PassengersFile))
        {
            return new List<Passenger>();
        }
        string json = File.ReadAllText(PassengersFile);
        return JsonConvert.DeserializeObject<List<Passenger>>(json);
    }
}
