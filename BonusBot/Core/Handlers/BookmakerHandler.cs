using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class BookmakerHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public BookmakerHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return db.Bookmakers.Any(x => x.Name == message.Text);
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var bookmaker = db.Bookmakers.Include(x => x.Bonuses)
                                     .FirstOrDefault(x => x.Name == message.Text);
        var keyboard = KeyboardManager.BuildBonuses(bookmaker);
        await Client.SendTextMessageAsync(user.ChatId, "Выбирай подходящий тебе бонус! 😎", replyMarkup: keyboard);
    }
}