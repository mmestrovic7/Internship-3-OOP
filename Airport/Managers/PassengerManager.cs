using Airport.Classes;
using Airport.Helpers;

namespace Airport.Managers;

public class PassengerManager
{
     private List<Passenger> passengers;

        public PassengerManager()
        {
            passengers = new List<Passenger>();
        }

        public bool RegisterPassenger(string firstName, string lastName, DateTime birthDay, string email, string password)
        {
            if (passengers.Any(p => p.Email.ToLower() == email.ToLower()))
                return false;

            var passenger = new Passenger
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDay= birthDay,
                Email = email,
                Password = password
            };

            passengers.Add(passenger);
            return true;
        }

        public Passenger Login(string email, string password)
        {
            return passengers.FirstOrDefault(p => 
                p.Email.ToLower() == email.ToLower() && 
                p.Password == password);
        }

        public bool BookFlight(string passengerId, string flightId, SeatCategory category)
        {
            var passenger = passengers.FirstOrDefault(p => p.Id == passengerId);
            if (passenger == null)
                return false;

            if (!passenger.BookedFlightIds.Contains(flightId))
            {
                passenger.BookedFlightIds.Add(flightId);
                passenger.FlightCategories[flightId] = category;
                passenger.UpdateTimestamp();
                return true;
            }

            return false;
        }

        public bool CancelFlight(string passengerId, string flightId)
        {
            var passenger = passengers.FirstOrDefault(p => p.Id == passengerId);
            if (passenger == null)
                return false;

            if (passenger.BookedFlightIds.Remove(flightId))
            {
                passenger.FlightCategories.Remove(flightId);
                passenger.UpdateTimestamp();
                return true;
            }

            return false;
        }

        public List<string> GetPassengerFlights(string passengerId)
        {
            var passenger = passengers.FirstOrDefault(p => p.Id == passengerId);
            return passenger?.BookedFlightIds ?? new List<string>();
        }

        public SeatCategory GetFlightCategory(string passengerId, string flightId)
        {
            var passenger = passengers.FirstOrDefault(p => p.Id == passengerId);
            if (passenger != null && passenger.FlightCategories.ContainsKey(flightId))
                return passenger.FlightCategories[flightId];
            
            return SeatCategory.Standard;
        }
    }
