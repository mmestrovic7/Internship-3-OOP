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
                    //CreateCrew();
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
}