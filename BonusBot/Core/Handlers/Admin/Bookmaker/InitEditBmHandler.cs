using System.Text;
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class InitEditBmHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitEditBmHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Редактировать БК" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Выберите ID БК для редактирования:");
        sb.AppendLine();
        var bookmakers = db.Bookmakers.Include(x => x.Country).ToList();
        foreach (var bookmaker in bookmakers)
        {
            sb.AppendLine($"{bookmaker.Id} - {bookmaker.Name} / {bookmaker.Country?.Name ?? "Без страны"}");
        }
        await Client.SendTextMessageAsync(user.ChatId, sb.ToString());
        user.Data = null;
    }
}