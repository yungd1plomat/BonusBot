using BonusBot.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.Entities
{
    public class TGUser
    {
        public int Id { get; set; }
        public long ChatId { get; set; }

        public Command LastCommand { get; set; }

        public int? Data { get; set; }
        
        public bool IsAdmin { get; set; }

        public TGUser(long chatId, bool isAdmin = false)
        {
            ChatId = chatId;
            IsAdmin = isAdmin;
        }

        public TGUser()
        {
        }
}
}
