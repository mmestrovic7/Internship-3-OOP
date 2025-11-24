namespace Airport.Helpers;

public class InputValidation
{
    public static int ValidIntegerInput()
    {
        int intInput;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out intInput) && intInput >= 0)
                return intInput;

            ConsoleHelper.PrintError("Neispravan unos");
        }
    }
    public static int ValidIntegerInput(int min, int max)
    {
        int intInput;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out intInput) && intInput >= min && intInput <= max)
                return intInput;

            ConsoleHelper.PrintError($"Neispravan unos, unesi broj {min}-{max}: ");
        }
    }
    
}