using System.Text.RegularExpressions;

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
    public static double ValidDoubleInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double result))
                return result;

            ConsoleHelper.PrintError("Neispravan unos. Molimo unesite decimalni broj.");
        }
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public static bool IsValidPassword(string password)
    {
        return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
    }

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 2 && name.All(c => char.IsLetter(c) || c == ' ' || c == '-');
    }

    public static bool IsValidBirthDay(DateTime birthDay)
    {
        int year = DateTime.Now.Year;
        return year >= 1900 && birthDay < DateTime.Now;
    }
    public static bool IsValidFutureDateTime(DateTime dateTime)
    {
        return dateTime > DateTime.Now;
    }

    public static bool IsValidDistance(double distance)
    {
        return distance > 0 && distance <= 25000;
    }
    public static bool IsValidManufactureYear(int year)
    {
        int currentYear = DateTime.Now.Year;
        return year >= 1950 && year <= currentYear;
    }
    public static bool IsValidSeatCount(int count)
    {
        return count > 0 && count <= 500;
    }

    public static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }
        
        
    public static DateTime ReadDateTime(string prompt, bool timeNecessary = false)
    {
        Console.WriteLine($"\n{prompt}");
    
        DateTime date = ReadDate("Datum");
        if (!timeNecessary) return date;
        TimeSpan time = ReadTime("Vrijeme");
        date = date.Add(time);
        return date;
    }

    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (format: dd.MM.yyyy): ");
            string? input = Console.ReadLine()?.Trim();
        
            if (DateTime.TryParseExact(input, "dd.MM.yyyy", 
                    null, System.Globalization.DateTimeStyles.None, out DateTime result))
                return result;
        
            ConsoleHelper.PrintError("Neispravan format datuma. Primjer: 25.12.2024");
        }
    }

    public static TimeSpan ReadTime(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (format: HH:mm): ");
            string? input = Console.ReadLine()?.Trim();
        
            if (TimeSpan.TryParseExact(input, "hh\\:mm", 
                    null, out TimeSpan result))
                return result;
        
            ConsoleHelper.PrintError("Neispravan format vremena. Primjer: 14:30");
        }
    }
}