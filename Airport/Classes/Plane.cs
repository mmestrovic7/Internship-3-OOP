using Airport.Helpers;

namespace Airport.Classes;

public class Plane : Base
{
    public string Name { get; set; }
    public int ManufactureYear { get; set; }
    public Dictionary<SeatCategory, int> SeatsByCategory { get; set; }
    public int CompletedFlights { get; set; }

    public Plane()
    {
        SeatsByCategory = new Dictionary<SeatCategory, int>();
        CompletedFlights = 0;
    }

    public int GetTotalCapacity()
    {
        int total = 0;
        foreach (var seats in SeatsByCategory.Values)
        {
            total += seats;
        }
        return total;
    }

    public bool HasCategory(SeatCategory category)
    {
        return SeatsByCategory.ContainsKey(category) && SeatsByCategory[category] > 0;
    }
}