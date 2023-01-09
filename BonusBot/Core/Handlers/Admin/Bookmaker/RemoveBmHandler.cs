using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class RemoveBmHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public RemoveBmHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitRemoveBookmaker;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        if (!int.TryParse(message.Text, out int Id))
        {
            await Client.SendTextMessageAsync(user.ChatId, "Некорректный ID!");
            throw new FormatException("Invalid Id when selecting bookmaker for remove");
        }
        var bookmaker = await db.Bookmakers.FirstOrDefaultAsync(x => x.Id == Id);
        if (bookmaker is null)
        {
            await Client.SendTextMessageAsync(user.ChatId, "БК не найден!");
            throw new NotImplementedException("Bookmaker not found");
        }
        await Client.SendTextMessageAsync(user.ChatId, $"БК {bookmaker.Name} удален");
        db.Bookmakers.Remove(bookmaker);
    }
}