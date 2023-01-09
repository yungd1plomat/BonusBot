using System.Globalization;
using System.Text;
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class EditCountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public EditCountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.SelectCountry && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        var countryId = user.Data;
        var newName = message.Text;
        var country = await db.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
        await Client.SendTextMessageAsync(user.ChatId, $"Название измененно {country.Name} --> {newName}");
        country.Name = newName;
        user.Data = null;
    }
}