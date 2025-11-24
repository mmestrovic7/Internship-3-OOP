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
            Console.WriteLine("IZBORNIK - PUTNICI");
            Console.WriteLine("1 - Registracija");
            Console.WriteLine("2 - Prijava");
            Console.WriteLine("3 - Povratak na glavni izbornik");
            Console.WriteLine();
            Console.WriteLine("Odabir:");

            int choice = InputValidation.ValidIntegerInput(1,3);

            switch (choice)
            {
                case 1:
                    //Register();
                    break;
                case 2:
                    //Login();
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
    
}