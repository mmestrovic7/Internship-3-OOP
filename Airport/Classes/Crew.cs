namespace Airport.Classes;

public class Crew
{
    public string Name { get; set; }
    public string PilotId { get; set; }
    public string CopilotId { get; set; }
    public List<string> FlightAttendantIds { get; set; }

    public Crew()
    {
        FlightAttendantIds = new List<string>();
    }

    public bool IsComplete()
    {
        return !string.IsNullOrEmpty(PilotId) &&
               !string.IsNullOrEmpty(CopilotId) &&
               FlightAttendantIds.Count == 2;
    }
}