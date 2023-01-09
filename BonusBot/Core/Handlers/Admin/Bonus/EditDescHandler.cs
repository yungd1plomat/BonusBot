using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class EditDescHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public EditDescHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.EditName && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var newDescription = message.Text;
        await Client.SendTextMessageAsync(user.ChatId, "Введите новую ссылку бонуса (none - оставить прежнюю)");
        if (newDescription== "none")
            return;
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Id == user.Data);
        bonus.Description = newDescription;
    }
}