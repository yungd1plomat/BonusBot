using System.Text;
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Core.Utils;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class InitRemoveBonusHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitRemoveBonusHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Удалить бонус" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Выберите ID бонуса для удаления: ");
        sb.AppendLine();
        var bonuses = db.Bonuses.Include(x => x.Bookmaker).ToList();
        foreach (var bonus in bonuses)
        {
            sb.AppendLine($"{bonus.Id} - {bonus.Name} / {bonus.Bookmaker?.Name} / {bonus.Link}");
        }
        await Client.SendTextMessageAsync(user.ChatId, sb.ToString());
        user.Data = null;
    }
}