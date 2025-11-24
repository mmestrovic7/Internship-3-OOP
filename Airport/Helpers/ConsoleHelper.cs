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

        public static void PrintFlightHeader()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("id - naziv - vrijeme polaska - vrijeme dolaska - udaljenost - vrijeme putovanja");
            Console.ResetColor();
            
        }
        public static void PrintFlight(Flight? flight)
        {
            if (flight == null)
            {
                Console.WriteLine("No flight data available.");
                return;
            }
            string departureTime = flight.DepartureTime.ToString("yyyy-MM-dd HH:mm");
            string arrivalTime = flight.ArrivalTime.ToString("yyyy-MM-dd HH:mm");
            string duration = flight.Duration.ToString(@"hh\:mm");
            
            Console.WriteLine($"{flight.FlightNumber} - {flight.DepartureLocation} - {flight.ArrivalLocation} - {departureTime} - {arrivalTime}  - {flight.Distance} km - {duration}");
            
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