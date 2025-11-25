using Airport.Classes;
using Airport.Helpers;
using Airport.Managers;

namespace Airport.Menus;

public class CrewMenu
{
    private CrewManager crewManager;

    public CrewMenu(CrewManager cm)
    {
        crewManager = cm;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("IZBORNIK - POSADA");
            Console.WriteLine("1 - Prikaz svih posada");
            Console.WriteLine("2 - Kreiranje nove posade");
            Console.WriteLine("3 - Dodavanje osobe");
            Console.WriteLine("4 - Povratak na glavni izbornik");
            Console.WriteLine();
            Console.Write("Odabir: ");

            int choice = InputValidation.ValidIntegerInput(1, 4);

            switch (choice)
            {
                case 1:
                    ShowAllCrews();
                    break;
                case 2:
                    CreateCrew();
                    break;
                case 3:
                    //AddCrewMember();
                    break;
                case 4:
                    return;
                default:
                    ConsoleHelper.PrintError("Neispravan odabir.");
                    ConsoleHelper.WaitForKey();
                    break;
            }
        }
        
    }
    void ShowAllCrews()
    {
        ConsoleHelper.PrintHeader("SVE POSADE");

        var crews = crewManager.GetAllCrews();
        if (crews.Count == 0)
        {
            ConsoleHelper.PrintInfo("Nema kreiranih posada.");
            ConsoleHelper.WaitForKey();
            return;
        }

        foreach (var crew in crews)
            ConsoleHelper.PrintCrewDetailed(crew, crewManager);

        ConsoleHelper.WaitForKey();
    }
    private void CreateCrew()
        {
            ConsoleHelper.PrintHeader("KREIRANJE POSADE");

            string name = InputValidation.ReadLine("Naziv posade: ");
            if (!InputValidation.IsValidName(name))
            {
                ConsoleHelper.PrintError("Neispravan naziv.");
                ConsoleHelper.WaitForKey();
                return;
            }

            var crew = new Crew { Name = name };

            Console.WriteLine("\nDostupni piloti:");
            var pilots = crewManager.GetAvailableCrewMembers(CrewPosition.Pilot);
            if (pilots.Count == 0)
            {
                ConsoleHelper.PrintError("Nema dostupnih pilota.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var pilot in pilots)
            {
                ConsoleHelper.PrintCrewMember(pilot);
            }

            string pilotId = InputValidation.ReadLine("Odaberite ID pilota: ");
            var selectedPilot = crewManager.GetCrewMemberById(pilotId);
            if (selectedPilot == null || selectedPilot.Position != CrewPosition.Pilot || !selectedPilot.IsAvailable())
            {
                ConsoleHelper.PrintError("Neispravan odabir pilota.");
                ConsoleHelper.WaitForKey();
                return;
            }
            crew.PilotId = pilotId;

            Console.WriteLine("\nDostupni kopiloti:");
            var copilots = crewManager.GetAvailableCrewMembers(CrewPosition.Copilot);
            if (copilots.Count == 0)
            {
                ConsoleHelper.PrintError("Nema dostupnih kopilota.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var copilot in copilots)
            {
                ConsoleHelper.PrintCrewMember(copilot);
            }

            string copilotId = InputValidation.ReadLine("Odaberite ID kopilota: ");
            var selectedCopilot = crewManager.GetCrewMemberById(copilotId);
            if (selectedCopilot == null || selectedCopilot.Position != CrewPosition.Copilot || !selectedCopilot.IsAvailable())
            {
                ConsoleHelper.PrintError("Neispravan odabir kopilota.");
                ConsoleHelper.WaitForKey();
                return;
            }
            crew.CopilotId = copilotId;

            Console.WriteLine("\nDostupne stjuardese/stjuardi:");
            var attendants = crewManager.GetAvailableCrewMembers(CrewPosition.FlightAttendant);
            if (attendants.Count < 2)
            {
                ConsoleHelper.PrintError("Potrebne su barem 2 dostupne stjuardese/stjuarda.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var attendant in attendants)
            {
                ConsoleHelper.PrintCrewMember(attendant);
            }

            for (int i = 1; i <= 2; i++)
            {
                string faId = InputValidation.ReadLine($"Odaberite ID stjuardese/stjuarda {i}: ");
                var selectedFa = crewManager.GetCrewMemberById(faId);
                
                if (selectedFa == null || selectedFa.Position != CrewPosition.FlightAttendant || 
                    !selectedFa.IsAvailable() || crew.FlightAttendantIds.Contains(faId))
                {
                    ConsoleHelper.PrintError("Neispravan odabir.");
                    ConsoleHelper.WaitForKey();
                    return;
                }
                
                crew.FlightAttendantIds.Add(faId);
            }

            if (!ConsoleHelper.Confirm("Želite li kreirati ovu posadu?"))
            {
                ConsoleHelper.PrintInfo("Kreiranje posade prekinuto.");
                ConsoleHelper.WaitForKey();
                return;
            }

            crewManager.AddCrew(crew);
            ConsoleHelper.PrintSuccess("Posada uspješno kreirana!");
            ConsoleHelper.WaitForKey();
        }
}