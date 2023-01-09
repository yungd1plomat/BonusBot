using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class AddBmHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public AddBmHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.SelectBmCountry;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var bookmaker = new Bookmaker()
        {
            Name = message.Text
        };
        if (user.Data is not null)
        {
            var countryId = user.Data;
            var country = await db.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
            bookmaker.Country = country;
        }
        user.Data = null;
        await db.Bookmakers.AddAsync(bookmaker);
        await Client.SendTextMessageAsync(user.ChatId, $"Букмекер {bookmaker.Name} добавлен");
    }
}