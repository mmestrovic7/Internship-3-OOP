using Airport.Classes;
using Airport.Helpers;

namespace Airport.Managers;

public class CrewManager
{
    private List<CrewMember> crewMembers;
    private List<Crew> crews;

    public CrewManager()
    {
        crewMembers = new List<CrewMember>();
        crews = new List<Crew>();
    }

    public void AddCrewMember(CrewMember member)
    {
        crewMembers.Add(member);
    }

    public List<CrewMember> GetAllCrewMembers()
    {
        return new List<CrewMember>(crewMembers);
    }

    public List<CrewMember> GetAvailableCrewMembers(CrewPosition position)
    {
        return crewMembers.Where(m => m.Position == position && m.IsAvailable()).ToList();
    }

    public CrewMember GetCrewMemberById(string id)
    {
        return crewMembers.FirstOrDefault(m => m.Id == id);
    }

    public void AddCrew(Crew crew)
    {
        crews.Add(crew);

        var pilot = GetCrewMemberById(crew.PilotId);
        if (pilot != null)
            pilot.AssignedCrewId = crew.Id;

        var copilot = GetCrewMemberById(crew.CopilotId);
        if (copilot != null)
            copilot.AssignedCrewId = crew.Id;

        foreach (var faId in crew.FlightAttendantIds)
        {
            var fa = GetCrewMemberById(faId);
            if (fa != null)
                fa.AssignedCrewId = crew.Id;
        }
    }

    public List<Crew> GetAllCrews()
    {
        return new List<Crew>(crews);
    }

    public Crew GetCrewById(string id)
    {
        return crews.FirstOrDefault(c => c.Id == id);
    }
}