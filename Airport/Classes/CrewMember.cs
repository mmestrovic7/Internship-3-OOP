using Airport.Helpers;

namespace Airport.Classes;

public class CrewMember : Person
{
    public Gender Gender { get; set; }
    public CrewPosition Position { get; set; }
    public string AssignedCrewId { get; set; }

    public bool IsAvailable()
    {
        return string.IsNullOrEmpty(AssignedCrewId);
    }
    
}