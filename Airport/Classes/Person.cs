namespace Airport.Classes;
public abstract class Person : Base
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public int GetAge()
    {
        var age = DateTime.Now.Year - BirthDay.Year;
        if (DateTime.Now.DayOfYear < BirthDay.DayOfYear)
            age--;
        return age;
    }
}