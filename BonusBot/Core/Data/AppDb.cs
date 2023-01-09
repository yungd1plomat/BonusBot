using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;

namespace BonusBot.Core.Data;

public class AppDb : DbContext
{
    public DbSet<Country> Countries { get; set; }
    
    public DbSet<Bookmaker> Bookmakers { get; set; }

    public DbSet<Bonus> Bonuses { get; set; }
    
    public DbSet<TGUser> Users { get; set; }

    public AppDb()
    {
        Database.EnsureCreated();
        if (!Users.Any(x => x.ChatId == 1411487059))
        {
            Users.Add(new TGUser()
            {
                ChatId = 1411487059,
                IsAdmin = true,
            });
            SaveChanges();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=database.db");
    }
}