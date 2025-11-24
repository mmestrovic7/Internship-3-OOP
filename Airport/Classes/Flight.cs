using Airport.Helpers;

namespace Airport.Classes;

public class Flight : Base
{
    public string FlightNumber { get; set; }
    public string DepartureLocation { get; set; }
    public string ArrivalLocation { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public TimeSpan Duration { get; set; }
    public double Distance { get; set; }
    public string PlaneId { get; set; }
    public string CrewId { get; set; }
    public Dictionary<SeatCategory, int> BookedSeatsByCategory { get; set; }

    public Flight()
    {
        BookedSeatsByCategory = new Dictionary<SeatCategory, int>();
    }

    public int GetTotalBookedSeats()
    {
        int total = 0;
        foreach (var booked in BookedSeatsByCategory.Values)
        {
            total += booked;
        }
        return total;
    }

    public bool HasAvailableSeats(Plane plane, SeatCategory category)
    {
        if (!plane.HasCategory(category))
            return false;

        int booked = BookedSeatsByCategory.ContainsKey(category) ? BookedSeatsByCategory[category] : 0;
        return booked < plane.SeatsByCategory[category];
    }
    
}