using Airport.Helpers;

namespace Airport.Classes;

public class Passenger : Person
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> BookedFlightIds { get; set; }
    public Dictionary<string, SeatCategory> FlightCategories { get; set; }

    public Passenger()
    {
        BookedFlightIds = new List<string>();
        FlightCategories = new Dictionary<string, SeatCategory>();
    }
}