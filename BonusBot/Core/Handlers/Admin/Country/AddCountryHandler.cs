using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class AddCountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public AddCountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }

    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitAddCountry && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        Country country = new Country()
        {
            Name = message.Text,
        };
        await db.Countries.AddAsync(country);
        await Client.SendTextMessageAsync(user.ChatId, "Страна успешно добавлена");
    }
}