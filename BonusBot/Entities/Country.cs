using BonusBot.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.Entities
{
    public class Country
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        
        public List<Bookmaker> Bookmakers { get; set; }

        public Country()
        {
        }
    }
}
