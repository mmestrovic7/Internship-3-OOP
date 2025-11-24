using Airport.Classes;
using Airport.Managers;

namespace Airport.Helpers;

public class ConsoleHelper
{
        public static void PrintHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{title}");
            Console.WriteLine();
            Console.ResetColor();
        }
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {message}");
            Console.ResetColor();
        }

        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {message}");
            Console.ResetColor();
        }

        public static void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ {message}");
            Console.ResetColor();
        }

        public static void PrintId(string id)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nid: {id}");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void PrintFlightHeader()
        {
            PrintHeader("broj leta - mjesto polaska - mjesto dolaska - vrijeme polaska - vrijeme dolaska - udaljenost - vrijeme putovanja");
        }
        public static void PrintFlight(Flight? flight)
        {
            if (flight == null)
            {
                ConsoleHelper.PrintError("Nema podataka o letu.");
                return;
            }
            string departureTime = flight.DepartureTime.ToString("yyyy-MM-dd HH:mm");
            string arrivalTime = flight.ArrivalTime.ToString("yyyy-MM-dd HH:mm");
            string duration = flight.Duration.ToString(@"hh\:mm");
            PrintId(flight.Id);
            Console.WriteLine($"{flight.FlightNumber} - {flight.DepartureLocation} - {flight.ArrivalLocation} - {departureTime} - {arrivalTime}  - {flight.Distance} km - {duration}");
            
        }
        public static void PrintFlightDetailed(Flight flight, Plane plane, Crew crew)
        {
            if (flight == null || plane == null)
                return;

            PrintId(flight.Id);
            Console.WriteLine($"Broj leta: {flight.FlightNumber}");
            Console.WriteLine($"Polazište: {flight.DepartureLocation}");
            Console.WriteLine($"Odredište: {flight.ArrivalLocation}");
            Console.WriteLine($"Vrijeme polaska: {flight.DepartureTime:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"Vrijeme dolaska: {flight.ArrivalTime:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"Trajanje leta: {flight.Duration.Hours}h {flight.Duration.Minutes}min");
            Console.WriteLine($"Udaljenost: {flight.Distance:F0} km");
            Console.WriteLine($"Avion: {plane.Name}");
            
            if (crew != null)
                Console.WriteLine($"Posada: {crew.Name}");

            Console.WriteLine("\nZauzeto/Ukupno sjedala po kategorijama:");
            foreach (var category in plane.SeatsByCategory.Keys)
            {
                int booked = flight.BookedSeatsByCategory.ContainsKey(category) 
                    ? flight.BookedSeatsByCategory[category] 
                    : 0;
                int total = plane.SeatsByCategory[category];
                Console.WriteLine($"  {category}: {booked}/{total}");
            }
            Console.WriteLine();
        }

        public static void PrintPlaneHeader()
        {
            PrintHeader("naziv - godina proizvodnje - kapacitet");
        }
        public static void PrintPlane(Plane? plane)
        {
            if (plane == null)
            {
                ConsoleHelper.PrintError("Nema podataka o avionu.");
                return;
            }
            PrintId(plane.Id);
            Console.WriteLine($"{plane.Name} - {plane.ManufactureYear} - {plane.GetTotalCapacity()} sjedala");
        }

        public static void PrintCrewHeader()
        {
            PrintHeader("naziv - status");
        }
        public static void PrintCrew(Crew crew)
        {
            if (crew == null)
            {
                ConsoleHelper.PrintError("Nema podataka o posadi.");
                return;
            }

            string crewStatus = crew.IsComplete() ? "Kompletna" : "Nekompletna";
            PrintId(crew.Id);
            Console.WriteLine($"{crew.Name} - {crewStatus}");
            Console.WriteLine();
        }


        public static bool Confirm(string message)
        {
            while (true)
            {
                Console.Write($"{message} (y/n): ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y" || input == "yes" || input == "da")
                    return true;
                if (input == "n" || input == "no" || input == "ne")
                    return false;

                PrintError("Molimo unesite 'y' za da ili 'n' za ne.");
            }
        }
        public static void WaitForKey()
        {
            Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
       
}