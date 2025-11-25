using Airport.Helpers;
using Airport.Managers;
using Airport.Menus;

namespace Airport
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var passengerManager = new PassengerManager();
            var planesManager = new PlanesManager();
            var flightsManager = new FlightsManager();
            var crewManager = new CrewManager();

            InitializeData.Seed(passengerManager, planesManager, flightsManager, crewManager);

            var passengersMenu = new PassengersMenu(passengerManager, flightsManager, planesManager, crewManager);
            var flightsMenu = new FlightsMenu(flightsManager, planesManager, crewManager);
            var planesMenu = new PlanesMenu(planesManager, flightsManager);
            var crewMenu = new CrewMenu(crewManager);

            while (true)
            {
                ConsoleHelper.PrintHeader("SUSTAV ZA UPRAVLJANJE AERODROMOM");
                Console.WriteLine("1 - Putnici");
                Console.WriteLine("2 - Letovi");
                Console.WriteLine("3 - Avioni");
                Console.WriteLine("4 - Posada");
                Console.WriteLine("5 - Izlaz iz programa");
                Console.WriteLine();
                Console.Write("Odabir: ");

                var choice = InputValidation.ValidIntegerInput(1,5);

                switch (choice)
                {
                    case 1:
                        passengersMenu.Show();
                        break;
                    case 2:
                        flightsMenu.Show();
                        break;
                    case 3:
                        planesMenu.Show();
                        break;
                    case 4:
                        crewMenu.Show();
                        break;
                    case 5:
                        if (ConsoleHelper.Confirm("Želite li stvarno izaći iz programa?"))
                        {
                            ConsoleHelper.PrintInfo("Hvala što ste koristili sustav. Doviđenja!");
                            return;
                        }

                        break;
                    default:
                        ConsoleHelper.PrintError("Neispravan odabir.");
                        ConsoleHelper.WaitForKey();
                        break;
                }

            }
        }
    }
}