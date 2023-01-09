using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class RemoveBonusHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public RemoveBonusHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return user.LastCommand == Command.InitRemoveBonus && user.IsAdmin;
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
        await Client.SendTextMessageAsync(user.ChatId, $"Бонус {bonus.Link} удален");
        db.Bonuses.Remove(bonus);
    }
}