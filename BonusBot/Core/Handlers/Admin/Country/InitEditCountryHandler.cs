using System.Text;
using BonusBot.Abstractions;
using BonusBot.Core.Data;
using BonusBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BonusBot.Core.Handlers.Admin;

public class InitEditCountryHandler : IHandler
{
    public ITelegramBotClient Client { get; set; }

    public InitEditCountryHandler(ITelegramBotClient client)
    {
        Client = client;
    }
    
    public bool IsMatch(Message message, TGUser user, AppDb db)
    {
        return message.Text == "Редактировать страну" && user.IsAdmin;
    }

    public async Task Handle(Message message, TGUser user, AppDb db)
    {
        StringBuilder sb = new StringBuilder();
        var countries = db.Countries.ToList();
        sb.AppendLine("Выберите ID страны для редактирования:");
        sb.AppendLine();
        foreach (var country in countries)
        {
            sb.AppendLine($"{country.Id} - {country.Name}");
        }
        await Client.SendTextMessageAsync(user.ChatId, sb.ToString());
    }
}