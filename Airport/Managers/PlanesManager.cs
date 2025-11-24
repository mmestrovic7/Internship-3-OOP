using Airport.Classes;
using Airport.Helpers;

namespace Airport.Managers;

public class PlanesManager
{
    private List<Plane> planes;

    public PlanesManager()
    {
        planes = new List<Plane>();
    }
    public void AddPlane(Plane plane)
    {
        planes.Add(plane);
    }

    public Plane GetPlaneById(string id)
    {
        return planes.FirstOrDefault(p => p.Id == id);
    }

    public Plane GetPlaneByName(string name)
    {
        return planes.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
    }

    public List<Plane> GetAllPlanes()
    {
        return planes;
    }

    public bool DeletePlane(string id, List<Flight> flights)
    {
        if (flights.Any(f => f.PlaneId == id))
            return false;

        var plane = planes.FirstOrDefault(p => p.Id == id);
        if (plane != null)
        {
            planes.Remove(plane);
            return true;
        }

        return false;
    }
}