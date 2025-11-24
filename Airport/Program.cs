using Airport.Helpers;

namespace Airport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Write(($"{SeatCategory.Business}"));
        }
    }
}