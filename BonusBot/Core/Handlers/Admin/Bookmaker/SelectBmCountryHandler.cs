using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class SelectBmCountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public SelectBmCountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitAddBookmaker;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        if (message.Text == "none")
        {
            await Client.SendTextMessageAsync(user.ChatId, "Введите название БК");
            return;
        }

        if (!int.TryParse(message.Text, out int Id))
        {
            await Client.SendTextMessageAsync(user.ChatId, "Некорректный ID!");
            throw new FormatException("Invalid id when selecting country for bookmaker");
        }
        var country = await db.Countries.FirstOrDefaultAsync(x => x.Id == Id);
        if (country is null)
        {
            await Client.SendTextMessageAsync(user.ChatId, "Страна не найдена!");
            throw new NotImplementedException("Country not found");
        }
        user.Data = Id;
        await Client.SendTextMessageAsync(user.ChatId, "Введите название БК");
    }
}