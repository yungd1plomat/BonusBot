using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class CountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public CountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return db.Countries.Any(x => x.Name == message.Text);
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var country = await db.Countries.Include(x => x.Bookmakers)
                                              .FirstOrDefaultAsync(x => x.Name == message.Text);
        var keyboard = KeyboardManager.BuildMainKeyboard(Enumerable.Empty<Country>(), country.Bookmakers, true);
        await Client.SendTextMessageAsync(user.ChatId, "Выбери свой бк. 🙏", replyMarkup: keyboard);
    }
}