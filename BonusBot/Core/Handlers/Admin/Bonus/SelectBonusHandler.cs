using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class SelectBonusHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public SelectBonusHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitEditBonus && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        if (!int.TryParse(message.Text, out int Id))
        {
            await Client.SendTextMessageAsync(user.ChatId, "Некорректный ID!");
            throw new FormatException("Invalid id when selecting bonus to edit");
        }
        var bonus = await db.Bonuses.FirstOrDefaultAsync(x => x.Id == Id);
        if (bonus is null)
        {
            await Client.SendTextMessageAsync(user.ChatId, "Бонус не найден!");
            throw new NotImplementedException("Bonus not found");
        }
        user.Data = bonus.Id;
        await Client.SendTextMessageAsync(user.ChatId, "Введите новое имя бонуса (none - чтобы оставить прежним)");
    }
}