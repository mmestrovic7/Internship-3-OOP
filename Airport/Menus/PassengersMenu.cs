using Airport.Classes;
using Airport.Helpers;
using Airport.Managers;

namespace Airport.Menus;

public class PassengersMenu
{
    private PassengerManager passengerManager;
    private FlightsManager flightsManager;
    private PlanesManager planesManager;
    private CrewManager crewManager;

    public PassengersMenu(PassengerManager pm, FlightsManager fm, PlanesManager plm, CrewManager cm)
    {
        passengerManager = pm;
        flightsManager = fm;
        planesManager = plm;
        crewManager = cm;
    }
    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("IZBORNIK - PUTNICI");
            Console.WriteLine("1 - Registracija");
            Console.WriteLine("2 - Prijava");
            Console.WriteLine("3 - Povratak na glavni izbornik");
            Console.WriteLine();
            Console.Write("Odabir: ");

            int choice = InputValidation.ValidIntegerInput(1,3);

            switch (choice)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Login();
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
    private void Register()
    {
        ConsoleHelper.PrintHeader("REGISTRACIJA");

        string firstName = InputValidation.ReadLine("Ime: ");
        if (!InputValidation.IsValidName(firstName))
        {
            ConsoleHelper.PrintError("Neispravno ime.");
            ConsoleHelper.WaitForKey();
            return;
        }

        string lastName = InputValidation.ReadLine("Prezime: ");
        if (!InputValidation.IsValidName(lastName))
        {
            ConsoleHelper.PrintError("Neispravno prezime.");
            ConsoleHelper.WaitForKey();
            return;
        }

        DateTime birthDay = InputValidation.ReadDateTime("Datum rodenja: ");
        if (!InputValidation.IsValidBirthDay(birthDay))
        {
            ConsoleHelper.PrintError("Neispravna godina rođenja!");
            ConsoleHelper.WaitForKey();
            return;
        }

        string email = InputValidation.ReadLine("Email: ");
        if (!InputValidation.IsValidEmail(email))
        {
            ConsoleHelper.PrintError("Neispravan email format.");
            ConsoleHelper.WaitForKey();
            return;
        }

        string password = InputValidation.ReadLine("Lozinka (min. 6 znakova): ");
        if (!InputValidation.IsValidPassword(password))
        {
            ConsoleHelper.PrintError("Lozinka mora imati najmanje 6 znakova.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (passengerManager.RegisterPassenger(firstName, lastName, birthDay, email, password))
            ConsoleHelper.PrintSuccess("Uspješno ste se registrirali!");
        
        else
            ConsoleHelper.PrintError("Email je već registriran.");

        ConsoleHelper.WaitForKey();
    }
    private void Login()
    {
        ConsoleHelper.PrintHeader("PRIJAVA (test: petar@test.com, password123)");
        while (true)
        {
            string email = InputValidation.ReadLine("Email (ili 'x' za povratak): ");
            if (email.ToLower() == "x")
                return;

            string password = InputValidation.ReadLine("Lozinka: ");
            
            var passenger = passengerManager.Login(email, password);
            if (passenger != null)
            {
                ConsoleHelper.PrintSuccess($"Dobrodošli, {passenger.FirstName}!");
                ConsoleHelper.WaitForKey();
                ShowPassengerMenu(passenger);
                return;
            }
            else
            {
                ConsoleHelper.PrintError("Neispravni podaci. Pokušajte ponovo.");
                Console.WriteLine();
            }
        }
    }
    private void ShowPassengerMenu(Passenger passenger)
    {
        while (true)
        {
            ConsoleHelper.PrintHeader($"IZBORNIK PUTNIKA - {passenger.GetFullName()}");
            Console.WriteLine("1 - Prikaz mojih letova");
            Console.WriteLine("2 - Odabir leta");
            Console.WriteLine("3 - Pretraživanje letova");
            Console.WriteLine("4 - Otkazivanje leta");
            Console.WriteLine("5 - Odjava");
            Console.WriteLine();
            Console.Write("Odaberi: ");

            int choice = InputValidation.ValidIntegerInput(1,5);

            switch (choice)
            {
                case 1:
                    ShowMyFlights(passenger);
                    break;
                case 2:
                    SelectFlight(passenger);
                    break;
                case 3:
                    FlightSearchHelper.SearchFlights(flightsManager,  planesManager, crewManager);
                    break;
                case 4:
                    CancelFlight(passenger);
                    break;
                case 5:
                    return;
                default:
                    ConsoleHelper.PrintError("Neispravan odabir.");
                    ConsoleHelper.WaitForKey();
                    break;
            }
        }
    }
    private void ShowMyFlights(Passenger passenger)
    {
        ConsoleHelper.PrintHeader("MOJI LETOVI");

        var flightIds = passengerManager.GetPassengerFlights(passenger.Id);
        if (flightIds.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nemate rezerviranih letova.");
            ConsoleHelper.WaitForKey();
            return;
        }
        ConsoleHelper.PrintFlightHeader();
        foreach (var flightId in flightIds)
        {
            var flight = flightsManager.GetFlightById(flightId);
            
            ConsoleHelper.PrintFlight(flight);
        }

        ConsoleHelper.WaitForKey();
    }
    
    private void SelectFlight(Passenger passenger)
    {
        ConsoleHelper.PrintHeader("ODABIR LETA");

        var availableFlights = flightsManager.GetAvailableFlights();
        if (availableFlights.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nema dostupnih letova.");
            ConsoleHelper.WaitForKey();
            return;
        }

        Console.WriteLine("Dostupni letovi:\n");
        ConsoleHelper.PrintFlightHeader();
        foreach (var flight in availableFlights)
            ConsoleHelper.PrintFlight(flight);

        string flightId = InputValidation.ReadLine("Unesite ID leta (ili 'x' za povratak): ");
        if (flightId.ToLower() == "x")
            return;

        var selectedFlight = flightsManager.GetFlightById(flightId);
        if (selectedFlight == null)
        {
            ConsoleHelper.PrintError("Let ne postoji.");
            ConsoleHelper.WaitForKey();
            return;
        }

        var selectedPlane = planesManager.GetPlaneById(selectedFlight.PlaneId);
        if (selectedPlane == null)
        {
            ConsoleHelper.PrintError("Avion nije pronađen.");
            ConsoleHelper.WaitForKey();
            return;
        }

        Console.WriteLine("\nDostupne kategorije:");
        int categoryIndex = 1;
        var availableCategories = selectedPlane.SeatsByCategory.Keys.ToList();
        foreach (var category in availableCategories)
        {
            if (selectedFlight.HasAvailableSeats(selectedPlane, category))
            {
                Console.WriteLine($"{categoryIndex} - {category}");
                categoryIndex++;
            }
        }

        if (categoryIndex == 1)
        {
            ConsoleHelper.PrintError("Nema dostupnih mjesta na ovom letu.");
            ConsoleHelper.WaitForKey();
            return;
        }

        int categoryChoice = InputValidation.ValidIntegerInput();
        if (categoryChoice < 1 || categoryChoice >= categoryIndex)
        {
            ConsoleHelper.PrintError("Neispravan odabir.");
            ConsoleHelper.WaitForKey();
            return;
        }

        var selectedCategory = availableCategories[categoryChoice - 1];

        if (!ConsoleHelper.Confirm($"Želite li rezervirati let {selectedFlight.FlightNumber} u {selectedCategory} kategoriji?"))
        {
            ConsoleHelper.PrintInfo("Rezervacija otkazana.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (passengerManager.BookFlight(passenger.Id, selectedFlight.Id, selectedCategory) &&
                flightsManager.BookSeat(selectedFlight.Id, selectedCategory))
                ConsoleHelper.PrintSuccess("Let uspješno rezerviran!");
        else
            ConsoleHelper.PrintError("Greška pri rezervaciji leta.");

        ConsoleHelper.WaitForKey();
    }
    private void CancelFlight(Passenger passenger)
    {
        ConsoleHelper.PrintHeader("OTKAZIVANJE LETA");

        var flightIds = passengerManager.GetPassengerFlights(passenger.Id);
        if (flightIds.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nemate rezerviranih letova.");
            ConsoleHelper.WaitForKey();
            return;
        }
        
        var cancellableFlights = new List<Flight>();

        Console.WriteLine("Vaši letovi:\n");
        ConsoleHelper.PrintFlightHeader();

        foreach (var flightId in flightIds)
        {
            var flight = flightsManager.GetFlightById(flightId);
            var plane = planesManager.GetPlaneById(flight?.PlaneId);
            
            if (flight != null && plane != null)
            {
                double hoursUntil = (flight.DepartureTime - DateTime.Now).TotalHours;
                ConsoleHelper.PrintFlight(flight);
                if (hoursUntil <= 24)
                    ConsoleHelper.PrintWarning("Ne može se otkazati (manje od 24h do polaska)");
                else
                    cancellableFlights.Add(flight);
            }
        }
        
        if (cancellableFlights.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nema letova koje možete otkazati.");
            ConsoleHelper.WaitForKey();
            return;
        }
        
        string cancelId = InputValidation.ReadLine("Unesite ID leta za otkazivanje (ili 'x' za povratak): ");
        if (cancelId.Equals("x", StringComparison.OrdinalIgnoreCase))
            return;

        var flightToCancel = cancellableFlights.FirstOrDefault(f => f.Id == cancelId);
        if (flightToCancel == null)
        {
            ConsoleHelper.PrintError("Neispravan ID leta ili let se ne može otkazati.");
            ConsoleHelper.WaitForKey();
            return;
        }
        
        if (!ConsoleHelper.Confirm($"Želite li otkazati let {flightToCancel.FlightNumber}?"))
        {
            ConsoleHelper.PrintInfo("Otkazivanje prekinuto.");
            ConsoleHelper.WaitForKey();
            return;
        }
        
        var category = passengerManager.GetFlightCategory(passenger.Id, cancelId);
        if (passengerManager.CancelFlight(passenger.Id, cancelId) &&
            flightsManager.CancelSeat(cancelId, category))
            ConsoleHelper.PrintSuccess("Let uspješno otkazan!");
        
        else
            ConsoleHelper.PrintError("Greška pri otkazivanju leta.");

        ConsoleHelper.WaitForKey();
    }

}