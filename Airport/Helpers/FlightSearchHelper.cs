using Airport.Managers;

namespace Airport.Helpers;

public static class FlightSearchHelper
{
    public static void SearchFlights(FlightsManager flightsManager, PlanesManager planesManager, CrewManager crewManager)
        {
            while (true)
            {
                ConsoleHelper.PrintHeader("PRETRAŽIVANJE LETOVA");
                Console.WriteLine("1 - Po ID-u");
                Console.WriteLine("2 - Po broju leta");
                Console.WriteLine("3 - Povratak");
                Console.WriteLine();
                Console.Write("Odabir: ");

                int choice = InputValidation.ValidIntegerInput(1, 3);
        
                switch (choice)
                {
                    case 1:
                        SearchById(flightsManager, planesManager, crewManager);
                        break;
                    case 2:
                        SearchByNumber(flightsManager, planesManager, crewManager);
                        break;
                    case 3:
                        return;
                    default:
                        ConsoleHelper.PrintError("Neispravan odabir.");
                        ConsoleHelper.WaitForKey();
                        break;
                }
            }
        }

        private static void SearchById(FlightsManager flightsManager, PlanesManager planesManager, CrewManager crewManager)
        {
            ConsoleHelper.PrintHeader("PRETRAŽIVANJE PO ID-U");
    
            string id = InputValidation.ReadLine("Unesite ID leta: ");
            var flight = flightsManager.GetFlightById(id);

            if (flight != null)
            {
                var plane = planesManager.GetPlaneById(flight.PlaneId);
                var crew = crewManager.GetCrewById(flight.CrewId);
                ConsoleHelper.PrintFlightDetailed(flight, plane, crew);
            }
            else
            {
                ConsoleHelper.PrintError("Let nije pronađen.");
            }

            ConsoleHelper.WaitForKey();
        }

        private static void SearchByNumber(FlightsManager flightsManager, PlanesManager planesManager, CrewManager crewManager)
        {
            ConsoleHelper.PrintHeader("PRETRAŽIVANJE PO BROJU LETA (test: OU101)");
    
            string flightNumber = InputValidation.ReadLine("Unesite broj leta: ");
            var flight = flightsManager.GetFlightByNumber(flightNumber);

            if (flight != null)
            {
                var plane = planesManager.GetPlaneById(flight.PlaneId);
                var crew = crewManager.GetCrewById(flight.CrewId);
                ConsoleHelper.PrintFlightDetailed(flight, plane, crew);
            }
            else
            {
                ConsoleHelper.PrintError("Let nije pronađen.");
            }

            ConsoleHelper.WaitForKey();
        }
    
}