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
                    //ShowAllFlights();
                    break;
                case 2:
                    //AddFlight();
                    break;
                case 3:
                    //SearchFlights();
                    break;
                case 4:
                    //EditFlight();
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
}
