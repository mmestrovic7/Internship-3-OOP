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
                        //AddPlane();
                        break;
                    case 3:
                        //SearchPlanes();
                        break;
                    case 4:
                        //DeletePlane();
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
}