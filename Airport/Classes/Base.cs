namespace Airport.Classes;

public class Base
{
    public string Id { get; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    protected Base()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateTimestamp()
    {
        UpdatedAt = DateTime.Now;
    }
}