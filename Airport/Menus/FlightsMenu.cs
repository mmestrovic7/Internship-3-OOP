using Airport.Classes;
using Airport.Helpers;
using Airport.Managers;

namespace Airport.Menus;

public class FlightsMenu
{
    private FlightsManager flightsManager;
    private PlanesManager planesManager;
    private CrewManager crewManager;

    public FlightsMenu(FlightsManager fm, PlanesManager plm, CrewManager cm)
    {
        flightsManager = fm;
        planesManager = plm;
        crewManager = cm;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("IZBORNIK - LETOVI");
            Console.WriteLine("1 - Prikaz svih letova");
            Console.WriteLine("2 - Dodavanje leta");
            Console.WriteLine("3 - Pretraživanje letova");
            Console.WriteLine("4 - Uređivanje leta");
            Console.WriteLine("5 - Brisanje leta");
            Console.WriteLine("6 - Povratak na glavni izbornik");
            Console.WriteLine();
            Console.Write("Odabir: ");

            int choice = InputValidation.ValidIntegerInput(1, 6);

            switch (choice)
            {
                case 1:
                    ShowAllFlights();
                    break;
                case 2:
                    AddFlight();
                    break;
                case 3:
                    FlightSearchHelper.SearchFlights(flightsManager,  planesManager, crewManager);
                    break;
                case 4:
                    EditFlight();
                    break;
                case 5:
                    //DeleteFlight();
                    break;
                case 6:
                    return;
                default:
                    ConsoleHelper.PrintError("Neispravan odabir.");
                    ConsoleHelper.WaitForKey();
                    break;
            }
        }
    }
    private void ShowAllFlights()
    {
        ConsoleHelper.PrintHeader("SVI LETOVI");

        var flights = flightsManager.GetAllFlights();
        if (flights.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nema registriranih letova.");
            ConsoleHelper.WaitForKey();
            return;
        }
        ConsoleHelper.PrintFlightHeader();
        foreach (var flight in flights)
            ConsoleHelper.PrintFlight(flight);

        ConsoleHelper.WaitForKey();
    }

    private void AddFlight()
    {
        ConsoleHelper.PrintHeader("DODAVANJE LETA");

        string flightNumber = InputValidation.ReadLine("Broj leta: ");
        if (string.IsNullOrWhiteSpace(flightNumber))
        {
            ConsoleHelper.PrintError("Broj leta ne može biti prazan.");
            ConsoleHelper.WaitForKey();
            return;
        }

        string departure = InputValidation.ReadLine("Mjesto polaska: ");
        if (!InputValidation.IsValidName(departure))
        {
            ConsoleHelper.PrintError("Neispravno mjesto polaska.");
            ConsoleHelper.WaitForKey();
            return;
        }

        string arrival = InputValidation.ReadLine("Mjesto dolaska: ");
        if (!InputValidation.IsValidName(arrival))
        {
            ConsoleHelper.PrintError("Neispravno mjesto dolaska.");
            ConsoleHelper.WaitForKey();
            return;
        }

        DateTime departureTime = InputValidation.ReadDateTime("Vrijeme polaska", true);
        if (!InputValidation.IsValidFutureDateTime(departureTime))
        {
            ConsoleHelper.PrintError("Vrijeme polaska mora biti u budućnosti.");
            ConsoleHelper.WaitForKey();
            return;
        }

        DateTime arrivalTime = InputValidation.ReadDateTime("Vrijeme dolaska", true);
        if (arrivalTime <= departureTime)
        {
            ConsoleHelper.PrintError("Vrijeme dolaska mora biti nakon vremena polaska.");
            ConsoleHelper.WaitForKey();
            return;
        }

        double distance = InputValidation.ValidDoubleInput("Udaljenost (km): ");
        if (!InputValidation.IsValidDistance(distance))
        {
            ConsoleHelper.PrintError("Neispravna udaljenost.");
            ConsoleHelper.WaitForKey();
            return;
        }

        Console.WriteLine("\nDostupni avioni:");
        var planes = planesManager.GetAllPlanes();
        if (planes.Count == 0)
        {
            ConsoleHelper.PrintError("Nema dostupnih aviona.");
            ConsoleHelper.WaitForKey();
            return;
        }
        ConsoleHelper.PrintPlaneHeader();
        foreach (var plane in planes)
            ConsoleHelper.PrintPlane(plane);

        string planeId = InputValidation.ReadLine("Unesite ID aviona: ");
        var selectedPlane = planesManager.GetPlaneById(planeId);
        if (selectedPlane == null)
        {
            ConsoleHelper.PrintError("Avion ne postoji.");
            ConsoleHelper.WaitForKey();
            return;
        }

        Console.WriteLine("\nDostupne posade:");
        var crews = crewManager.GetAllCrews();
        if (crews.Count == 0)
        {
            ConsoleHelper.PrintError("Nema dostupnih posada.");
            ConsoleHelper.WaitForKey();
            return;
        }
        ConsoleHelper.PrintCrewHeader();
        foreach (var c in crews)
            ConsoleHelper.PrintCrew(c);

        string crewId = InputValidation.ReadLine("Unesite ID posade: ");
        var selectedCrew = crewManager.GetCrewById(crewId);
        if (selectedCrew == null || !selectedCrew.IsComplete())
        {
            ConsoleHelper.PrintError("Posada ne postoji ili nije kompletna.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (!ConsoleHelper.Confirm("Želite li dodati ovaj let?"))
        {
            ConsoleHelper.PrintInfo("Dodavanje leta prekinuto.");
            ConsoleHelper.WaitForKey();
            return;
        }

        var flight = new Flight
        {
            FlightNumber = flightNumber,
            DepartureLocation = departure,
            ArrivalLocation = arrival,
            DepartureTime = departureTime,
            ArrivalTime = arrivalTime,
            Duration = arrivalTime - departureTime,
            Distance = distance,
            PlaneId = planeId,
            CrewId = crewId 
        };

        flightsManager.AddFlight(flight);
        ConsoleHelper.PrintSuccess("Let uspješno dodan!");
        ConsoleHelper.WaitForKey();
    }

    private void EditFlight()
    {
        ConsoleHelper.PrintHeader("UREĐIVANJE LETA");

        string id = InputValidation.ReadLine("Unesite ID leta: ");
        var flight = flightsManager.GetFlightById(id);

        if (flight == null)
        {
            ConsoleHelper.PrintError("Let ne postoji.");
            ConsoleHelper.WaitForKey();
            return;
        }

        var plane = planesManager.GetPlaneById(flight.PlaneId);
        var crew = crewManager.GetCrewById(flight.CrewId);
        ConsoleHelper.PrintFlightDetailed(flight, plane, crew);

        Console.WriteLine("Što želite urediti?");
        Console.WriteLine("1 - Vrijeme polaska");
        Console.WriteLine("2 - Vrijeme dolaska");
        Console.WriteLine("3 - Oba vremena");
        Console.WriteLine("4 - Posadu");
        Console.WriteLine();

        int choice = InputValidation.ValidIntegerInput(1,4);

        DateTime? newDeparture = null;
        DateTime? newArrival = null;
        string newCrewId = null;

        if (choice == 1 || choice == 3)
        {
            newDeparture = InputValidation.ReadDateTime("Novo vrijeme polaska", true);
            if (!InputValidation.IsValidFutureDateTime(newDeparture.Value))
            {
                ConsoleHelper.PrintError("Vrijeme polaska mora biti u budućnosti.");
                ConsoleHelper.WaitForKey();
                return;
            }
        }
        if (choice == 2 || choice == 3)
        {
            newArrival = InputValidation.ReadDateTime("Novo vrijeme dolaska",true);
        
            DateTime compareTime = newDeparture ?? flight.DepartureTime;
            if (newArrival <= compareTime)
            {
                ConsoleHelper.PrintError("Vrijeme dolaska mora biti nakon vremena polaska.");
                ConsoleHelper.WaitForKey();
                return;
            }
        }

        if (choice == 4)
        {
            Console.WriteLine("\nDostupne posade:");
            var crews = crewManager.GetAllCrews();
            ConsoleHelper.PrintCrewHeader();
            foreach (var c in crews)
            {
                ConsoleHelper.PrintCrew(c);
            }

            newCrewId = InputValidation.ReadLine("Unesite ID nove posade: ");
            var selectedCrew = crewManager.GetCrewById(newCrewId);
            if (selectedCrew == null || !selectedCrew.IsComplete())
            {
                ConsoleHelper.PrintError("Posada ne postoji ili nije kompletna.");
                ConsoleHelper.WaitForKey();
                return;
            }
        }

        if (!ConsoleHelper.Confirm("Želite li spremiti promjene?"))
        {
            ConsoleHelper.PrintInfo("Uređivanje prekinuto.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (flightsManager.UpdateFlight(id, newDeparture, newArrival, newCrewId))
        {
            ConsoleHelper.PrintSuccess("Let uspješno uređen!");
        }
        else
        {
            ConsoleHelper.PrintError("Greška pri uređivanju leta.");
        }

        ConsoleHelper.WaitForKey();
    }
}
