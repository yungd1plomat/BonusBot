using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class SelectBonusBmHandler: IHandler
{
    public ITelegramBotClient Client { get; set; }

    public SelectBonusBmHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.AddLink && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        if (!int.TryParse(message.Text, out int Id))
        {
            await Client.SendTextMessageAsync(user.ChatId, "Некорректный ID!");
            throw new FormatException("Invalid id when selecting bookmaker for bonus");
        }
        var bookmaker = await db.Bookmakers.FirstOrDefaultAsync(x => x.Id == Id);
        if (bookmaker is null)
        {
            await Client.SendTextMessageAsync(user.ChatId, "БК не найдено!");
            throw new NotImplementedException("Bookmaker for bonus not found");
        }
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Id == user.Data);
        bonus.Bookmaker = bookmaker;
        await Client.SendTextMessageAsync(user.ChatId, "Бонус успешно добавлен");
        user.Data = null;
    }
}