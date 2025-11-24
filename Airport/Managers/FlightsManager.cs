using Airport.Classes;
using Airport.Helpers;

namespace Airport.Managers;

public class FlightsManager
{
    private List<Flight> flights;

        public FlightsManager()
        {
            flights = new List<Flight>();
        }

        public void AddFlight(Flight flight)
        {
            flights.Add(flight);
        }

        public Flight GetFlightById(string id)
        {
            return flights.FirstOrDefault(f => f.Id == id);
        }

        public Flight GetFlightByNumber(string flightNumber)
        {
            return flights.FirstOrDefault(f => f.FlightNumber.ToLower() == flightNumber.ToLower());
        }

        public List<Flight> GetAllFlights()
        {
            return new List<Flight>(flights);
        }

        public List<Flight> GetAvailableFlights()
        {
            return flights.Where(f => f.DepartureTime > DateTime.Now).ToList();
        }

        public bool UpdateFlight(string id, DateTime? newDepartureTime, DateTime? newArrivalTime, string newCrewId)
        {
            var flight = GetFlightById(id);
            if (flight == null)
                return false;

            if (newDepartureTime.HasValue)
            {
                flight.DepartureTime = newDepartureTime.Value;
            }

            if (newArrivalTime.HasValue)
            {
                flight.ArrivalTime = newArrivalTime.Value;
            }

            if (newDepartureTime.HasValue && newArrivalTime.HasValue)
            {
                flight.Duration = newArrivalTime.Value - newDepartureTime.Value;
            }

            if (!string.IsNullOrEmpty(newCrewId))
            {
                flight.CrewId = newCrewId;
            }

            flight.UpdateTimestamp();
            return true;
        }

        public bool DeleteFlight(string id, PlanesManager planesManager)
        {
            var flight = GetFlightById(id);
            if (flight == null)
                return false;

            var plane = planesManager.GetPlaneById(flight.PlaneId);
            if (plane == null)
                return false;

            var totalCapacity = plane.GetTotalCapacity();
            var bookedSeats = flight.GetTotalBookedSeats();
            var hoursUntilDeparture = (flight.DepartureTime - DateTime.Now).TotalHours;

            if (bookedSeats >= totalCapacity * 0.5)
                return false;

            if (hoursUntilDeparture <= 24)
                return false;

            flights.Remove(flight);
            return true;
        }

        public bool BookSeat(string flightId, SeatCategory category)
        {
            var flight = GetFlightById(flightId);
            if (flight == null)
                return false;

            if (!flight.BookedSeatsByCategory.ContainsKey(category))
            {
                flight.BookedSeatsByCategory[category] = 0;
            }

            flight.BookedSeatsByCategory[category]++;
            flight.UpdateTimestamp();
            return true;
        }

        public bool CancelSeat(string flightId, SeatCategory category)
        {
            var flight = GetFlightById(flightId);
            if (flight == null)
                return false;

            if (flight.BookedSeatsByCategory.ContainsKey(category) && flight.BookedSeatsByCategory[category] > 0)
            {
                flight.BookedSeatsByCategory[category]--;
                flight.UpdateTimestamp();
                return true;
            }

            return false;
        }
}