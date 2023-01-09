using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class StartHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public StartHandler(ITelegramBotClient client)
    {
        Client = client;
    }

    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return true;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var countries = db.Countries.ToList();
        var bookmakers = db.Bookmakers.Where(x => x.Country == null);
        var keyboard = KeyboardManager.BuildMainKeyboard(countries, bookmakers);
        await Client.SendTextMessageAsync(user.ChatId, "Чтобы получить крутой бонус, выбери свою страну и бк. 🙏", replyMarkup: keyboard);
    }
}