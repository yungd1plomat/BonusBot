using System.Text;
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers;

public class InitAddBmHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitAddBmHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Добавить БК" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Выберите ID страны для БК:");
        sb.AppendLine();
        var countries = db.Countries.ToList();
        foreach (var country in countries)
        {
            sb.AppendLine($"{country.Id} - {country.Name}");
        }
        sb.AppendLine("none - без страны");
        await Client.SendTextMessageAsync(user.ChatId, sb.ToString());
        user.Data = null;
    }
}