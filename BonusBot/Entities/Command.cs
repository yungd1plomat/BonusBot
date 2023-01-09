using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.Entities
{
    /// <summary>
    /// Все команды бота
    /// </summary>
    public enum Command
    {
        None,
        Start,
        Admin,
        
        InitAddCountry,
        InitEditCountry,
        InitRemoveCountry,
        SelectCountry,
        EditCountry,
        AddCountry,
        RemoveCountry,
        
        InitAddBookmaker,
        InitEditBookmaker,
        InitRemoveBookmaker,
        RemoveBookmaker,
        EditBookmaker,
        SelectBmCountry,
        SelectEditBm,
        AddBookmaker,

        InitAddBonus,
        InitEditBonus,
        InitRemoveBonus,
        RemoveBonus,
        AddName,
        AddDescription,
        AddLink,
        SelectBonusBm,
        SelectBonus,
        EditName,
        EditDescription,
        EditLink,
        
        Cancel,
        
        InitSend,
        Send,

        CountrySelect,
        BookmakerSelect,
        BonusSelect,
        
        
    }
}
