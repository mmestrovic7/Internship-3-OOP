using Airport.Classes;
using Airport.Helpers;
using Airport.Managers;

namespace Airport.Menus;

public class PlanesMenu
{
    private PlanesManager planesManager;
        private FlightsManager flightsManager;

        public PlanesMenu(PlanesManager plm, FlightsManager fm)
        {
            planesManager = plm;
            flightsManager = fm;
        }

        public void Show()
        {
            while (true)
            {
                ConsoleHelper.PrintHeader("IZBORNIK - AVIONI");
                Console.WriteLine("1 - Prikaz svih aviona");
                Console.WriteLine("2 - Dodavanje novog aviona");
                Console.WriteLine("3 - Pretraživanje aviona");
                Console.WriteLine("4 - Brisanje aviona");
                Console.WriteLine("5 - Povratak na glavni izbornik");
                Console.WriteLine();
                Console.Write("Odabir: ");

                int choice = InputValidation.ValidIntegerInput(1,5);

                switch (choice)
                {
                    case 1:
                        ShowAllPlanes();
                        break;
                    case 2:
                        AddPlane();
                        break;
                    case 3:
                        SearchPlanes();
                        break;
                    case 4:
                        DeletePlane();
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

        private void ShowAllPlanes()
        {
            ConsoleHelper.PrintHeader("SVI AVIONI");

            var planes = planesManager.GetAllPlanes();
            if (planes.Count == 0)
            {
                ConsoleHelper.PrintInfo("Nema registriranih aviona.");
                ConsoleHelper.WaitForKey();
                return;
            }
            ConsoleHelper.PrintPlaneHeader();
            foreach (var plane in planes)
            {
                int totalFlights = 0;
                var flights = flightsManager.GetAllFlights();
                ConsoleHelper.PrintPlane(plane);
                foreach (var flight in flights)
                    if(plane.Id == flight.PlaneId)
                        totalFlights++;
                Console.WriteLine("Broj letova: " + totalFlights);
            }
            
            ConsoleHelper.WaitForKey();
        }
    private void AddPlane()
    {
        ConsoleHelper.PrintHeader("DODAVANJE AVIONA");

        string name = InputValidation.ReadLine("Naziv aviona: ");
        if (!InputValidation.IsValidName(name))
        {
            ConsoleHelper.PrintError("Neispravan naziv.");
            ConsoleHelper.WaitForKey();
            return;
        }
        Console.Write("Godina proizvodnje: ");
        int year = InputValidation.ValidIntegerInput();
        if (!InputValidation.IsValidManufactureYear(year))
        {
            ConsoleHelper.PrintError("Neispravna godina proizvodnje.");
            ConsoleHelper.WaitForKey();
            return;
        }

        var plane = new Plane
        {
            Name = name,
            ManufactureYear = year
        };

        Console.WriteLine("\nDostupne kategorije:");
        Console.WriteLine("1 - Standard");
        Console.WriteLine("2 - Business");
        Console.WriteLine("3 - VIP");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("Dodajte kategoriju (unesite 0 za završetak):");
            Console.Write("Odabir kategorije: ");
            int catChoice = InputValidation.ValidIntegerInput(0, 3);

            if (catChoice == 0)
                break;

            SeatCategory category;
            switch (catChoice)
            {
                case 1:
                    category = SeatCategory.Standard;
                    break;
                case 2:
                    category = SeatCategory.Business;
                    break;
                case 3:
                    category = SeatCategory.VIP;
                    break;
                default:
                    ConsoleHelper.PrintError("Neispravan odabir.");
                    continue;
            }

            if (plane.SeatsByCategory.ContainsKey(category))
            {
                ConsoleHelper.PrintWarning("Kategorija je već dodana.");
                continue;
            }
            Console.Write("Broj sjedala: ");
            int seats = InputValidation.ValidIntegerInput();
            if (!InputValidation.IsValidSeatCount(seats))
            {
                ConsoleHelper.PrintError("Neispravan broj sjedala.");
                continue;
            }

            plane.SeatsByCategory[category] = seats;
            ConsoleHelper.PrintSuccess($"Kategorija {category} dodana s {seats} sjedala.");
            Console.WriteLine();
        }

        if (plane.SeatsByCategory.Count == 0)
        {
            ConsoleHelper.PrintError("Avion mora imati barem jednu kategoriju sjedala.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (!ConsoleHelper.Confirm("Želite li dodati ovaj avion?"))
        {
            ConsoleHelper.PrintInfo("Dodavanje aviona prekinuto.");
            ConsoleHelper.WaitForKey();
            return;
        }

        planesManager.AddPlane(plane);
        ConsoleHelper.PrintSuccess("Avion uspješno dodan!");
        ConsoleHelper.WaitForKey();
    }

    private void SearchPlanes()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("PRETRAŽIVANJE AVIONA");
            Console.WriteLine("1 - Po ID-u");
            Console.WriteLine("2 - Po nazivu");
            Console.WriteLine("3 - Povratak");
            Console.WriteLine();
            Console.Write("Odabir: ");

            int choice = InputValidation.ValidIntegerInput(1, 3);
            
            switch (choice)
            {
                case 1:
                    SearchPlaneById();
                    break;
                case 2:
                    SearchPlaneByName();
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

    private void SearchPlaneById()
    {
        ConsoleHelper.PrintHeader("PRETRAŽIVANJE AVIONA PO ID-U");
        
        string id = InputValidation.ReadLine("Unesite ID aviona: ");
        var plane = planesManager.GetPlaneById(id);

        if (plane != null)
        {
            ConsoleHelper.PrintPlane(plane);
        }
        else
        {
            ConsoleHelper.PrintError("Avion nije pronađen.");
        }

        ConsoleHelper.WaitForKey();
    }

    private void SearchPlaneByName()
    {
        ConsoleHelper.PrintHeader("PRETRAŽIVANJE AVIONA PO NAZIVU");
        
        string name = InputValidation.ReadLine("Unesite naziv aviona: ");
        var plane = planesManager.GetPlaneByName(name);

        if (plane != null)
        {
            ConsoleHelper.PrintPlane(plane);
        }
        else
        {
            ConsoleHelper.PrintError("Avion nije pronađen.");
        }

        ConsoleHelper.WaitForKey();
    }

    private void DeletePlane()
    {
        ConsoleHelper.PrintHeader("BRISANJE AVIONA");
        
        var planes = planesManager.GetAllPlanes();
        if (planes.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nema registriranih aviona.");
            ConsoleHelper.WaitForKey();
            return;
        }

        Console.WriteLine("Svi avioni:\n");
        ConsoleHelper.PrintPlaneHeader();
        
        var flights = flightsManager.GetAllFlights();
        
        foreach (var plane in planes)
        {
            ConsoleHelper.PrintPlane(plane);
            
            var assignedFlights = flights.Where(f => f.PlaneId == plane.Id).ToList();
            if (assignedFlights.Count > 0)
            {
                ConsoleHelper.PrintWarning($"Ne može se izbrisati (dodijeljen na {assignedFlights.Count} letova)");
            }
            Console.WriteLine();
        }

        string id = InputValidation.ReadLine("Unesite ID aviona za brisanje (ili 'x' za povratak): ");
        if (id.Equals("x", StringComparison.OrdinalIgnoreCase))
            return;

        var planeToDelete = planesManager.GetPlaneById(id);
        if (planeToDelete == null)
        {
            ConsoleHelper.PrintError("Avion ne postoji.");
            ConsoleHelper.WaitForKey();
            return;
        }

        var assignedFlightsCount = flights.Where(f => f.PlaneId == planeToDelete.Id).ToList();
        if (assignedFlightsCount.Count > 0)
        {
            ConsoleHelper.PrintError($"Ne može se izbrisati - avion je dodijeljen na {assignedFlightsCount.Count} letova.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (!ConsoleHelper.Confirm($"Želite li stvarno izbrisati avion {planeToDelete.Name}?"))
        {
            ConsoleHelper.PrintInfo("Brisanje prekinuto.");
            ConsoleHelper.WaitForKey();
            return;
        }

        if (planesManager.DeletePlane(planeToDelete.Id, flights))
        {
            ConsoleHelper.PrintSuccess("Avion uspješno izbrisan!");
        }
        else
        {
            ConsoleHelper.PrintError("Greška pri brisanju aviona.");
        }

        ConsoleHelper.WaitForKey();
    }

}