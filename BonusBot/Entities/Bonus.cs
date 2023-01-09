namespace BonusBot.Entities;

public class Bonus
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Link { get; set; }

    public Bookmaker? Bookmaker { get; set; }
    
    public Bonus()
    {
    }
}