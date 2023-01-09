using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class AddDescHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public AddDescHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.AddName && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var description = message.Text;
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Id == user.Data);
        bonus.Description = description;
        await Client.SendTextMessageAsync(user.ChatId, "Введите ссылку на бонус");
    }
}