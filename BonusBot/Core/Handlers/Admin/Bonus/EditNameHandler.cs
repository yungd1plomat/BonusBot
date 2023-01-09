using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class EditNameHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public EditNameHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.SelectBonus && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var newName = message.Text;
        await Client.SendTextMessageAsync(user.ChatId, "Введите новое описание бонуса (none - чтобы оставить прежним)");
        if (newName == "none")
            return;
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Id == user.Data);
        bonus.Name = newName;
    }
}