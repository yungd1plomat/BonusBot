using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class BonusHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public BonusHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return db.Bonuses.Any(x => x.Name == message.Text);
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Name == message.Text);
        var keyboard = KeyboardManager.BuildBonus(bonus);
        await Client.SendTextMessageAsync(user.ChatId, bonus.Description, replyMarkup: keyboard);
    }
}