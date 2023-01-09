using BonusBot.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.Entities
{
    public class Bookmaker
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public Country? Country { get; set; }

        public List<Bonus> Bonuses { get; set; }
        
        public Bookmaker()
        {
        }
    }
}
